FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ProductApi.sln ./
COPY Directory.Build.props ./
COPY src/ProductApi.Api/ProductApi.Api.csproj src/ProductApi.Api/
COPY src/ProductApi.Application/ProductApi.Application.csproj src/ProductApi.Application/
COPY src/ProductApi.Domain/ProductApi.Domain.csproj src/ProductApi.Domain/
COPY src/ProductApi.Infrastructure/ProductApi.Infrastructure.csproj src/ProductApi.Infrastructure/
COPY tests/ProductApi.Tests/ProductApi.Tests.csproj tests/ProductApi.Tests/

RUN dotnet restore ProductApi.sln

COPY . .
RUN dotnet publish src/ProductApi.Api/ProductApi.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductApi.Api.dll"]