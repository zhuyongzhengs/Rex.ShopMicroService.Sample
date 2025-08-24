using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Commons
{
    /// <summary>
    /// 微信支付参数Dto
    /// </summary>
    public class JsSdkWeChatPayParameterDto : EntityDto
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 时间戳（单位：秒）
        /// </summary>
        public long TimeStamp { get; set; }

        /// <summary>
        /// 订单详情扩展字符串
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string PaySign { get; set; }
    }
}