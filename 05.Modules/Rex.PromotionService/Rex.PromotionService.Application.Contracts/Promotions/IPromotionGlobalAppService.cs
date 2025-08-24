using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销(全局)服务接口
    /// </summary>
    public interface IPromotionGlobalAppService : ICrudAppService<PromotionDto, Guid, PagedAndSortedResultRequestDto, PromotionCreateDto, PromotionUpdateDto>
    {
        /// <summary>
        /// 获取(分页)促销列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PromotionDto>> GetPageListAsync(GetPromotionInput input);

        /// <summary>
        /// 修改是否排他
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="isExclusive">是否排他</param>
        /// <returns></returns>
        Task UpdateIsExclusiveAsync(Guid promotionId, bool isExclusive);

        /// <summary>
        /// 修改是否开启
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="isEnable">是否开启</param>
        /// <returns></returns>
        Task UpdateIsEnableAsync(Guid promotionId, bool isEnable);

        /// <summary>
        /// 获取促销条件类型
        /// </summary>
        /// <returns></returns>
        Task<List<CommonKeyValue>> GetPromotionConditionTypeAsync();

        /// <summary>
        /// 获取促销添加结果类型
        /// </summary>
        /// <returns></returns>
        Task<List<CommonKeyValue>> GetPromotionResultTypeAsync();

        /// <summary>
        /// 生成优惠券code 方法
        /// </summary>
        /// <param name="noOfCodes">定义一个int类型的参数 用来确定生成多少个优惠码</param>
        /// <param name="excludeCodesArray">定义一个exclude_codes_array类型的数组</param>
        /// <param name="codeLength">定义一个code_length的参数来确定优惠码的长度</param>
        /// <returns></returns>
        List<string> GeneratePromotionCodes(int noOfCodes = 1, List<string> excludeCodesArray = null,
            int codeLength = 10);

        /// <summary>
        /// 用户领取优惠券
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        Task UserReceiveCouponAsync(Guid promotionId);

        /// <summary>
        /// 验证优惠劵是否可以领取
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        Task<PromotionDto> ValidateCouponCanReceiveAsync(Guid promotionId);
    }
}