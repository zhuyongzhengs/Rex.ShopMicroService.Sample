using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.PromotionService.Promotions;
using Rex.Service.Permission.PromotionServices;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 团购秒杀控制器
    /// </summary>
    [Route("api/promotion/groupSeckill")]
    public class PromotionGroupSeckillController : PromotionServiceController, IPromotionGlobalAppService
    {
        private readonly IPromotionGlobalAppService _promotionAppService;

        public PromotionGroupSeckillController(IPromotionGlobalAppService promotionAppService)
        {
            _promotionAppService = promotionAppService;
        }

        /// <summary>
        /// 获取(分页)团购秒杀列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<PromotionDto>> GetPageListAsync([FromQuery] GetPromotionInput input)
        {
            input.Types = input.Types.Any() ? input.Types : [3, 4];
            return await _promotionAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 创建团购秒杀
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        [Authorize(PromotionServicePermissions.GroupSeckills.Create)]
        [HttpPost]
        public async Task<PromotionDto> CreateAsync([FromBody] PromotionCreateDto input)
        {
            return await _promotionAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(PromotionServicePermissions.GroupSeckills.Delete)]
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id)
        {
            await _promotionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据ID查询团购秒杀
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<PromotionDto> GetAsync([FromRoute] Guid id)
        {
            return await _promotionAppService.GetAsync(id);
        }

        /// <summary>
        /// 查询团购秒杀列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("list")]
        [NonAction]
        public async Task<PagedResultDto<PromotionDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return await _promotionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改团购秒杀
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        [Authorize(PromotionServicePermissions.GroupSeckills.Update)]
        [HttpPut("{id}")]
        public async Task<PromotionDto> UpdateAsync([FromRoute] Guid id, [FromBody] PromotionUpdateDto input)
        {
            if (!input.Parameters.IsNullOrWhiteSpace() && (input.PromotionConditions == null || !input.PromotionConditions.Any()))
            {
                PromotionConditionDto pCondition = new PromotionConditionDto();
                pCondition.Code = "GOODS_IDS";
                pCondition.Parameters = input.Parameters;
                input.PromotionConditions = new List<PromotionConditionDto>() { pCondition };
            }
            return await _promotionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 修改是否排他
        /// </summary>
        /// <param name="promotionId">团购秒杀ID</param>
        /// <param name="isExclusive">是否排他</param>
        /// <returns></returns>
        [Authorize(PromotionServicePermissions.GroupSeckills.Update)]
        [HttpPut("isExclusive/{promotionId}/{isExclusive}")]
        public async Task UpdateIsExclusiveAsync([FromRoute] Guid promotionId, [FromRoute] bool isExclusive)
        {
            await _promotionAppService.UpdateIsExclusiveAsync(promotionId, isExclusive);
        }

        /// <summary>
        /// 修改是否开启
        /// </summary>
        /// <param name="promotionId">团购秒杀ID</param>
        /// <param name="isEnable">是否开启</param>
        /// <returns></returns>
        [Authorize(PromotionServicePermissions.GroupSeckills.Update)]
        [HttpPut("isEnable/{promotionId}/{isEnable}")]
        public async Task UpdateIsEnableAsync([FromRoute] Guid promotionId, [FromRoute] bool isEnable)
        {
            await _promotionAppService.UpdateIsEnableAsync(promotionId, isEnable);
        }

        /// <summary>
        /// 获取团购秒杀条件类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("conditionType")]
        public async Task<List<CommonKeyValue>> GetPromotionConditionTypeAsync()
        {
            return await _promotionAppService.GetPromotionConditionTypeAsync();
        }

        /// <summary>
        /// 获取团购秒杀添加结果类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("resultType")]
        public async Task<List<CommonKeyValue>> GetPromotionResultTypeAsync()
        {
            return await _promotionAppService.GetPromotionResultTypeAsync();
        }

        /// <summary>
        /// 生成优惠券code 方法
        /// </summary>
        /// <param name="noOfCodes">定义一个int类型的参数 用来确定生成多少个优惠码</param>
        /// <param name="excludeCodesArray">定义一个exclude_codes_array类型的数组</param>
        /// <param name="codeLength">定义一个code_length的参数来确定优惠码的长度</param>
        /// <returns></returns>
        [HttpGet("generateCodes")]
        [NonAction]
        public List<string> GeneratePromotionCodes([FromQuery] int noOfCodes = 1, [FromBody] List<string> excludeCodesArray = null, [FromQuery] int codeLength = 10)
        {
            return _promotionAppService.GeneratePromotionCodes(noOfCodes, excludeCodesArray, codeLength);
        }

        /// <summary>
        /// 用户领取优惠券
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        [HttpPost("userReceive/{promotionId}")]
        [NonAction]
        public async Task UserReceiveCouponAsync([FromRoute] Guid promotionId)
        {
            await _promotionAppService.UserReceiveCouponAsync(promotionId);
        }

        /// <summary>
        /// 验证优惠劵是否可以领取
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        [HttpGet("validateReceive/{promotionId}")]
        [NonAction]
        public async Task<PromotionDto> ValidateCouponCanReceiveAsync([FromRoute] Guid promotionId)
        {
            return await _promotionAppService.ValidateCouponCanReceiveAsync(promotionId);
        }
    }
}