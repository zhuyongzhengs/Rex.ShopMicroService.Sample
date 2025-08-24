using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 创建店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkCreateDto
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}