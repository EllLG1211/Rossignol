FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sources/ApiAssemblies/API_WebSocket/API_WebSocket.csproj", "ApiAssemblies/API_WebSocket/"]
COPY ["Sources/Model/Model.csproj", "Model/"]
RUN dotnet restore "ApiAssemblies/API_WebSocket/API_WebSocket.csproj"
COPY . .
WORKDIR "/Sources/ApiAssemblies/API_WebSocket"
RUN dotnet build "API_WebSocket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_WebSocket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_WebSocket.dll"]