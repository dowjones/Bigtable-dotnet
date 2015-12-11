## Installation Guide ##
This library is cross-platform and cross-architecture.  The gRPC library needed a few adjustments for this to work, and until those are merged back upstream, it is necessary to take a few extra steps in order to get started.

### The easy way ###

To prime the build, you can simply run the command line builds script from the repository's root folder: 

```

scripts\build-debug.bat
scripts\build-release.bat

or

script/build.sh /property:Configuration=Debug
script/build.sh /property:Configuration=Release

```

You can now open `src\Bigtable.NET.sln`. 


### The hard way ###
Open submodules\grpc\vsprojects\grpc_csharp_ext.sln in Visual Studio 2013.  Build 32 bit version and 64 bit version.  Close.

Open submodules\grpc\src\csharp\Grpc.sln in Visual Studio 2013 or 2015.  Make sure you have the Package Restore turned on.  In Visual Studio 2015, the options are:

- Allow NuGet to download missing packages
- Automatically check for missing packages during build

Both should be checked.  Build the solution.

This step is necessary because of an inconsistency in how submodule dependencies are handled.  Microsoft is working on a solution.

From here you can follow the directions from the [readme](../README.md#getting-started) to get started.

## Deploying to Windows ##

The BigtableNet.Native project contains a targets reference which will copy the native libraries to your build folder.  There are separate dlls for Debug and Release.  With this, you should be able to deploy to any (non-RT, non-CE) Windows platform that supports .NET 4.5 and has MSVCRT.DLL (7.0.7601.17744).

No effort has been made to test against CE.  There is a good chance that there will be missing methods/libraries between the OS and .NET platform.

No effort has been made to test against RT.  This is a good chance it can be made to work.  See [compiling from scratch](#compiling-from-scratch)


## Deploying to Linux ##

Deploying to Linux was successfully tested on 64-bit Ubuntu 15.04, Kernel 3.19.0-28.  The compilation was performed by XBuild 12.0 using Mono 4.2.1 which was built from source.

Directions for compiling Mono can be found [here](http://www.mono-project.com/docs/compiling-mono/linux/).

If you are using apt-get, yum, or pacman and already have a sufficient version of mono, you should make sure build tools are installed

``` 

 sudo apt-get install git autoconf libtool automake build-essential mono-devel gettext unzip

```
 
Start by cloning this repository.

You will need to build and install [protobuf](https://github.com/google/protobuf.git) as a prerequisite.  This is a submodule of grpc, which is a submodule of Bigtable.NET:

```

git submodule update --init
cd submodules/grpc
git submodule update --init
cd third_party/protobuf
./autogen.sh
./configure
make
make install

```

You will then need reconfigure the dynamic linker's runtime bindings:

```

ldconfig

```

You will need to download, build and install grpc from their [github repository](https://github.com/grpc/grpc.git)


In addition to building grpc, you will need to build and install the csharp plugin:

```

	make shared_csharp
	make install_csharp

```

Lastly, you must install root certificates, if they have not been installed.

```

	mozroots --import --ask-remove

```

From here you can follow the directions from the [readme](../README.md#GettingStarted) to get started.


## Compiling from scratch ##

The project has the submodules which also has submodules.  Changes have been made to the submodules which have not been committed back to the upstream project.  Once that is done, this process will become easier.

After cloning this repository, do the following steps in git bash.

```

	git submodule update --init
	cd submodule/grpc
	git submodule update --init

```

Now the submodule can be built.  Using Visual Studio 2013, open and build:

```

	submodule\grpc\vsprojects\grpc.sln
	submodule\grpc\vsprojects\grpc_csharp_ext.sln

```

Optional, you can rebuild.  This is already provided in the tools folder.

```

	submodule\grpc\vsprojects\grpc_protoc_plugins.sln


```

Then, from the root of the repository folder, run:

```

	scripts/generate-proto-classes.sh
	or
	scripts\generate-proto-classes.bat

```

This will regenerate all of the protobuf classes in ```src\Native\Generated```. 