using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 修改支付方式Dto
    /// </summary>
    public class PaymentUpdateDto : EntityDto
    {
        /// <summary>
        /// 支付类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否线上支付
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 方式描述
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}
