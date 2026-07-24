FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebTIC.API.csproj ./
RUN dotnet restore WebTIC.API.csproj

COPY . .
RUN dotnet publish WebTIC.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render inyecta PORT en tiempo de ejecucion; Kestrel debe escuchar ahi.
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebTIC.API.dll"]
