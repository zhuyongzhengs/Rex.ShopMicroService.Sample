using AutoMapper;
using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;

namespace Rex.PromotionService;

public class PromotionServiceApplicationAutoMapperProfile : Profile
{
    public PromotionServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Promotions

        CreateMap<PromotionCreateDto, Promotion>();
        CreateMap<PromotionUpdateDto, Promotion>();
        CreateMap<PromotionDto, Promotion>();
        CreateMap<Promotion, PromotionDto>();

        #endregion Promotions

        #region PromotionConditions

        CreateMap<PromotionConditionCreateDto, PromotionCondition>();
        CreateMap<PromotionConditionUpdateDto, PromotionCondition>();
        CreateMap<PromotionConditionDto, PromotionCondition>();
        CreateMap<PromotionCondition, PromotionConditionDto>();

        #endregion PromotionConditions

        #region PromotionRecords

        CreateMap<PromotionRecordCreateDto, PromotionRecord>();
        CreateMap<PromotionRecordUpdateDto, PromotionRecord>();
        CreateMap<PromotionRecordDto, PromotionRecord>();
        CreateMap<PromotionRecord, PromotionRecordDto>();

        #endregion PromotionRecords

        #region PromotionResults

        CreateMap<PromotionResultCreateDto, PromotionResult>();
        CreateMap<PromotionResultUpdateDto, PromotionResult>();
        CreateMap<PromotionResultDto, PromotionResult>();
        CreateMap<PromotionResult, PromotionResultDto>();

        #endregion PromotionResults

        #region Coupons

        CreateMap<CouponCreateDto, Coupon>();
        CreateMap<CouponUpdateDto, Coupon>();
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();

        #endregion Coupons

        #region PinTuanRules

        CreateMap<PinTuanRuleCreateDto, PinTuanRule>();
        CreateMap<PinTuanRuleUpdateDto, PinTuanRule>();
        CreateMap<PinTuanRuleDto, PinTuanRule>();
        CreateMap<PinTuanRule, PinTuanRuleDto>();

        #endregion PinTuanRules

        #region PinTuanRecords

        CreateMap<PinTuanRecordCreateDto, PinTuanRecord>();
        CreateMap<PinTuanRecordUpdateDto, PinTuanRecord>();
        CreateMap<PinTuanRecordDto, PinTuanRecord>();
        CreateMap<PinTuanRecord, PinTuanRecordDto>();

        #endregion PinTuanRecords

        #region PinTuanGoods

        CreateMap<PinTuanGoodCreateDto, PinTuanGood>();
        CreateMap<PinTuanGoodUpdateDto, PinTuanGood>();
        CreateMap<PinTuanGoodDto, PinTuanGood>();
        CreateMap<PinTuanGood, PinTuanGoodDto>();

        #endregion PinTuanGoods
    }
}