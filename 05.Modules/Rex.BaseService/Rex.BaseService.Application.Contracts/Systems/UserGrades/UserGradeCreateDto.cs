using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 创建用户等级Dto
    /// </summary>
    public class UserGradeCreateDto : EntityDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}