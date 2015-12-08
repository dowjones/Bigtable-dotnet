using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BigtableNet.Common.Extensions;
using BigtableNet.Models.Types;
using Examples.Web.Helpers;
using Nancy;

namespace Examples.Web.Modules
{
    public class BigModule : NancyModule
    {

        public BigModule(IClientFactory clientFactory, ITableNameCache tableNameCache)
        {
            Get["/big/tables"] = _ =>
            {
                var model = new TablesModel();

                using (var adminClient = clientFactory.GetAdminClient())
                {
                    var tables = adminClient.ListTablesAsync().Result;

                    model.Tables.AddRange(tables.Select(x => new TablesEntryModel
                    {
                        Name = x.Name
                    }));
                }

                return View["Tables.sshtml", model];
            };



            Get["/big/data/list"] = _ =>
            {
                var model = new DataListModel
                {
                    Table = Request.Query["table"],
                    StartKey = Request.Query["skey"],
                    CurrentKey = Request.Query["ckey"],
                    EndKey = Request.Query["ekey"]
                };

                model.SetTableNames(tableNameCache.Names);
                model.SetMaxRows(Request.Query["maxrows"]);

                if (string.IsNullOrEmpty(model.CurrentKey))
                {
                    model.CurrentKey = model.StartKey;
                }

                if (!string.IsNullOrEmpty(model.Table) && !string.IsNullOrEmpty(model.StartKey) && !string.IsNullOrEmpty(model.EndKey))
                {
                    using (var dataClient = clientFactory.GetDataClient())
                    {
                        var table = new BigTable(model.Table);

                        var rows = dataClient.GetRowsAsync(table, model.CurrentKey, model.EndKey, model.MaxRows + 1).Result;

                        foreach (var row in rows)
                        {
                            if (model.KeyCount < model.MaxRows)
                            {
                                model.AddRow(row);
                            }
                            else
                            {
                                model.NextKey = row.KeyString;
                            }
                        }
                    }
                }

                return View["DataList.sshtml", model];
            };



            Get["/big/data/sample"] = _ =>
            {
                const string timeSpanFormat = @"hh\:mm\:ss\.ff";

                var model = new DataSampleModel
                {
                    Table = Request.Query["table"]
                };

                model.SetTableNames(tableNameCache.Names);
                model.SetMaxRows(Request.Query["maxrows"]);

                if (!string.IsNullOrEmpty(model.Table))
                {
                    using (var dataClient = clientFactory.GetDataClient())
                    {
                        var table = new BigTable(model.Table);

                        Stopwatch watch = Stopwatch.StartNew();
                        var keys = dataClient.SampleRowKeysAsync(table).Result.ToList();
                        watch.Stop();
                        model.SampleElapsed = watch.Elapsed.ToString(timeSpanFormat);
                        model.SampleCount = keys.Count;

                        watch = Stopwatch.StartNew();

                        foreach (var batch in keys.Take(model.MaxRows).Batch(50))
                        {
                            var rowTasks = batch
                                .Select(x => new
                                {
                                    Task = dataClient.GetRowsAsync(table, x.Key, null, 1),
                                    x.KeyString
                                })
                                .ToArray();

                            Task.WhenAll(rowTasks.Select(x => x.Task)).Wait();      // silly syntax to avoid resharper warning

                            foreach (var rowset in rowTasks)
                            {
                                if (rowset.Task.Result != null)
                                {
                                    var row = rowset.Task.Result.FirstOrDefault();
                                    if (row != null)
                                    {
                                        model.AddRow(row);
                                    }
                                }
                            }
                        }

                        watch.Stop();
                        model.GetRowsElapsed = watch.Elapsed.ToString(timeSpanFormat);
                    }
                }

                return View["DataSample.sshtml", model];
            };
        }



        public class TablesModel
        {
            private readonly List<TablesEntryModel> _tables = new List<TablesEntryModel>();

            public List<TablesEntryModel> Tables { get { return _tables; } }
        }


        public class TablesEntryModel
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }



        public abstract class DataListRowHolder
        {
            private readonly List<DataListRowModel> _rows = new List<DataListRowModel>();

            public List<DataListRowModel> Rows { get { return _rows; } }

            public IEnumerable<TablesEntryModel> Tables { get; set; }

            public string Table { get; set; }
            public int MaxRows { get; set; }

            public int KeyCount { get; private set; }


            public void SetMaxRows(string qs)
            {
                int scratch;
                if (int.TryParse(qs, out scratch))
                {
                    MaxRows = Math.Min(scratch, 1000);      // insanity stopgap
                }
                else
                {
                    MaxRows = 10;
                }
            }


            public void SetTableNames(IEnumerable<string> names)
            {
                Tables = names.Select(x => new TablesEntryModel
                {
                    Name = x,
                    IsSelected = Table == x
                });
            }


            public void AddRow(BigRow bigRow)
            {
                KeyCount += 1;

                bool added = false;
                foreach (var family in bigRow.GetFamilyNames())
                {
                    foreach (var field in bigRow.GetFields(family))
                    {
                        AddRow(bigRow.KeyString, family, field.ColumnName, field.StringValue, field.Timestamp);
                        added = true;
                    }
                }
                
                if (!added)
                {
                    // No data for this key? Add an entry anyway...
                    AddRow(bigRow.KeyString, null, null, null, null);
                }
            }


            private DataListRowModel _lastKeyRow;
            private DataListRowModel _lastFamilyRow;
            private DataListRowModel _lastColumnRow;
            private DataListRowModel _lastTimeStampRow;
            private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            private void AddRow(string key, string family, string column, string value, long? timestamp)
            {
                var row = new DataListRowModel();

                if ((_lastKeyRow == null) || (key != _lastKeyRow.Key))
                {
                    row.Key = key;
                    row.KeyRowSpan = 1;
                    _lastKeyRow = row;
                    _lastFamilyRow = null;
                    _lastColumnRow = null;
                    _lastTimeStampRow = null;
                }
                else
                {
                    _lastKeyRow.KeyRowSpan += 1;
                }

                if ((_lastFamilyRow == null) || (family != _lastFamilyRow.Family))
                {
                    row.Family = family ?? "n/a";
                    row.FamilyRowSpan = 1;
                    _lastFamilyRow = row;
                    _lastColumnRow = null;
                }
                else
                {
                    _lastFamilyRow.FamilyRowSpan += 1;
                }

                if ((_lastColumnRow == null) || (column != _lastColumnRow.Column))
                {
                    row.Column = column ?? "n/a";
                    row.ColumnRowSpan = 1;
                    _lastColumnRow = row;
                }
                else
                {
                    _lastColumnRow.ColumnRowSpan += 1;
                }

                if (timestamp.HasValue)
                {
                    var millis = timestamp.Value;
                    // TODO - look at the table info to know if timestamp is micro- or nano- (or milli-?) seconds
                    millis /= 1000;     // for now, assume microseconds
                    var tstring = UnixEpoch.AddMilliseconds(millis).ToString("dd-MMM-yyyy HH:mm:ss.fff K");

                    if ((_lastTimeStampRow == null) || (tstring != _lastTimeStampRow.TimeStamp))
                    {
                        row.TimeStamp = tstring;
                        row.TimeStampRowSpan = 1;
                        _lastTimeStampRow = row;
                    }
                    else
                    {
                        _lastTimeStampRow.TimeStampRowSpan += 1;
                    }
                }
                else
                {
                    _lastTimeStampRow = null;
                }

                row.Value = value;

                _rows.Add(row);
            }
        }


        public class DataListModel : DataListRowHolder
        {
            public string StartKey { get; set; }
            public string CurrentKey { get; set; }
            public string EndKey { get; set; }

            public bool HasStartLink { get { return StartKey != CurrentKey; } }

            public bool HasNextLink { get { return !string.IsNullOrEmpty(NextKey); } }
            public string NextKey { get; set; }
        }



        public class DataSampleModel : DataListRowHolder
        {
            public string SampleElapsed { get; set; }
            public string GetRowsElapsed { get; set; }
            public int SampleCount { get; set; }
        }



        public class DataListRowModel
        {
            public string Key { get; set; }
            public int KeyRowSpan { get; set; }
            public bool HasKey { get { return !string.IsNullOrEmpty(Key); } }

            public string Family { get; set; }
            public int FamilyRowSpan { get; set; }
            public bool HasFamily { get { return !string.IsNullOrEmpty(Family); } }

            public string Column { get; set; }
            public int ColumnRowSpan { get; set; }
            public bool HasColumn { get { return !string.IsNullOrEmpty(Column); } }

            public int TimeStampRowSpan { get; set; }
            public bool HasTimeStamp { get { return !string.IsNullOrEmpty(TimeStamp); } }

            public string Value { get; set; }
            public string TimeStamp { get; set; }
        }
    }
}
