using Rex.Service.Core.Configurations;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销服务
    /// </summary>
    [RemoteService]
    public class PromotionGlobalAppService : CrudAppService<Promotion, PromotionDto, Guid, PagedAndSortedResultRequestDto, PromotionCreateDto, PromotionUpdateDto>, IPromotionGlobalAppService
    {
        private readonly IPromotionRepository _promotionRepository;
        public IRepository<PromotionCondition> PromotionConditionRepository { get; set; }
        public IRepository<PromotionResult> PromotionResultRepository { get; set; }
        private readonly ICouponRepository _couponRepository;

        public PromotionGlobalAppService(IPromotionRepository repository, ICouponRepository couponRepository) : base(repository)
        {
            _promotionRepository = repository;
            _couponRepository = couponRepository;
        }

        /// <summary>
        /// 获取(分页)促销列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PromotionDto>> GetPageListAsync(GetPromotionInput input)
        {
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (input.StartAndEndTime != null && input.StartAndEndTime.Any())
            {
                startTime = input.StartAndEndTime[0];
                endTime = input.StartAndEndTime[1];
            }

            input.Types = input.Types.Any() ? input.Types : [(int)PromotionType.Promotion];

            // 查询数量
            long totalCount = await _promotionRepository.GetPageCountAsync(input.Types, input.Name, input.IsEnable, input.IsExclusive, startTime, endTime, input.Status);
            if (totalCount < 1)
            {
                return new PagedResultDto<PromotionDto>();
            }

            // 查询数据列表
            List<Promotion> promotionList = await _promotionRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Types, input.Name, input.IsEnable, input.IsExclusive, startTime, endTime, input.Status);
            return new PagedResultDto<PromotionDto>(
                totalCount,
                ObjectMapper.Map<List<Promotion>, List<PromotionDto>>(promotionList)
            );
        }

        /// <summary>
        /// 创建促销
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<PromotionDto> CreateAsync(PromotionCreateDto input)
        {
            return base.CreateAsync(input);
        }

        /// <summary>
        /// 修改促销
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        public override async Task<PromotionDto> UpdateAsync(Guid id, PromotionUpdateDto input)
        {
            Promotion promotion = await _promotionRepository.GetAsync(id);
            if (promotion == null)
                throw new UserFriendlyException($"促销ID[{id}]不存在或已被删除，请检查！", SystemStatusCode.Fail.ToPromotionServicePrefix());
            promotion.Name = input.Name;
            promotion.Type = input.Type;
            promotion.Weight = input.Weight;
            promotion.Parameters = input.Parameters;
            promotion.MaxNums = input.MaxNums;
            promotion.MaxGoodNums = input.MaxGoodNums;
            promotion.MaxRecevieNums = input.MaxRecevieNums;
            promotion.StartTime = input.StartTime;
            promotion.EndTime = input.EndTime;
            promotion.IsExclusive = input.IsExclusive;
            promotion.IsAutoReceive = input.IsAutoReceive;
            promotion.IsEnable = input.IsEnable;
            promotion.EffectiveDays = input.EffectiveDays;
            promotion.EffectiveHours = input.EffectiveHours;

            #region 删除促销条件/结果

            await PromotionConditionRepository.DeleteAsync(p => p.PromotionId == promotion.Id);
            await PromotionResultRepository.DeleteAsync(p => p.PromotionId == promotion.Id);

            #endregion 删除促销条件/结果

            #region 创建促销条件

            List<PromotionCondition> promotionConditions = new List<PromotionCondition>();
            foreach (var promotionCondition in input.PromotionConditions)
            {
                promotionConditions.Add(new PromotionCondition(GuidGenerator.Create())
                {
                    PromotionId = promotion.Id,
                    Code = promotionCondition.Code,
                    Parameters = promotionCondition.Parameters,
                });
            }
            if (promotionConditions.Count > 0)
                await PromotionConditionRepository.InsertManyAsync(promotionConditions);

            #endregion 创建促销条件

            #region 创建促销结果

            List<PromotionResult> promotionResults = new List<PromotionResult>();
            foreach (var promotionResult in input.PromotionResults)
            {
                promotionResults.Add(new PromotionResult(GuidGenerator.Create())
                {
                    PromotionId = promotion.Id,
                    Code = promotionResult.Code,
                    Parameters = promotionResult.Parameters,
                });
            }
            if (promotionResults.Count > 0)
                await PromotionResultRepository.InsertManyAsync(promotionResults);

            #endregion 创建促销结果

            return ObjectMapper.Map<Promotion, PromotionDto>(promotion);
        }

        /// <summary>
        /// 修改是否排他
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="isExclusive">是否排他</param>
        /// <returns></returns>
        public async Task UpdateIsExclusiveAsync(Guid promotionId, bool isExclusive)
        {
            Promotion promotion = await _promotionRepository.GetAsync(promotionId);
            if (promotion != null)
            {
                promotion.IsExclusive = isExclusive;
            }
        }

        /// <summary>
        /// 修改是否开启
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="isEnable">是否开启</param>
        /// <returns></returns>
        public async Task UpdateIsEnableAsync(Guid promotionId, bool isEnable)
        {
            Promotion promotion = await _promotionRepository.GetAsync(promotionId);
            if (promotion != null)
            {
                promotion.IsEnable = isEnable;
            }
        }

        /// <summary>
        /// 获取促销条件类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommonKeyValue>> GetPromotionConditionTypeAsync()
        {
            return await Task.FromResult(SystemSettingDictionary.GetPromotionConditionType());
        }

        /// <summary>
        /// 获取促销添加结果类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommonKeyValue>> GetPromotionResultTypeAsync()
        {
            return await Task.FromResult(SystemSettingDictionary.GetPromotionResultType());
        }

        /// <summary>
        /// 生成优惠券code 方法
        /// </summary>
        /// <param name="noOfCodes">定义一个int类型的参数 用来确定生成多少个优惠码</param>
        /// <param name="excludeCodesArray">定义一个exclude_codes_array类型的数组</param>
        /// <param name="codeLength">定义一个code_length的参数来确定优惠码的长度</param>
        /// <returns></returns>
        public List<string> GeneratePromotionCodes(int noOfCodes = 1, List<string> excludeCodesArray = null, int codeLength = 10)
        {
            char[] constant =
            {
                '0','1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            var promotionCodes = new List<string>();  //这个数组用来接收生成的优惠码
            Random rd = new Random();
            for (int i = 0; i < noOfCodes; i++)
            {
                var code = "";
                for (int j = 0; j < codeLength; j++)
                {
                    code += constant[rd.Next(62)];
                }
                //如果生成的6位随机数不再我们定义的$promotion_codes函数里面
                if (!promotionCodes.Contains(code))
                {
                    if (excludeCodesArray != null && excludeCodesArray.Any())
                    {
                        if (!excludeCodesArray.Contains(code))
                        {
                            promotionCodes.Add(code);//将优惠码赋值给数组
                        }
                        else
                        {
                            i--;
                        }
                    }
                    else
                    {
                        promotionCodes.Add(code);//将优惠码赋值给数组
                    }
                }
                else
                {
                    i--;
                }
            }
            return promotionCodes;
        }

        /// <summary>
        /// 用户领取优惠券
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        public async Task UserReceiveCouponAsync(Guid promotionId)
        {
            PromotionDto promotion = await ValidateCouponCanReceiveAsync(promotionId);
            DateTime beginDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (promotion.EffectiveDays > 0)
            {
                endDate = endDate.AddDays(promotion.EffectiveDays);
            }
            if (promotion.EffectiveHours > 0)
            {
                endDate = endDate.AddHours(promotion.EffectiveHours);
            }
            Coupon coupon = new Coupon();
            coupon.CouponCode = GeneratePromotionCodes().First();
            coupon.PromotionId = promotionId;
            coupon.IsUsed = false;
            coupon.UserId = CurrentUser.Id.Value;
            coupon.StartTime = beginDate;
            coupon.EndTime = endDate;
            coupon.Remark = "[接口]用户领取优惠券。";
            coupon.CreationTime = beginDate;
            await _couponRepository.InsertAsync(coupon);
        }

        /// <summary>
        /// 验证优惠劵是否可以领取
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        public async Task<PromotionDto> ValidateCouponCanReceiveAsync(Guid promotionId)
        {
            DateTime now = DateTime.Now;
            Promotion promotion = await _promotionRepository.FindAsync(x => x.Id == promotionId && x.EndTime > now && x.IsEnable && x.IsAutoReceive && x.Type == (int)PromotionType.Coupon);
            if (promotion == null)
                throw new UserFriendlyException($"优惠券[{promotionId}]不存在或已过期，请下次再来！", SystemStatusCode.Fail.ToPromotionServicePrefix());

            int receiveNums = (await _couponRepository.GetQueryableAsync()).Where(x => x.PromotionId == promotionId).Count();
            if (promotion.MaxRecevieNums <= 0 || receiveNums >= promotion.MaxRecevieNums)
                throw new UserFriendlyException($"该优惠券已被领完，请下次再来！", SystemStatusCode.Fail.ToPromotionServicePrefix());

            if (promotion.MaxNums > 0)
            {
                // 判断用户是否已领取
                int userReceiveNums = (await _couponRepository.GetQueryableAsync()).Where(x => x.PromotionId == promotionId && x.UserId == CurrentUser.Id).Count();
                if (userReceiveNums >= promotion.MaxNums)
                    throw new UserFriendlyException($"已超出领取限额！", SystemStatusCode.Fail.ToPromotionServicePrefix());
            }
            return ObjectMapper.Map<Promotion, PromotionDto>(promotion);
        }
    }
}