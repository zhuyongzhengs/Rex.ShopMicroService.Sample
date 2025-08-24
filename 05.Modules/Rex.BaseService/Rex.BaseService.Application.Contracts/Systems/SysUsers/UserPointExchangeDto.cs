using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户可用积分Dto
    /// </summary>
    public class UserPointExchangeDto : EntityDto
    {
        /// <summary>
        /// 是否使用积分
        /// </summary>
        public bool IsUsePoint { get; set; }

        /// <summary>
        /// 用户总积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 可以使用的积分
        /// </summary>
        public int CanUsePoint { get; set; }

        /// <summary>
        /// 积分抵扣的金额
        /// </summary>
        public decimal Money { get; set; }
    }
}