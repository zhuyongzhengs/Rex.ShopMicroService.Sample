using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 查询注册用户
    /// </summary>
    public class GetSysUserInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 用户等级ID
        /// </summary>
        public Guid? GradeId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}