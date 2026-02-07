using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Rex.PromotionService;

#region Promotions

[Mapper]
public partial class PromotionCreateMapper : TwoWayMapperBase<PromotionCreateDto, Promotion>
{
    public override partial Promotion Map(PromotionCreateDto source);

    public override partial void Map(PromotionCreateDto source, Promotion destination);

    public override partial PromotionCreateDto ReverseMap(Promotion source);

    public override partial void ReverseMap(Promotion source, PromotionCreateDto destination);
}

[Mapper]
public partial class PromotionUpdateMapper : TwoWayMapperBase<PromotionUpdateDto, Promotion>
{
    public override partial Promotion Map(PromotionUpdateDto source);

    public override partial void Map(PromotionUpdateDto source, Promotion destination);

    public override partial PromotionUpdateDto ReverseMap(Promotion source);

    public override partial void ReverseMap(Promotion source, PromotionUpdateDto destination);
}

[Mapper]
public partial class PromotionDtoMapper : TwoWayMapperBase<PromotionDto, Promotion>
{
    public override partial Promotion Map(PromotionDto source);

    public override partial void Map(PromotionDto source, Promotion destination);

    public override partial PromotionDto ReverseMap(Promotion source);

    public override partial void ReverseMap(Promotion source, PromotionDto destination);
}

#endregion Promotions

#region PromotionConditions

[Mapper]
public partial class PromotionConditionCreateMapper : TwoWayMapperBase<PromotionConditionCreateDto, PromotionCondition>
{
    public override partial PromotionCondition Map(PromotionConditionCreateDto source);

    public override partial void Map(PromotionConditionCreateDto source, PromotionCondition destination);

    public override partial PromotionConditionCreateDto ReverseMap(PromotionCondition source);

    public override partial void ReverseMap(PromotionCondition source, PromotionConditionCreateDto destination);
}

[Mapper]
public partial class PromotionConditionUpdateMapper : TwoWayMapperBase<PromotionConditionUpdateDto, PromotionCondition>
{
    public override partial PromotionCondition Map(PromotionConditionUpdateDto source);

    public override partial void Map(PromotionConditionUpdateDto source, PromotionCondition destination);

    public override partial PromotionConditionUpdateDto ReverseMap(PromotionCondition source);

    public override partial void ReverseMap(PromotionCondition source, PromotionConditionUpdateDto destination);
}

[Mapper]
public partial class PromotionConditionDtoMapper : TwoWayMapperBase<PromotionConditionDto, PromotionCondition>
{
    public override partial PromotionCondition Map(PromotionConditionDto source);

    public override partial void Map(PromotionConditionDto source, PromotionCondition destination);

    public override partial PromotionConditionDto ReverseMap(PromotionCondition source);

    public override partial void ReverseMap(PromotionCondition source, PromotionConditionDto destination);
}

#endregion PromotionConditions

#region PromotionRecords

[Mapper]
public partial class PromotionRecordCreateMapper : TwoWayMapperBase<PromotionRecordCreateDto, PromotionRecord>
{
    public override partial PromotionRecord Map(PromotionRecordCreateDto source);

    public override partial void Map(PromotionRecordCreateDto source, PromotionRecord destination);

    public override partial PromotionRecordCreateDto ReverseMap(PromotionRecord source);

    public override partial void ReverseMap(PromotionRecord source, PromotionRecordCreateDto destination);
}

[Mapper]
public partial class PromotionRecordUpdateMapper : TwoWayMapperBase<PromotionRecordUpdateDto, PromotionRecord>
{
    public override partial PromotionRecord Map(PromotionRecordUpdateDto source);

    public override partial void Map(PromotionRecordUpdateDto source, PromotionRecord destination);

    public override partial PromotionRecordUpdateDto ReverseMap(PromotionRecord source);

    public override partial void ReverseMap(PromotionRecord source, PromotionRecordUpdateDto destination);
}

[Mapper]
public partial class PromotionRecordDtoMapper : TwoWayMapperBase<PromotionRecordDto, PromotionRecord>
{
    public override partial PromotionRecord Map(PromotionRecordDto source);

    public override partial void Map(PromotionRecordDto source, PromotionRecord destination);

    public override partial PromotionRecordDto ReverseMap(PromotionRecord source);

    public override partial void ReverseMap(PromotionRecord source, PromotionRecordDto destination);
}

#endregion PromotionRecords

#region PromotionResults

[Mapper]
public partial class PromotionResultCreateMapper : TwoWayMapperBase<PromotionResultCreateDto, PromotionResult>
{
    public override partial PromotionResult Map(PromotionResultCreateDto source);

    public override partial void Map(PromotionResultCreateDto source, PromotionResult destination);

    public override partial PromotionResultCreateDto ReverseMap(PromotionResult source);

    public override partial void ReverseMap(PromotionResult source, PromotionResultCreateDto destination);
}

[Mapper]
public partial class PromotionResultUpdateMapper : TwoWayMapperBase<PromotionResultUpdateDto, PromotionResult>
{
    public override partial PromotionResult Map(PromotionResultUpdateDto source);

    public override partial void Map(PromotionResultUpdateDto source, PromotionResult destination);

    public override partial PromotionResultUpdateDto ReverseMap(PromotionResult source);

    public override partial void ReverseMap(PromotionResult source, PromotionResultUpdateDto destination);
}

[Mapper]
public partial class PromotionResultDtoMapper : TwoWayMapperBase<PromotionResultDto, PromotionResult>
{
    public override partial PromotionResult Map(PromotionResultDto source);

    public override partial void Map(PromotionResultDto source, PromotionResult destination);

    public override partial PromotionResultDto ReverseMap(PromotionResult source);

    public override partial void ReverseMap(PromotionResult source, PromotionResultDto destination);
}

#endregion PromotionResults

#region Coupons

[Mapper]
public partial class CouponCreateMapper : TwoWayMapperBase<CouponCreateDto, Coupon>
{
    public override partial Coupon Map(CouponCreateDto source);

    public override partial void Map(CouponCreateDto source, Coupon destination);

    public override partial CouponCreateDto ReverseMap(Coupon source);

    public override partial void ReverseMap(Coupon source, CouponCreateDto destination);
}

[Mapper]
public partial class CouponUpdateMapper : TwoWayMapperBase<CouponUpdateDto, Coupon>
{
    public override partial Coupon Map(CouponUpdateDto source);

    public override partial void Map(CouponUpdateDto source, Coupon destination);

    public override partial CouponUpdateDto ReverseMap(Coupon source);

    public override partial void ReverseMap(Coupon source, CouponUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class CouponDtoMapper : TwoWayMapperBase<CouponDto, Coupon>
{
    public override partial Coupon Map(CouponDto source);

    public override partial void Map(CouponDto source, Coupon destination);

    public override partial CouponDto ReverseMap(Coupon source);

    public override partial void ReverseMap(Coupon source, CouponDto destination);
}

#endregion Coupons

#region PinTuanRules

[Mapper]
public partial class PinTuanRuleCreateMapper : TwoWayMapperBase<PinTuanRuleCreateDto, PinTuanRule>
{
    public override partial PinTuanRule Map(PinTuanRuleCreateDto source);

    public override partial void Map(PinTuanRuleCreateDto source, PinTuanRule destination);

    public override partial PinTuanRuleCreateDto ReverseMap(PinTuanRule source);

    public override partial void ReverseMap(PinTuanRule source, PinTuanRuleCreateDto destination);
}

[Mapper]
public partial class PinTuanRuleUpdateMapper : TwoWayMapperBase<PinTuanRuleUpdateDto, PinTuanRule>
{
    public override partial PinTuanRule Map(PinTuanRuleUpdateDto source);

    public override partial void Map(PinTuanRuleUpdateDto source, PinTuanRule destination);

    public override partial PinTuanRuleUpdateDto ReverseMap(PinTuanRule source);

    public override partial void ReverseMap(PinTuanRule source, PinTuanRuleUpdateDto destination);
}

[Mapper]
public partial class PinTuanRuleDtoMapper : TwoWayMapperBase<PinTuanRuleDto, PinTuanRule>
{
    public override partial PinTuanRule Map(PinTuanRuleDto source);

    public override partial void Map(PinTuanRuleDto source, PinTuanRule destination);

    public override partial PinTuanRuleDto ReverseMap(PinTuanRule source);

    public override partial void ReverseMap(PinTuanRule source, PinTuanRuleDto destination);
}

#endregion PinTuanRules

#region PinTuanRecords

[Mapper]
public partial class PinTuanRecordCreateMapper : TwoWayMapperBase<PinTuanRecordCreateDto, PinTuanRecord>
{
    public override partial PinTuanRecord Map(PinTuanRecordCreateDto source);

    public override partial void Map(PinTuanRecordCreateDto source, PinTuanRecord destination);

    public override partial PinTuanRecordCreateDto ReverseMap(PinTuanRecord source);

    public override partial void ReverseMap(PinTuanRecord source, PinTuanRecordCreateDto destination);
}

[Mapper]
public partial class PinTuanRecordUpdateMapper : TwoWayMapperBase<PinTuanRecordUpdateDto, PinTuanRecord>
{
    public override partial PinTuanRecord Map(PinTuanRecordUpdateDto source);

    public override partial void Map(PinTuanRecordUpdateDto source, PinTuanRecord destination);

    public override partial PinTuanRecordUpdateDto ReverseMap(PinTuanRecord source);

    public override partial void ReverseMap(PinTuanRecord source, PinTuanRecordUpdateDto destination);
}

[Mapper]
public partial class PinTuanRecordDtoMapper : TwoWayMapperBase<PinTuanRecordDto, PinTuanRecord>
{
    public override partial PinTuanRecord Map(PinTuanRecordDto source);

    public override partial void Map(PinTuanRecordDto source, PinTuanRecord destination);

    public override partial PinTuanRecordDto ReverseMap(PinTuanRecord source);

    public override partial void ReverseMap(PinTuanRecord source, PinTuanRecordDto destination);
}

#endregion PinTuanRecords

#region PinTuanGoods

[Mapper]
public partial class PinTuanGoodCreateMapper : TwoWayMapperBase<PinTuanGoodCreateDto, PinTuanGood>
{
    public override partial PinTuanGood Map(PinTuanGoodCreateDto source);

    public override partial void Map(PinTuanGoodCreateDto source, PinTuanGood destination);

    public override partial PinTuanGoodCreateDto ReverseMap(PinTuanGood source);

    public override partial void ReverseMap(PinTuanGood source, PinTuanGoodCreateDto destination);
}

[Mapper]
public partial class PinTuanGoodUpdateMapper : TwoWayMapperBase<PinTuanGoodUpdateDto, PinTuanGood>
{
    public override partial PinTuanGood Map(PinTuanGoodUpdateDto source);

    public override partial void Map(PinTuanGoodUpdateDto source, PinTuanGood destination);

    public override partial PinTuanGoodUpdateDto ReverseMap(PinTuanGood source);

    public override partial void ReverseMap(PinTuanGood source, PinTuanGoodUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class PinTuanGoodDtoMapper : TwoWayMapperBase<PinTuanGoodDto, PinTuanGood>
{
    public override partial PinTuanGood Map(PinTuanGoodDto source);

    public override partial void Map(PinTuanGoodDto source, PinTuanGood destination);

    public override partial PinTuanGoodDto ReverseMap(PinTuanGood source);

    public override partial void ReverseMap(PinTuanGood source, PinTuanGoodDto destination);
}

#endregion PinTuanGoods