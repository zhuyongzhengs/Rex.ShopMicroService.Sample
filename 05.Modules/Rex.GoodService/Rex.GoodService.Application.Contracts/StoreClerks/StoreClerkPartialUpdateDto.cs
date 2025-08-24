using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 修改店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkUpdateDto
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}