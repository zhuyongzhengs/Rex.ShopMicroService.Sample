using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rex.Service.AspNetCore.JsonSerializers;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Logging;

namespace Rex.Service.AspNetCore.Extensions
{
    /// <summary>
    /// AspNetCore扩展处理
    /// </summary>
    public static class AspNetCoreExtension
    {
        /// <summary>
        /// 路由错误响应
        /// </summary>
        /// <param name="app"></param>
        public static void UseRemoteErrorResponse(this IApplicationBuilder app, string defaultErrorCode)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    if (exception != null)
                    {
                        context.Response.ContentType = "application/json";

                        // --- 1: 获取 ILogger 和 IOptions ---
                        var logger = context.RequestServices.GetRequiredService<ILogger<IApplicationBuilder>>();
                        var statusCodeOptions = context.RequestServices.GetRequiredService<IOptions<AbpExceptionHttpStatusCodeOptions>>().Value;

                        RemoteServiceErrorInfo? errInfo = null;
                        int statusCode = StatusCodes.Status500InternalServerError; // 默认为 500

                        if (exception is IHasErrorCode errorCodeException)
                        {
                            // --- 2: 映射状态码 ---
                            if (errorCodeException.Code != null && statusCodeOptions.ErrorCodeToHttpStatusCodeMappings.ContainsKey(errorCodeException.Code))
                            {
                                statusCode = (int)statusCodeOptions.ErrorCodeToHttpStatusCodeMappings[errorCodeException.Code];
                            }
                            // 如果是业务异常，且没有映射，通常也可以设为 403 或 400
                            else if (exception is UserFriendlyException)
                            {
                                statusCode = StatusCodes.Status403Forbidden;
                            }

                            errInfo = new RemoteServiceErrorInfo(
                                exception.Message,
                                (exception as IHasErrorDetails)?.Details,
                                errorCodeException.Code,
                                exception.Data);
                        }
                        else
                        {
                            errInfo = new RemoteServiceErrorInfo(exception.Message, null, defaultErrorCode, exception.Data);
                        }

                        // --- 3: 根据异常级别记录日志 ---
                        var logLevel = (exception as IHasLogLevel)?.LogLevel ?? LogLevel.Error;
                        logger.Log(logLevel, exception, "Route Error: {Message}", exception.Message);

                        // --- 4: 设置状态码 ---
                        context.Response.StatusCode = statusCode;

                        RemoteServiceErrorResponse errResponse = new RemoteServiceErrorResponse(errInfo);
                        var exceptionJson = JsonSerializer.Serialize(errResponse, options: GetJsonSerializerOptions());
                        await context.Response.WriteAsync(exceptionJson, Encoding.UTF8);
                    }
                });
            });
        }

        /// <summary>
        /// 获取请求IP地址
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(this HttpContext httpContext)
        {
            string ip = "127.0.0.1";
            if (httpContext != null && httpContext.Connection.RemoteIpAddress != null)
            {
                ip = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            return ip;
        }

        /// <summary>
        /// 获取默认的Json序列化配置
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            // 定义Json序列化配置
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeFormat("yyyy-MM-dd HH:mm:ss"));
            return jsonSerializerOptions;
        }
    }
}