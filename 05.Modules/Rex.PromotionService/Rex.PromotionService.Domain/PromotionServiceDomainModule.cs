using Rex.PromotionService.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Rex.PromotionService;

[DependsOn(
    typeof(PromotionServiceDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(AbpCachingStackExchangeRedisModule)
)]
public class PromotionServiceDomainModule : AbpModule
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
    }
}