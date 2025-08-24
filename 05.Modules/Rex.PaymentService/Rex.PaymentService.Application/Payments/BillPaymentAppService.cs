using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单服务接口
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillPaymentAppService : ApplicationService, IBillPaymentAppService
    {
        private readonly IBillPaymentRepository _billPaymentRepository;

        public BillPaymentAppService(IBillPaymentRepository billPaymentRepository)
        {
            _billPaymentRepository = billPaymentRepository;
        }

        /// <summary>
        /// 查询单据类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetTypesAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillPaymentType>());
        }

        /// <summary>
        /// 查询支付状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillPaymentStatus>());
        }

        /// <summary>
        /// 获取(分页)支付单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [Authorize(PaymentServicePermissions.BillPayments.Default)]
        public async Task<PagedResultDto<BillPaymentDto>> GetListAsync(GetBillPaymentInput input)
        {
            // 查询数量
            long totalCount = (await _billPaymentRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No.Equals(input.No))
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(!input.TradeNo.IsNullOrWhiteSpace(), p => p.TradeNo.Equals(input.TradeNo))
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<BillPaymentDto>();
            }
            List<BillPayment> billPaymentList = (await _billPaymentRepository.GetQueryableAsync())
                    .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No.Equals(input.No))
                    .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                    .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                    .WhereIf(!input.TradeNo.IsNullOrWhiteSpace(), p => p.TradeNo.Equals(input.TradeNo))
                    .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                    .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                    .OrderByIf<BillPayment, IQueryable<BillPayment>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();
            return new PagedResultDto<BillPaymentDto>(
                totalCount,
                ObjectMapper.Map<List<BillPayment>, List<BillPaymentDto>>(billPaymentList)
            );
        }

        /// <summary>
        /// 获取用户支付单信息
        /// </summary>
        /// <param name="no">单号</param>
        /// <returns></returns>
        public async Task<BillPaymentDto> GetUserBillPaymentAsync(string no)
        {
            BillPayment billPayment = await _billPaymentRepository.FindAsync(x => x.No == no && x.UserId == CurrentUser.Id);
            if (billPayment == null)
            {
                throw new UserFriendlyException($"支付单[{no}]不存在或已被删除，请检查！", SystemStatusCode.Fail.ToPaymentServicePrefix()).WithData(nameof(no), no);
            }
            return ObjectMapper.Map<BillPayment, BillPaymentDto>(billPayment);
        }

        /// <summary>
        /// 根据[资源编号]获取支付单
        /// </summary>
        /// <param name="sourceId">资源编号</param>
        /// <returns></returns>
        public async Task<List<BillPaymentDto>> GetSourceIdAsync(string sourceId)
        {
            List<BillPayment> billPayments = await _billPaymentRepository.GetListAsync(x => x.SourceId.Contains(sourceId));
            return ObjectMapper.Map<List<BillPayment>, List<BillPaymentDto>>(billPayments);
        }
    }
}