using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Rex.Service.AspNetCore.JsonSerializers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.Http;

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
                        RemoteServiceErrorInfo? errInfo = null;
                        if (exception is UserFriendlyException userFriendlyException)
                        {
                            errInfo = new RemoteServiceErrorInfo(userFriendlyException.Message, userFriendlyException.Details, userFriendlyException.Code, userFriendlyException.Data);
                        }
                        else if (exception is BusinessException businessException)
                        {
                            errInfo = new RemoteServiceErrorInfo(businessException.Message, businessException.Details, businessException.Code, businessException.Data);
                        }
                        else
                        {
                            errInfo = new RemoteServiceErrorInfo(exception.Message, null, defaultErrorCode, exception.Data);
                        }
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