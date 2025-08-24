@echo off
:: 代码页更改为
chcp 65001

title Rex商城微服务启动

@echo.
@echo 正在启动【认证授权服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.AuthService/src/Rex.AuthService.HttpApi.Host && dotnet run"
:: 等待【认证授权服务】启动完成

timeout /t 1 > nul
set "authLogDirectory=04.MicroServices/Rex.AuthService/src/Rex.AuthService.HttpApi.Host/Logs"
if not exist "%authLogDirectory%" (
    mkdir "%authLogDirectory%"
)
cd ./04.MicroServices/Rex.AuthService/src/Rex.AuthService.HttpApi.Host/Logs
echo. > logs.log
:StartAuthService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartAuthService
)
@echo [认证授权服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【Base服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host && dotnet run"
:: 等待【Base服务】启动完成

timeout /t 1 > nul
set "baseLogDirectory=04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host/Logs"
if not exist "%baseLogDirectory%" (
    mkdir "%baseLogDirectory%"
)
cd ./04.MicroServices/Rex.BaseService/src/Rex.BaseService.HttpApi.Host/Logs
echo. > logs.log
:StartBaseService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartBaseService
)
@echo [Base服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【商品服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.GoodService/src/Rex.GoodService.HttpApi.Host && dotnet run"
:: 等待【商品服务】启动完成

timeout /t 1 > nul
set "goodLogDirectory=04.MicroServices/Rex.GoodService/src/Rex.GoodService.HttpApi.Host/Logs"
if not exist "%goodLogDirectory%" (
    mkdir "%goodLogDirectory%"
)
cd ./04.MicroServices/Rex.GoodService/src/Rex.GoodService.HttpApi.Host/Logs
echo. > logs.log
:StartGoodService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartGoodService
)
@echo [商品服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【订单服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.OrderService/src/Rex.OrderService.HttpApi.Host && dotnet run"
:: 等待【订单服务】启动完成

timeout /t 1 > nul
set "orderLogDirectory=04.MicroServices/Rex.OrderService/src/Rex.OrderService.HttpApi.Host/Logs"
if not exist "%orderLogDirectory%" (
    mkdir "%orderLogDirectory%"
)
cd ./04.MicroServices/Rex.OrderService/src/Rex.OrderService.HttpApi.Host/Logs
echo. > logs.log
:OrderService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto OrderService
)
@echo [订单服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【促销服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.PromotionService/src/Rex.PromotionService.HttpApi.Host && dotnet run"
:: 等待【促销服务】启动完成

timeout /t 1 > nul
set "promotionLogDirectory=04.MicroServices/Rex.PromotionService/src/Rex.PromotionService.HttpApi.Host/Logs"
if not exist "%promotionLogDirectory%" (
    mkdir "%promotionLogDirectory%"
)
cd ./04.MicroServices/Rex.PromotionService/src/Rex.PromotionService.HttpApi.Host/Logs
echo. > logs.log
:PromotionService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto PromotionService
)
@echo [促销服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【支付服务】，请稍等！
start cmd /k "cd ./04.MicroServices/Rex.PaymentService/src/Rex.PaymentService.HttpApi.Host && dotnet run"
:: 等待【支付服务】启动完成

timeout /t 1 > nul
set "paymentDirectory=04.MicroServices/Rex.PaymentService/src/Rex.PaymentService.HttpApi.Host/Logs"
if not exist "%paymentDirectory%" (
    mkdir "%paymentDirectory%"
)
cd ./04.MicroServices/Rex.PaymentService/src/Rex.PaymentService.HttpApi.Host/Logs
echo. > logs.log
:PaymentService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto PaymentService
)
@echo [支付服务]启动成功.

:: 回到原路径
cd ../../../../../

@echo.
@echo 正在启动【后台聚合服务】，请稍等！
start cmd /k "cd ./03.AggregationServices/Rex.BackendAggregationService && dotnet run"
:: 等待【后台聚合服务】启动完成

timeout /t 1 > nul
set "backendAggregationLogDirectory=03.AggregationServices/Rex.BackendAggregationService/Logs"
if not exist "%backendAggregationLogDirectory%" (
    mkdir "%backendAggregationLogDirectory%"
)
cd ./03.AggregationServices/Rex.BackendAggregationService/Logs
echo. > logs.log
:StartBackendAggregationService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartBackendAggregationService
)
@echo [后台聚合服务]启动成功.

:: 回到原路径
cd ../../../

@echo.
@echo 正在启动【前台聚合服务】，请稍等！
start cmd /k "cd ./03.AggregationServices/Rex.FrontAggregationService && dotnet run"
:: 等待【前台聚合服务】启动完成

timeout /t 1 > nul
set "frontAggregationLogDirectory=03.AggregationServices/Rex.FrontAggregationService/Logs"
if not exist "%frontAggregationLogDirectory%" (
    mkdir "%frontAggregationLogDirectory%"
)
cd ./03.AggregationServices/Rex.FrontAggregationService/Logs
echo. > logs.log
:StartFrontAggregationService
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartFrontAggregationService
)
@echo [前台聚合服务]启动成功.

:: 回到原路径
cd ../../../

@echo.
@echo 正在启动【后台网关服务】，请稍等！
start cmd /k "cd ./02.Gateways/Rex.Shop.WebGateway && dotnet run"
:: 等待【后台网关服务】启动完成

timeout /t 1 > nul
set "backendApiLogDirectory=02.Gateways/Rex.Shop.WebGateway/Logs"
if not exist "%backendApiLogDirectory%" (
    mkdir "%backendApiLogDirectory%"
)
cd ./02.Gateways/Rex.Shop.WebGateway/Logs
echo. > logs.log
:StartBackendApiGateway
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartBackendApiGateway
)
@echo [后台网关服务]启动成功.

:: 回到原路径
cd ../../../

@echo.
@echo 正在启动【外部网关服务】，请稍等！
start cmd /k "cd ./02.Gateways/Rex.Shop.WebPublicGateway && dotnet run"
:: 等待【外部网关服务】启动完成

timeout /t 1 > nul
set "frontApiLogDirectory=02.Gateways/Rex.Shop.WebPublicGateway/Logs"
if not exist "%frontApiLogDirectory%" (
    mkdir "%frontApiLogDirectory%"
)
cd ./02.Gateways/Rex.Shop.WebPublicGateway/Logs
echo. > logs.log
:StartFrontApiGateway
type logs.log | find /i "Application started." > nul
if errorlevel 1 (
  goto StartFrontApiGateway
)
@echo [外部网关服务]启动成功.

:: 回到原路径
cd ../../../

@echo.
@echo 所有服务已启动完成！
echo. & pause