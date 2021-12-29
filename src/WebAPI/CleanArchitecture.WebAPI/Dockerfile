#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/WebAPI/StoreSalesAdvisor.WebAPI/StoreSalesAdvisor.WebAPI.csproj", "src/WebAPI/StoreSalesAdvisor.WebAPI/"]
COPY ["src/Core/StoreSalesAdvisor.Application/StoreSalesAdvisor.Application.csproj", "src/Core/StoreSalesAdvisor.Application/"]
COPY ["src/Core/StoreSalesAdvisor.Domain/StoreSalesAdvisor.Domain.csproj", "src/Core/StoreSalesAdvisor.Domain/"]
COPY ["src/Infrastructure/StoreSalesAdvisor.Persistence/StoreSalesAdvisor.Persistence.csproj", "src/Infrastructure/StoreSalesAdvisor.Persistence/"]
RUN dotnet restore "src/WebAPI/StoreSalesAdvisor.WebAPI/StoreSalesAdvisor.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI/StoreSalesAdvisor.WebAPI"
RUN dotnet build "StoreSalesAdvisor.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreSalesAdvisor.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreSalesAdvisor.WebAPI.dll"]