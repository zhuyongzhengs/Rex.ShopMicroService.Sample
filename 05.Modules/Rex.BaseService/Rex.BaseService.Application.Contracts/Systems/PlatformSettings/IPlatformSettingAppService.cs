using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.PlatformSettings
{
    /// <summary>
    /// 平台设置服务接口
    /// </summary>
    public interface IPlatformSettingAppService : IApplicationService
    {
        /// <summary>
        /// 保存平台设置
        /// </summary>
        /// <param name="platformSetting">平台设置Dto</param>
        /// <returns></returns>
        Task UpdateAsync(PlatformSettingDto platformSetting);

        /// <summary>
        /// 获取平台设置
        /// </summary>
        /// <returns></returns>
        Task<PlatformSettingDto> GetAsync();
    }
}