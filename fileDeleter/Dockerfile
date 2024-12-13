FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["fileDeleter/fileDeleter.csproj", "fileDeleter/"]
RUN dotnet restore "fileDeleter/fileDeleter.csproj"
COPY . .
WORKDIR "/src/fileDeleter"
RUN dotnet build "fileDeleter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fileDeleter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fileDeleter.dll"]
