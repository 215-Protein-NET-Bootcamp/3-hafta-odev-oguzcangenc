#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/JWTAuth.WebAPI/JWTAuth.WebAPI.csproj", "src/JWTAuth.WebAPI/"]
COPY ["src/JWTAuth.Data/JWTAuth.Data.csproj", "src/JWTAuth.Data/"]
COPY ["src/JWTAuth.Entities/JWTAuth.Entities.csproj", "src/JWTAuth.Entities/"]
COPY ["src/JWTAuth.Core/JWTAuth.Core.csproj", "src/JWTAuth.Core/"]
COPY ["src/JWTAuth.Business/JWTAuth.Business.csproj", "src/JWTAuth.Business/"]
RUN dotnet restore "src/JWTAuth.WebAPI/JWTAuth.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/JWTAuth.WebAPI"
RUN dotnet build "JWTAuth.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTAuth.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuth.WebAPI.dll"]