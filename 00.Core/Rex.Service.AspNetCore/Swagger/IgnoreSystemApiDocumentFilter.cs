using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rex.Service.AspNetCore.Swagger
{
    /// <summary>
    /// (忽略系统API)SwaggerAPI文档过滤
    /// </summary>
    public class IgnoreSystemApiDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // 忽略的链接路径
            string[] ignoredPaths = new string[]{
                "/api/abp/api-definition",
                "/api/abp/api-application-localization",
                "/api/abp/application-configuration",
                "/api/abp/application-localization",
                "/api/abp/multi-tenancy/tenants/by-name/{name}",
                "/api/abp/multi-tenancy/tenants/by-id/{id}",
                "/api/account/register",
                "/api/account/send-password-reset-code",
                "/api/account/verify-password-reset-token",
                "/api/account/reset-password",
                "/api/account/dynamic-claims/refresh",
                "/api/setting-management/timezone",
                "/api/setting-management/timezone/timezones",
                "/api/identity/roles",
                "/api/identity/roles/all",
                "/api/identity/roles/{id}",
                "/api/identity/users",
                "/api/identity/users/assignable-roles",
                "/api/identity/users/by-email/{email}",
                "/api/identity/users/by-username/{userName}",
                "/api/identity/users/lookup/by-username/{userName}",
                "/api/identity/users/lookup/count",
                "/api/identity/users/lookup/search",
                "/api/identity/users/lookup/{id}",
                "/api/identity/users/{id}",
                "/api/identity/users/{id}/roles",
                "/api/multi-tenancy/tenants",
                "/api/multi-tenancy/tenants/{id}",
                "/api/multi-tenancy/tenants/{id}/default-connection-string",
                "/api/permission-management/permissions",
                "/api/setting-management/emailing",
                "/api/setting-management/emailing/send-test-email",
                "/api/account/my-profile",
                "/api/account/my-profile/change-password",
                "/api/feature-management/features",
            };
            foreach (var ignoredPath in ignoredPaths)
            {
                var operationToRemove = swaggerDoc.Paths.FirstOrDefault(p => p.Key.EndsWith(ignoredPath));
                if (operationToRemove.Key != null)
                {
                    swaggerDoc.Paths.Remove(operationToRemove.Key);
                }
            }
        }
    }
}