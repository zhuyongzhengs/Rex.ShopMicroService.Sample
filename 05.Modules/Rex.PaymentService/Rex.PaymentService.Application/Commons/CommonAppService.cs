using EasyAbp.Abp.WeChat.MiniProgram.Settings;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Settings;
using Microsoft.AspNetCore.Authorization;
using Rex.PaymentService.Payments;
using Rex.Service.Core.Configurations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using static Rex.Service.Core.Configurations.GlobalEnums;
using CreateOrderRequest = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models.CreateOrderRequest;

namespace Rex.PaymentService.Commons
{
    /// <summary>
    /// 公共服务
    /// </summary>
    [RemoteService]
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAbpWeChatPayServiceFactory _weChatPayServiceFactory;
        private readonly IWeChatPayClientRequestHandlingService _weChatPayClientRequestHandlingService;
        private readonly ISettingManager _settingManager;
        private readonly ISettingProvider _settingProvider;

        public CommonAppService(IPaymentRepository paymentRepository, IBillPaymentRepository billPaymentRepository, IAbpWeChatPayServiceFactory weChatPayServiceFactory, IWeChatPayClientRequestHandlingService weChatPayClientRequestHandlingService, ISettingManager settingManager, ISettingProvider settingProvider)
        {
            _billPaymentRepository = billPaymentRepository;
            _paymentRepository = paymentRepository;
            _weChatPayServiceFactory = weChatPayServiceFactory;
            _weChatPayClientRequestHandlingService = weChatPayClientRequestHandlingService;
            _settingManager = settingManager;
            _settingProvider = settingProvider;
        }

        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        [Authorize]
        public async Task UpdateSettingCurrentTenantAsync(string name, string? value)
        {
            await _settingManager.SetForCurrentTenantAsync(name, value);
        }

        /// <summary>
        /// 获取启用的支付方式
        /// </summary>
        /// <param name="code">支付编码</param>
        /// <returns></returns>
        [Authorize]
        public async Task<PaymentDto> GetPaymentEnableByCodeAsync(string code)
        {
            Payment payment = await _paymentRepository.FindAsync(x => x.Code.Equals(code) && x.IsEnable);
            return ObjectMapper.Map<Payment, PaymentDto>(payment);
        }

        /// <summary>
        /// 创建支付单
        /// </summary>
        /// <param name="input">支付单Dto</param>
        /// <returns></returns>
        [Authorize]
        public async Task<BillPaymentDto> CreateBillPaymentAsync(BillPaymentCreateDto input)
        {
            // 验证支付编码是否启用
            PaymentDto payment = await GetPaymentEnableByCodeAsync(input.PaymentCode);
            if (payment == null) throw new UserFriendlyException("此支付方式未启用！", SystemStatusCode.Fail.ToPaymentServicePrefix()).WithData(nameof(input.PaymentCode), input.PaymentCode);

            // 创建支付单信息
            BillPayment billPayment = ObjectMapper.Map<BillPaymentCreateDto, BillPayment>(input);
            await _billPaymentRepository.InsertAsync(billPayment);
            return ObjectMapper.Map<BillPayment, BillPaymentDto>(billPayment);
        }

        /// <summary>
        /// 更新支付单状态
        /// </summary>
        /// <param name="no">支付单号</param>
        /// <param name="input">支付单状态</param>
        /// <returns></returns>
        [Authorize]
        public async Task<BillPaymentDto> UpdateBillPaymentStatusAsync(string no, BillPaymentStatusUpdateDto input)
        {
            // 更新支付单
            BillPayment billPayment = await _billPaymentRepository.FindAsync(x => x.No == no && x.Money == input.Money && x.Status != (int)OrderPayStatus.Yes);
            if (billPayment == null) throw new UserFriendlyException("没有找到此未支付的支付单号！", SystemStatusCode.Fail.ToPaymentServicePrefix()).WithData(nameof(billPayment.No), no);
            billPayment.Status = input.Status;
            billPayment.PaymentCode = input.PaymentCode;
            billPayment.PayedMsg = input.PayedMsg;
            billPayment.TradeNo = input.TradeNo;
            billPayment = await _billPaymentRepository.UpdateAsync(billPayment);
            return ObjectMapper.Map<BillPayment, BillPaymentDto>(billPayment);
        }

        /// <summary>
        /// 发起微信[小程序]支付
        /// </summary>
        /// <param name="openId">微信用户OpenID</param>
        /// <param name="billPayment">支付单</param>
        /// <returns></returns>
        [Authorize]
        public async Task<WeChatPayParameterResultDto> PubWxmpPayAsync(string openId, BillPaymentDto billPayment)
        {
            JsPaymentService jsPaymentService = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
            string? appId = await _settingProvider.GetOrNullAsync(AbpWeChatMiniProgramSettings.AppId);
            string? notifyUrl = await _settingProvider.GetOrNullAsync(AbpWeChatPaySettings.NotifyUrl);

            // 创建订单
            CreateOrderResponse createOrderResponse = await jsPaymentService.CreateOrderAsync(new CreateOrderRequest
            {
                MchId = jsPaymentService.MchId,
                OutTradeNo = billPayment.No,
                NotifyUrl = notifyUrl,
                AppId = appId,
                Description = $"支付订单号：{billPayment.No}",
                Amount = new CreateOrderAmountModel
                {
                    Total = Convert.ToInt32(billPayment.Money * 100),
                    // Total = 1, // 测试金额：0.1元
                    Currency = "CNY"
                },
                Payer = new CreateOrderRequest.CreateOrderPayerModel
                {
                    OpenId = openId
                }
            });

            // 获取JS-SDK支付[调用]参数
            GetJsSdkWeChatPayParametersResult sdkResponse = await _weChatPayClientRequestHandlingService.GetJsSdkWeChatPayParametersAsync(new GetJsSdkWeChatPayParametersInput
            {
                MchId = jsPaymentService.MchId,
                PrepayId = createOrderResponse.PrepayId,
                AppId = appId
            });
            if (!sdkResponse.Success) throw new UserFriendlyException(sdkResponse.FailureReason, SystemStatusCode.Fail.ToPaymentServicePrefix());

            WeChatPayParameterResultDto weChatPayParameterResult = new WeChatPayParameterResultDto();
            weChatPayParameterResult.Detail = ObjectMapper.Map<BillPaymentDto, BillPaymentDetailDto>(billPayment);
            weChatPayParameterResult.PayParameter = ObjectMapper.Map<GetJsSdkWeChatPayParametersResult, JsSdkWeChatPayParameterDto>(sdkResponse);
            return weChatPayParameterResult;
        }
    }
}