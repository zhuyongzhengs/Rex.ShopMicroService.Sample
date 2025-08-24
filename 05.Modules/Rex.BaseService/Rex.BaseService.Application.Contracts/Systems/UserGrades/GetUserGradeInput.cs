using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 查询用户等级
    /// </summary>
    public class GetUserGradeInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }
    }
}