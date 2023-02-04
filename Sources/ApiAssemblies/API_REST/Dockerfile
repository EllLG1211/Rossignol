FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sources/ApiAssemblies/API_REST/API_REST.csproj", "ApiAssemblies/API_REST/"]
COPY ["Sources/Model/Model.csproj", "Model/"]
COPY ["Sources/ApiAssemblies/DTOs/DTOs.csproj", "ApiAssemblies/DTOs/"]
COPY ["Sources/Data/Data.csproj", "Data/"]
COPY ["Sources/EF_Local/EF_Local.csproj", "EF_Local/"]
COPY ["Sources/EF_Model/EF_Model.csproj", "EF_Model/"]
COPY ["Sources/EncryptionAssemblies/EncryptedModel/EncryptedModel.csproj", "EncryptionAssemblies/EncryptedModel/"]
COPY ["Sources/EncryptionAssemblies/AESEncryption/AESEncryption.csproj", "EncryptionAssemblies/AESEncryption/"]
COPY ["Sources/EncryptionAssemblies/Encryption/Encryption.csproj", "EncryptionAssemblies/Encryption/"]
RUN dotnet restore "ApiAssemblies/API_REST/API_REST.csproj"
COPY . .
WORKDIR "Sources/ApiAssemblies/API_REST"
RUN dotnet build "API_REST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_REST.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_REST.dll"]