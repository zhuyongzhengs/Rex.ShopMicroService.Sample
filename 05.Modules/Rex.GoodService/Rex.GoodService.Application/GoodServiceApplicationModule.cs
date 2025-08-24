using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Yitter.IdGenerator;

namespace Rex.GoodService;

[DependsOn(
    typeof(GoodServiceDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(GoodServiceApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundWorkersQuartzModule)
    )]
public class GoodServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<GoodServiceApplicationModule>();
        });

        #region 雪花漂移算法

        // 创建 IdGeneratorOptions 对象，请在构造函数中输入 WorkerId：
        var options = new IdGeneratorOptions(30);
        // 保存参数（必须的操作，否则以上设置都不能生效）：
        YitIdHelper.SetIdGenerator(options);

        #endregion 雪花漂移算法

        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
}