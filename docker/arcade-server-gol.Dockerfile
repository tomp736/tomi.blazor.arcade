# build intermediary
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
EXPOSE 80
EXPOSE 443

WORKDIR /code
COPY ./src/tomi.arcade.game.gol.server/tomi.arcade.game.gol.server.csproj ./src/tomi.arcade.game.gol.server/
COPY ./src/tomi.arcade.game.gol.server/tomi.arcade.game.gol.csproj ./src/tomi.arcade.game.gol/
COPY ./src/tomi.arcade.proto ./src/tomi.arcade.proto


WORKDIR /code/src/tomi.arcade.game.gol.server
RUN dotnet restore "tomi.arcade.game.gol.server.csproj"

# build 
COPY ./src/tomi.arcade.game.gol.server /code/src/tomi.arcade.game.gol.server
COPY ./src/tomi.arcade.game.gol /code/src/tomi.arcade.game.gol
RUN dotnet build "tomi.arcade.game.gol.server.csproj" -c Release -o /build

# publish 
FROM build AS publish
RUN dotnet publish "tomi.arcade.game.gol.server.csproj" -c Release -o /publish

# final image on aspnet-focal
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app
COPY --from=publish /publish ./

ENTRYPOINT [ "dotnet", "tomi.arcade.game.gol.server.dll"]
