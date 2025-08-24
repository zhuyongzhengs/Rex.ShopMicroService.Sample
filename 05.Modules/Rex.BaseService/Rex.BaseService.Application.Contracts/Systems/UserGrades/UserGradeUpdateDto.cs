using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 修改用户等级Dto
    /// </summary>
    public class UserGradeUpdateDto : EntityDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}