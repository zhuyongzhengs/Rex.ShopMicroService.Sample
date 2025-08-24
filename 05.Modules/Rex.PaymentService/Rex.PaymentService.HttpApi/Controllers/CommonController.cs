using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rex.PaymentService.Controllers
{
    /// <summary>
    /// 公共控制器
    /// </summary>
    [Route("api/payment/common")]
    public class CommonController : PaymentServiceController
    {
        private readonly IWeChatPayEventRequestHandlingService _eventRequestHandlingService;

        public CommonController(IWeChatPayEventRequestHandlingService eventRequestHandlingService)
        {
            _eventRequestHandlingService = eventRequestHandlingService;
        }

        /// <summary>
        /// 微信支付回调接口
        /// </summary>
        /// <param name="mchId">商户ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("wechat/notify")]
        public virtual async Task<IActionResult> WechatNotifyAsync([CanBeNull][FromQuery] string mchId)
        {
            WeChatRequestHandlingResult requestHandlingResult = await _eventRequestHandlingService.PaidNotifyAsync(BuildNotifyInputDto(await GetPostDataAsync(), mchId));
            return !requestHandlingResult.Success ? NotifyFailure(requestHandlingResult.FailureReason) : NotifySuccess();
        }

        /// <summary>
        /// 微信退款回调接口
        /// </summary>
        /// <param name="mchId">商户ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("wechat/refundNotify")]
        public virtual async Task<IActionResult> WechatRefundNotifyAsync([CanBeNull][FromQuery] string mchId)
        {
            WeChatRequestHandlingResult requestHandlingResult = await _eventRequestHandlingService.RefundNotifyAsync(BuildNotifyInputDto(await GetPostDataAsync(), mchId));
            return !requestHandlingResult.Success ? NotifyFailure(requestHandlingResult.FailureReason) : NotifySuccess();
        }

        /// <summary>
        /// 通知成功
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public IActionResult NotifySuccess()
        {
            return Ok(new WeChatPayNotificationOutput
            {
                Code = "SUCCESS"
            });
        }

        /// <summary>
        /// 通知失败
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        [NonAction]
        public IActionResult NotifyFailure(string message)
        {
            return BadRequest(new WeChatPayNotificationOutput
            {
                Code = "FAIL",
                Message = message
            });
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetPostDataAsync()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, 1024, leaveOpen: true);
            string requestBody = await reader.ReadToEndAsync(); ;
            return requestBody.IsNullOrWhiteSpace() ? string.Empty : requestBody;
        }

        /// <summary>
        /// 生成通知Dto
        /// </summary>
        /// <param name="requestBody">请求正文</param>
        /// <param name="mchId"></param>
        /// <returns></returns>
        private NotifyInputDto BuildNotifyInputDto(string requestBody, string mchId)
        {
            return new NotifyInputDto
            {
                MchId = mchId,
                RequestBodyString = requestBody,
                RequestBody = JsonSerializer.Deserialize<WeChatPayNotificationInput>(requestBody),
                HttpHeader = new NotifyHttpHeaderModel(Request.Headers["Wechatpay-Serial"],
                    Request.Headers["Wechatpay-TimeStamp"],
                    Request.Headers["Wechatpay-Nonce"],
                    Request.Headers["Wechatpay-Signature"])
            };
        }
    }
}