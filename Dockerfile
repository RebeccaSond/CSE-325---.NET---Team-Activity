FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["RestaurantOrderingSystem.csproj", "./"]
RUN dotnet restore "RestaurantOrderingSystem.csproj"

COPY . .
RUN dotnet publish "RestaurantOrderingSystem.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "RestaurantOrderingSystem.dll"]





