# build intermediary
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

WORKDIR /code
COPY ./src/tomi.arcade.game.client/tomi.arcade.game.client.csproj ./src/tomi.arcade.game.client/
COPY ./src/tomi.arcade.proto ./src/tomi.arcade.proto


WORKDIR /code/src/tomi.arcade.game.client
RUN dotnet restore "tomi.arcade.game.client.csproj"

# build 
COPY ./src/tomi.arcade.game.client /code/src/tomi.arcade.game.client
RUN dotnet build "tomi.arcade.game.client.csproj" -c Release -o /build

# publish 
FROM build AS publish
RUN dotnet publish "tomi.arcade.game.client.csproj" -c Release -o /publish

# final image on nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
