using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 更新物流退货单Dto
    /// </summary>
    public class BillReshipLogiUpdateDto : EntityDto
    {
        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string LogiCode { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string LogiNo { get; set; }
    }
}