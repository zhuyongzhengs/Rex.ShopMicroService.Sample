using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 查询选择角色参数
    /// </summary>
    public class GetSelectRoleInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 包括详细信息
        /// </summary>
        public bool IncludeDetails { get; set; }
    }
}