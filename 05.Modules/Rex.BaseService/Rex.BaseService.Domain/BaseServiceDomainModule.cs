using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.Abp.WeChat.Pay;
using EasyAbp.Abp.WeChat.Pay.Options;
using Rex.BaseService.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Rex.BaseService;

[DependsOn(
    typeof(BaseServiceDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(AbpWeChatMiniProgramModule),
    typeof(AbpWeChatPayModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpBlobStoringMinioModule)
)]
public class BaseServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("ar", "ar", "Arabic"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "Chinese (Simplified)"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "Chinese (Traditional)"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Czech"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (United Kingdom)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "French"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "German (Germany)"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi "));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Hungarian"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italian"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Portuguese (Brazil)"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Romanian (Romania)"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Russian"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("es", "es", "Spanish"));
            options.Languages.Add(new LanguageInfo("sv", "sv", "Swedish"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Turkish"));
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        // 配置微信小程序
        ConfigureWeChatMiniPrograms(context);

        // 配置微信支付
        ConfigureWeChatPays(context);
    }

    /// <summary>
    /// 配置[默认]微信支付
    /// </summary>
    /// <param name="context">配置服务上下文</param>
    private void ConfigureWeChatPays(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatPayOptions>(op =>
        {
            // 默认商户 Id
            op.MchId = "";
            // 微信支付的 API V3 密钥信息，会在后续进行 签名/加密/解密 时被使用。
            op.ApiV3Key = "";
            // 支付结果回调地址，用于接收支付结果通知。
            // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/notify 路由。
            op.NotifyUrl = "";

            // 如果需要支持退款操作，则以下配置必须

            // 退款结果回调地址，用于接收退款结果通知。
            // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/refund-notify 路由。
            op.RefundNotifyUrl = "";
            // 存放微信支付API证书的BLOB容器名，参考：https://docs.abp.io/en/abp/latest/Blob-Storing
            op.CertificateBlobContainerName = "rex.shop";
            // 存放微信支付API证书的BLOB名称
            op.CertificateBlobName = "apiclient_cert.p12";
            // 微信支付API证书秘钥，默认为商户名
            op.CertificateSecret = "";
        });
    }

    /// <summary>
    /// 配置[默认]微信小程序
    /// </summary>
    /// <param name="context">配置服务上下文</param>
    private void ConfigureWeChatMiniPrograms(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatMiniProgramOptions>(op =>
        {
            // 微信小程序分配的 AppId。
            op.AppId = "";
            // 微信小程序的唯一密钥。
            op.AppSecret = "";
            // 微信小程序所配置的 Token 和 EncodingAesKey 值。
            op.Token = "";
            op.EncodingAesKey = "";
        });
    }
}