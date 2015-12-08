## Bigtable.NET Examples: Web ##

This project demonstrates consumption of [BigtableNET.Models](../../Models) and provides a web interface.  This can be handy because at this time there is no such interface for Bigtable.

The project uses [Nancy](http://nancyfx.org/) because it needs to be cross-platform and MVC 4 and 5 [are not fully supported on Mono](http://www.mono-project.com/docs/about-mono/compatibility/), yet.

This project also uses [Autofac](http://autofac.org/) to provide simple services to the Webhost.

