using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using Rex.Service.Permission.AuthServices;
using Rex.Service.Permission.BaseServices;
using Rex.Service.Permission.GoodServices;
using Rex.Service.Permission.OrderServices;
using Rex.Service.Permission.PaymentServices;
using Rex.Service.Permission.PromotionServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.Uow;

namespace Rex.AuthService.OpenIddict;

/* Creates initial data that is needed to property run the application
 * and make client-to-server communication possible.
 */

public class OpenIddictDataSeedContributor : OpenIddictDataSeedContributorBase, IDataSeedContributor, ITransientDependency
{
    private readonly IConfiguration _configuration;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;
    private readonly IAbpApplicationManager _applicationManager;
    private readonly IOpenIddictScopeRepository _openIddictScopeRepository;
    private readonly IOpenIddictScopeManager _scopeManager;

    public OpenIddictDataSeedContributor(
        IConfiguration configuration,
        IOpenIddictApplicationRepository openIddictApplicationRepository,
        IAbpApplicationManager applicationManager,
        IOpenIddictScopeRepository openIddictScopeRepository,
        IOpenIddictScopeManager scopeManager)
        : base(configuration, openIddictApplicationRepository, applicationManager, openIddictScopeRepository, scopeManager)
    {
        _configuration = configuration;
        _openIddictApplicationRepository = openIddictApplicationRepository;
        _applicationManager = applicationManager;
        _openIddictScopeRepository = openIddictScopeRepository;
        _scopeManager = scopeManager;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        #region Scopes

        await CreateScopesAsync();

        #endregion Scopes

        #region Applications

        await CreateApplicationsAsync();

        #endregion Applications
    }

    /// <summary>
    /// 创建Scopes
    /// </summary>
    /// <returns></returns>
    private async Task CreateScopesAsync()
    {
        // 认证授权服务
        string authSysNameScope = AuthServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = authSysNameScope,
            DisplayName = $"{authSysNameScope} API 接口服务",
            Resources = { AuthServicePermissions.GroupName }
        });

        // Base服务
        string baseSysNameScope = BaseServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = baseSysNameScope,
            DisplayName = $"{baseSysNameScope} API 接口服务",
            Resources = { BaseServicePermissions.GroupName }
        });

        // 商品服务
        string goodSysNameScope = GoodServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = goodSysNameScope,
            DisplayName = $"{goodSysNameScope} API 接口服务",
            Resources = { GoodServicePermissions.GroupName }
        });

        // 促销服务
        string promotionSysNameScope = PromotionServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = promotionSysNameScope,
            DisplayName = $"{promotionSysNameScope} API 接口服务",
            Resources = { PromotionServicePermissions.GroupName }
        });

        // 订单服务
        string orderSysNameScope = OrderServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = orderSysNameScope,
            DisplayName = $"{orderSysNameScope} API 接口服务",
            Resources = { OrderServicePermissions.GroupName }
        });

        // 支付服务
        string paymentSysNameScope = PaymentServicePermissions.GroupName + "Scope";
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = paymentSysNameScope,
            DisplayName = $"{paymentSysNameScope} API 接口服务",
            Resources = { PaymentServicePermissions.GroupName }
        });
    }

    /// <summary>
    /// 创建Applications
    /// </summary>
    /// <returns></returns>
    private async Task CreateApplicationsAsync()
    {
        string authSysNameScope = AuthServicePermissions.GroupName + "Scope";
        string baseSysNameScope = BaseServicePermissions.GroupName + "Scope";
        string goodSysNameScope = GoodServicePermissions.GroupName + "Scope";
        string promotionSysNameScope = PromotionServicePermissions.GroupName + "Scope";
        string orderSysNameScope = OrderServicePermissions.GroupName + "Scope";
        string paymentSysNameScope = PaymentServicePermissions.GroupName + "Scope";
        string[] commonScopes = {
            OpenIddictConstants.Permissions.Scopes.Address,
            OpenIddictConstants.Permissions.Scopes.Email,
            OpenIddictConstants.Permissions.Scopes.Phone,
            OpenIddictConstants.Permissions.Scopes.Profile,
            OpenIddictConstants.Permissions.Scopes.Roles
        };

        var configurationSection = _configuration.GetSection("OpenIddict:Applications");

        #region 认证授权服务SwaggerClient

        var authSwaggerClientId = configurationSection["AuthService_Swagger:ClientId"];
        if (!authSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["AuthService_Swagger:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(authSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: authSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "AuthSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion 认证授权服务SwaggerClient

        #region Base服务SwaggerClient

        var baseSwaggerClientId = configurationSection["BaseService_Swagger:ClientId"];
        if (!baseSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["BaseService_Swagger:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(baseSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: baseSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "BaseSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion Base服务SwaggerClient

        #region 商品服务SwaggerClient

        var goodSwaggerClientId = configurationSection["GoodService_Swagger:ClientId"];
        if (!goodSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["GoodService_Swagger:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(goodSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: goodSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "GoodSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion 商品服务SwaggerClient

        #region 促销服务SwaggerClient

        var promotionSwaggerClientId = configurationSection["PromotionService_Swagger:ClientId"];
        if (!promotionSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["PromotionService_Swagger:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(promotionSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: promotionSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "PromotionSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion 促销服务SwaggerClient

        #region 订单服务SwaggerClient

        var orderSwaggerClientId = configurationSection["OrderService_Swagger:ClientId"];
        if (!orderSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["OrderService_Swagger:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(orderSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: orderSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "OrderSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion 订单服务SwaggerClient

        #region 支付服务SwaggerClient

        var paymentSwaggerClientId = configurationSection["PaymentService_Swagger:ClientId"];
        if (!paymentSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["PaymentService_Swagger:RootUrl"]?.TrimEnd('/');

            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(paymentSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: paymentSwaggerClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "PaymentSwagger Application",
                secret: null,
                grantTypes: new List<string> { OpenIddictConstants.GrantTypes.AuthorizationCode },
                scopes: authScopes,
                redirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                postLogoutRedirectUris: new List<string>() { $"{swaggerRootUrl}/swagger/oauth2-redirect.html" },
                clientUri: swaggerRootUrl
            );
        }

        #endregion 支付服务SwaggerClient

        #region Rex商城后台管理平台客户端

        var shopWebAdminServiceClientId = configurationSection["ShopWebAdminService:ClientId"];
        if (!shopWebAdminServiceClientId.IsNullOrWhiteSpace())
        {
            var rootUrl = configurationSection["ShopWebAdminService:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(authSysNameScope);
            authScopes.Add(baseSysNameScope);
            authScopes.Add(goodSysNameScope);
            authScopes.Add(promotionSysNameScope);
            authScopes.Add(orderSysNameScope);
            authScopes.Add(paymentSysNameScope);
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: shopWebAdminServiceClientId!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "Rex商城后台管理客户端",
                secret: null,
                grantTypes: new List<string> {
                    OpenIddictConstants.GrantTypes.Password,
                    OpenIddictConstants.GrantTypes.RefreshToken
                },
                scopes: authScopes,
                redirectUris: null,
                postLogoutRedirectUris: null,
                clientUri: rootUrl
            );
        }

        #endregion Rex商城后台管理平台客户端

        #region Rex商品微信小程序客户端

        var shopMiniProgramWechatCodeClient = configurationSection["ShopMiniProgramWechatCodeService:ClientId"];
        if (!shopMiniProgramWechatCodeClient.IsNullOrWhiteSpace())
        {
            var rootUrl = configurationSection["ShopMiniProgramWechatCodeService:RootUrl"]?.TrimEnd('/');
            List<string> authScopes = commonScopes.ToList();
            authScopes.Add(authSysNameScope);
            authScopes.Add(baseSysNameScope);
            authScopes.Add(goodSysNameScope);
            authScopes.Add(promotionSysNameScope);
            authScopes.Add(orderSysNameScope);
            authScopes.Add(paymentSysNameScope);

            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Web,
                name: shopMiniProgramWechatCodeClient!,
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "Rex商城微信小程序客户端",
                secret: null,
                grantTypes: new List<string> {
                    AuthServiceConsts.GrantTypes.WechatCode,
                    OpenIddictConstants.GrantTypes.RefreshToken
                },
                scopes: authScopes,
                redirectUris: null,
                postLogoutRedirectUris: null,
                clientUri: rootUrl
            );
        }

        #endregion Rex商品微信小程序客户端
    }
}