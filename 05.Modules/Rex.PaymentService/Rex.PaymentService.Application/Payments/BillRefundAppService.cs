using DotNetCore.CAP;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Settings;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Bases;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Settings;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单服务接口
    /// </summary>
    [RemoteService]
    public class BillRefundAppService : ApplicationService, IBillRefundAppService
    {
        private readonly IBillRefundRepository _billRefundRepository;
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAbpWeChatPayServiceFactory _weChatPayServiceFactory;
        private readonly ISettingProvider _settingProvider;
        private readonly ICapPublisher _capEventBus;

        public BillRefundAppService(IBillRefundRepository billRefundRepository, IBillPaymentRepository billPaymentRepository, IPaymentRepository paymentRepository, IAbpWeChatPayServiceFactory weChatPayServiceFactory, ISettingProvider settingProvider, ICapPublisher capEventBus)
        {
            _billRefundRepository = billRefundRepository;
            _billPaymentRepository = billPaymentRepository;
            _paymentRepository = paymentRepository;
            _weChatPayServiceFactory = weChatPayServiceFactory;
            _settingProvider = settingProvider;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 获取(分页)退款单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BillRefundDto>> GetListAsync(GetBillRefundInput input)
        {
            // 查询数量
            long totalCount = (await _billRefundRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No.Equals(input.No))
                .WhereIf(!input.PaymentCode.IsNullOrWhiteSpace(), p => p.PaymentCode.Equals(input.PaymentCode))
                .WhereIf(input.AftersalesId.HasValue, p => p.BillAftersalesId == input.AftersalesId)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<BillRefundDto>();
            }
            List<BillRefund> billRefundList = (await _billRefundRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No.Equals(input.No))
                .WhereIf(!input.PaymentCode.IsNullOrWhiteSpace(), p => p.PaymentCode.Equals(input.PaymentCode))
                .WhereIf(input.AftersalesId.HasValue, p => p.BillAftersalesId == input.AftersalesId)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .OrderByIf<BillRefund, IQueryable<BillRefund>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();
            return new PagedResultDto<BillRefundDto>(
                totalCount,
                ObjectMapper.Map<List<BillRefund>, List<BillRefundDto>>(billRefundList)
            );
        }

        /// <summary>
        /// 根据[售后单]获取退款单
        /// </summary>
        /// <param name="aftersalesId">售后ID</param>
        /// <returns></returns>
        public async Task<BillRefundDto> GetBillRefundByAftersalesIdAsync(Guid aftersalesId)
        {
            BillRefund billRefund = (await _billRefundRepository.GetQueryableAsync())
                .Where(x => x.BillAftersalesId == aftersalesId)
                .FirstOrDefault();
            if (billRefund == null) return null;
            return ObjectMapper.Map<BillRefund, BillRefundDto>(billRefund);
        }

        /// <summary>
        /// 根据[资源编号]获取退款单
        /// </summary>
        /// <param name="sourceId">资源编号</param>
        /// <returns></returns>
        public async Task<List<BillRefundDto>> GetBillRefundBySourceIdAsync(string sourceId)
        {
            List<BillRefund> billRefunds = await _billRefundRepository.GetListAsync(x => x.SourceId.Contains(sourceId));
            return ObjectMapper.Map<List<BillRefund>, List<BillRefundDto>>(billRefunds);
        }

        /// <summary>
        /// 查询退款单类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetBillRefundTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillRefundType>());
        }

        /// <summary>
        /// 查询退款单状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetBillRefundStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillRefundStatus>());
        }

        /// <summary>
        /// 审核退款单
        /// </summary>
        /// <param name="id">退款单ID</param>
        /// <param name="input">退款参数</param>
        /// <returns></returns>
        public async Task UpdateAuditAsync(Guid id, AuditRefundDto input)
        {
            if (input.Status != (int)GlobalEnums.BillRefundStatus.Refund && input.Status != (int)GlobalEnums.BillRefundStatus.Refuse)
                throw new UserFriendlyException("退款状态错误，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

            BillRefund billRefund = await _billRefundRepository.GetAsync(x => x.Id == id && x.Status == (int)GlobalEnums.BillRefundStatus.Norefund);
            if (billRefund == null)
                throw new UserFriendlyException("退款单不存在(或已被处理)，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

            if (input.Status == (int)GlobalEnums.BillRefundStatus.Refund)
            {
                // 表示同意退款
                if (input.PaymentCode.Equals("Offline", StringComparison.OrdinalIgnoreCase))
                {
                    // 线下(支付)退款，只更改状态不做实际退款
                    billRefund.Status = input.Status;
                    billRefund.PaymentCode = input.PaymentCode;
                    billRefund.Memo = "退款成功。";
                }
                else
                {
                    // 线上(支付宝、微信、余额)退款，需要调用支付接口退款
                    await UpdatePaymentRefundAsync(id);
                }
            }
            else if (input.Status == (int)GlobalEnums.BillRefundStatus.Refuse)
            {
                // 表示拒绝退款
                billRefund.Status = input.Status;
                billRefund.PaymentCode = input.PaymentCode;
                billRefund.Memo = "退款单已拒绝退款！";
            }
        }

        /// <summary>
        /// 支付退款
        /// </summary>
        /// <param name="id">退款单ID</param>
        /// <returns></returns>
        public async Task UpdatePaymentRefundAsync(Guid id)
        {
            BillRefund billRefund = await _billRefundRepository.GetAsync(x => x.Id == id && x.Status != (int)GlobalEnums.BillRefundStatus.Refund);
            if (billRefund == null)
                throw new UserFriendlyException("退款单不存在(或该退款单已退款)，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

            BillPayment bPayment = (await _billPaymentRepository.GetQueryableAsync())
                .Where(x => x.SourceId.Equals(billRefund.SourceId) && x.Type == billRefund.Type && x.Status == (int)GlobalEnums.BillPaymentStatus.Payed).FirstOrDefault();
            if (bPayment == null)
                throw new UserFriendlyException("没有找到支付成功的支付单，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

            Payment payment = (await _paymentRepository.GetQueryableAsync()).Where(x => x.Code.Equals(bPayment.PaymentCode) && x.IsEnable).FirstOrDefault();
            if (payment == null)
                throw new UserFriendlyException($"没有找到支付方式(或已被禁用)：{bPayment.PaymentCode}，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

            // 调用支付接口退款
            string memo = string.Empty;
            if (bPayment.PaymentCode.Equals(GlobalEnums.PaymentType.WechatPay.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                string? refundNotifyUrl = await _settingProvider.GetOrNullAsync(AbpWeChatPaySettings.RefundNotifyUrl);
                if (refundNotifyUrl.IsNullOrWhiteSpace())
                    throw new UserFriendlyException($"微信支付退款通知地址未设置，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix());

                // 微信退款
                JsPaymentService jsPaymentService = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
                RefundOrderRequest refundOrderReq = new RefundOrderRequest
                {
                    OutRefundNo = billRefund.No,
                    OutTradeNo = bPayment.No,
                    NotifyUrl = refundNotifyUrl,
                    Amount = new RefundOrderRequest.AmountInfo
                    {
                        Total = Convert.ToInt32(bPayment.Money * 100),
                        Refund = Convert.ToInt32(billRefund.Money * 100),

                        // 测试金额：0.1元
                        //Refund = 1,
                        //Total = 1,
                        Currency = "CNY"
                    }
                };
                RefundOrderResponse refundOrderRes = await jsPaymentService.RefundAsync(refundOrderReq);
                string[] okStatus = { "PROCESSING", "SUCCESS" };
                bool refundResult = okStatus.Contains(refundOrderRes.Status);
                if (!refundResult)
                    throw new UserFriendlyException($"[微信支付]退款失败：{refundOrderRes.Message}。", SystemStatusCode.Fail.ToPaymentServicePrefix());
                memo = $"{refundOrderRes.Status}：{FormatWechatRefundStatus(refundOrderRes.Status)}。";
            }
            else if (bPayment.PaymentCode.Equals(GlobalEnums.PaymentType.AliPay.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // 支付宝退款
                throw new UserFriendlyException($"支付宝退款尚未开通，请选择其它退款方式！", SystemStatusCode.Fail.ToPaymentServicePrefix());
            }
            else if (bPayment.PaymentCode.Equals(GlobalEnums.PaymentType.BalancePay.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // 余额退款
                await _capEventBus.PublishAsync(ChangeUserBalanceEto.EventName, new ChangeUserBalanceEto()
                {
                    UserId = bPayment.UserId,
                    Type = (int)GlobalEnums.UserBalanceType.Refund,
                    Money = billRefund.Money,
                    SourceId = bPayment.Id.ToString()
                });
                memo = "退款成功。";
            }

            // 更改退款单状态
            billRefund.Status = (int)GlobalEnums.BillRefundStatus.Refund;
            billRefund.Memo = memo;
        }

        /// <summary>
        /// 格式化微信退款状态
        /// </summary>
        /// <param name="status">退款状态</param>
        /// <returns></returns>
        private string FormatWechatRefundStatus(string status)
        {
            string memo = string.Empty;
            switch (status)
            {
                case "PROCESSING":
                    memo = "退款处理中";
                    break;

                case "SUCCESS":
                    memo = "退款成功";
                    break;

                case "CLOSED":
                    memo = "退款关闭";
                    break;

                case "ABNORMAL":
                    memo = "退款异常";
                    break;

                default:
                    break;
            }
            return memo;
        }
    }
}