#!/bin/bash

# Run this tool from root git folder:  scripts/generate-proto-classes.sh

# Locals
array=( api bigtable/v1 bigtable/admin/cluster/v1 bigtable/admin/table/v1 longrunning protobuf rpc type )
dest=src/Native/Generated

# Loops through proto folders
for i in "${array[@]}"
do
	echo Generating $i...
	# Generate classes for each proto file
	tools/protoc.exe -Iprotos --csharp_out $dest --grpc_out $dest --plugin=protoc-gen-grpc=tools/grpc_csharp_plugin.exe  protos/google/$i/*.proto
done

# Woot
echo Done!
