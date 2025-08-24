using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式服务接口
    /// </summary>
    [RemoteService]
    [Authorize(PaymentServicePermissions.Payments.Default)]
    public class PaymentsAppService : ApplicationService, IPaymentsAppService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsAppService(IPaymentRepository repository)
        {
            _paymentRepository = repository;
        }

        /// <summary>
        /// 获取(分页)支付方式列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PaymentDto>> GetWayAsync(GetPaymentInput input)
        {
            // 查询数量
            long totalCount = (await _paymentRepository.GetQueryableAsync())
                .WhereIf(!input.Code.IsNullOrWhiteSpace(), p => p.Code.Equals(input.Code))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                .WhereIf(input.IsEnable.HasValue, p => p.IsEnable == input.IsEnable)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<PaymentDto>();
            }
            List<Payment> paymentList = (await _paymentRepository.GetQueryableAsync())
                    .WhereIf(!input.Code.IsNullOrWhiteSpace(), p => p.Code.Equals(input.Name))
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .WhereIf(input.IsEnable.HasValue, p => p.IsEnable == input.IsEnable)
                    .OrderByIf<Payment, IQueryable<Payment>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();
            return new PagedResultDto<PaymentDto>(
                totalCount,
                ObjectMapper.Map<List<Payment>, List<PaymentDto>>(paymentList)
            );
        }

        /// <summary>
        /// 修改支付方式是否启用
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task UpdateIsEnableAsync(Guid id, bool isEnable)
        {
            Payment payment = await _paymentRepository.GetAsync(id);
            if (payment != null)
            {
                payment.IsEnable = isEnable;
            }
        }
    }
}