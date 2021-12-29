#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/WebAPI/CleanArchitecture.WebAPI/CleanArchitecture.WebAPI.csproj", "src/WebAPI/CleanArchitecture.WebAPI/"]
COPY ["src/Core/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/Core/CleanArchitecture.Application/"]
COPY ["src/Core/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/Core/CleanArchitecture.Domain/"]
COPY ["src/Infrastructure/CleanArchitecture.Persistence/CleanArchitecture.Persistence.csproj", "src/Infrastructure/CleanArchitecture.Persistence/"]
RUN dotnet restore "src/WebAPI/CleanArchitecture.WebAPI/CleanArchitecture.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI/CleanArchitecture.WebAPI"
RUN dotnet build "CleanArchitecture.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.WebAPI.dll"]