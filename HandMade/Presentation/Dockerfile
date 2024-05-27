FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Business/Business.csproj","src/"]
COPY ["./DataAccess/DataAccess.csproj","src/"]
COPY ["Presentation.csproj", "src/"]
RUN dotnet restore "src/DataAccess.csproj"
RUN dotnet restore "src/Business.csproj"
RUN dotnet restore "src/Presentation.csproj"

COPY . .
WORKDIR /src
RUN dotnet build "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet build "Business.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet build "DataAccess.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
