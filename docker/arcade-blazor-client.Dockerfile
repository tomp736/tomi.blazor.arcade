FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

WORKDIR /code

COPY ./tomi.blazor.arcade.sln .
COPY ./src/tomi.arcade.game.client/tomi.arcade.game.client.csproj ./src/tomi.arcade.game.client/
COPY ./src/tomi.arcade.proto/tomi.arcade.proto.csproj ./src/tomi.arcade.proto/

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build ./test/Ingredients.Tests/Ingredients.Tests.csproj
RUN dotnet publish src/Ingredients -c Release -o /app --no-build

# host blazor from nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
