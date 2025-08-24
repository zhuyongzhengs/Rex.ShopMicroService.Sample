using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 查询支付方式
    /// </summary>
    public class GetPaymentInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 支付类型名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}