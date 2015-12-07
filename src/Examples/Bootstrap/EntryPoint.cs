namespace Examples.Bootstrap
{
    class EntryPoint
    {
        public static void Main()
        {
            Data.Loader.Run(Utilities.GetConfig()).Wait();
        }
    }
}
