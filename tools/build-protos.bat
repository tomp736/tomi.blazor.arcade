protoc-3.15.6-win64\bin\protoc.exe -I=..\src\tomi.arcade.proto\Protos gameoflife.proto --js_out=import_style=commonjs:..\src\tomi.arcade.client.blazor\wwwroot\js\grpc --grpc-web_out=import_style=commonjs,mode=grpcwebtext:..\src\tomi.arcade.client.blazor\wwwroot\js\grpc