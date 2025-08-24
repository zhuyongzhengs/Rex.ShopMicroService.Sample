# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["02.Gateways/Rex.Shop.WebPublicGateway/Rex.Shop.WebPublicGateway.csproj", "02.Gateways/Rex.Shop.WebPublicGateway/"]
COPY ["00.Core/Rex.Service.Permission/Rex.Service.Permission.csproj", "00.Core/Rex.Service.Permission/"]
RUN dotnet restore "./02.Gateways/Rex.Shop.WebPublicGateway/Rex.Shop.WebPublicGateway.csproj"
COPY . .
WORKDIR "/src/02.Gateways/Rex.Shop.WebPublicGateway"
RUN dotnet build "./Rex.Shop.WebPublicGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Rex.Shop.WebPublicGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rex.Shop.WebPublicGateway.dll"]