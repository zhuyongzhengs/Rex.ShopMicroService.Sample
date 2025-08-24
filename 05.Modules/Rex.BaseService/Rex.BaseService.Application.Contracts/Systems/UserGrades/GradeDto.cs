using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 等级Dto
    /// </summary>
    public class GradeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        //public bool IsDefault { get; set; }
    }
}