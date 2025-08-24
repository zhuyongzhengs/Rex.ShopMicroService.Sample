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
COPY ["04.MicroServices/Rex.BaseService/NuGet.Config", "04.MicroServices/Rex.BaseService/"]
COPY ["04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host/Rex.BaseService.HttpApi.Host.csproj", "04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.Application/Rex.BaseService.Application.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.Application/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.Domain/Rex.BaseService.Domain.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.Domain/"]
COPY ["00.Core/Rex.Service.AspNetCore/Rex.Service.AspNetCore.csproj", "00.Core/Rex.Service.AspNetCore/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.Domain.Shared/Rex.BaseService.Domain.Shared.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.Domain.Shared/"]
COPY ["00.Core/Rex.Service.Core/Rex.Service.Core/Rex.Service.Core.csproj", "00.Core/Rex.Service.Core/Rex.Service.Core/"]
COPY ["00.Core/Rex.Service.Permission/Rex.Service.Permission.csproj", "00.Core/Rex.Service.Permission/"]
COPY ["00.Core/Rex.Service.Setting/Rex.Service.Setting.csproj", "00.Core/Rex.Service.Setting/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.Application.Contracts/Rex.BaseService.Application.Contracts.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.Application.Contracts/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.EntityFrameworkCore/Rex.BaseService.EntityFrameworkCore.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.EntityFrameworkCore/"]
COPY ["05.Modules/Rex.BaseService/Rex.BaseService.HttpApi/Rex.BaseService.HttpApi.csproj", "05.Modules/Rex.BaseService/Rex.BaseService.HttpApi/"]
RUN dotnet restore "./04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host/Rex.BaseService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host"
RUN dotnet build "./Rex.BaseService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Rex.BaseService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rex.BaseService.HttpApi.Host.dll"]