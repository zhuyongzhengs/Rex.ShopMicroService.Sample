using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Yitter.IdGenerator;

namespace Rex.AuthService;

[DependsOn(
    typeof(AuthServiceDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(AuthServiceApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AuthServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        #region 雪花漂移算法

        // 创建 IdGeneratorOptions 对象，请在构造函数中输入 WorkerId：
        var options = new IdGeneratorOptions(10);
        // 保存参数（必须的操作，否则以上设置都不能生效）：
        YitIdHelper.SetIdGenerator(options);

        #endregion 雪花漂移算法
    }
}