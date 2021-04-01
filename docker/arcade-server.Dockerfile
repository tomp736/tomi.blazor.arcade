# build intermediary
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

WORKDIR /code
COPY ./src/tomi.arcade.server/tomi.arcade.server.csproj ./src/tomi.arcade.server/
COPY ./src/tomi.arcade.proto ./src/tomi.arcade.proto


WORKDIR /code/src/tomi.arcade.server
RUN dotnet restore "tomi.arcade.server.csproj"

# build 
COPY ./src/tomi.arcade.server /code/src/tomi.arcade.server
RUN dotnet build "tomi.arcade.server.csproj" -c Release -o /build

# publish 
FROM build AS publish
RUN dotnet publish "tomi.arcade.server.csproj" -c Release -o /publish

# final image on aspnet-focal
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app
COPY --from=publish /publish .

ENTRYPOINT [ "dotnet", "tomi.arcade.server.dll"]
