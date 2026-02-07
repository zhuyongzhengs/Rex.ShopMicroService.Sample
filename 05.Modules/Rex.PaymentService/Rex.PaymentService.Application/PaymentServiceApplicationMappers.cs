using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using Rex.PaymentService.Commons;
using Rex.PaymentService.Payments;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Rex.PaymentService;

/* 在此处添加自己的映射关系 */

#region Payments

[Mapper]
public partial class PaymentMapper : TwoWayMapperBase<PaymentCreateDto, Payment>
{
    public override partial Payment Map(PaymentCreateDto source);

    public override partial void Map(PaymentCreateDto source, Payment destination);

    public override partial PaymentCreateDto ReverseMap(Payment source);

    public override partial void ReverseMap(Payment source, PaymentCreateDto destination);
}

[Mapper]
public partial class PaymentUpdateMapper : TwoWayMapperBase<PaymentUpdateDto, Payment>
{
    public override partial Payment Map(PaymentUpdateDto source);

    public override partial void Map(PaymentUpdateDto source, Payment destination);

    public override partial PaymentUpdateDto ReverseMap(Payment source);

    public override partial void ReverseMap(Payment source, PaymentUpdateDto destination);
}

[Mapper]
public partial class PaymentDtoMapper : TwoWayMapperBase<PaymentDto, Payment>
{
    public override partial Payment Map(PaymentDto source);

    public override partial void Map(PaymentDto source, Payment destination);

    public override partial PaymentDto ReverseMap(Payment source);

    public override partial void ReverseMap(Payment source, PaymentDto destination);
}

#endregion Payments

#region BillPayments

[Mapper]
public partial class BillPaymentMapper : TwoWayMapperBase<BillPaymentCreateDto, BillPayment>
{
    public override partial BillPayment Map(BillPaymentCreateDto source);

    public override partial void Map(BillPaymentCreateDto source, BillPayment destination);

    public override partial BillPaymentCreateDto ReverseMap(BillPayment source);

    public override partial void ReverseMap(BillPayment source, BillPaymentCreateDto destination);
}

[Mapper]
public partial class BillPaymentUpdateMapper : TwoWayMapperBase<BillPaymentUpdateDto, BillPayment>
{
    public override partial BillPayment Map(BillPaymentUpdateDto source);

    public override partial void Map(BillPaymentUpdateDto source, BillPayment destination);

    public override partial BillPaymentUpdateDto ReverseMap(BillPayment source);

    public override partial void ReverseMap(BillPayment source, BillPaymentUpdateDto destination);
}

[Mapper]
public partial class BillPaymentDtoMapper : TwoWayMapperBase<BillPaymentDto, BillPayment>
{
    public override partial BillPayment Map(BillPaymentDto source);

    public override partial void Map(BillPaymentDto source, BillPayment destination);

    public override partial BillPaymentDto ReverseMap(BillPayment source);

    public override partial void ReverseMap(BillPayment source, BillPaymentDto destination);
}

// 注意：BillPaymentDto → BillPaymentDetailDto 是 DTO 到 DTO 的单向映射
[Mapper]
public partial class BillPaymentDetailMapper : MapperBase<BillPaymentDto, BillPaymentDetailDto>
{
    public override partial BillPaymentDetailDto Map(BillPaymentDto source);

    public override partial void Map(BillPaymentDto source, BillPaymentDetailDto destination);
}

// BillPayment → BillPaymentDetailDto 也是单向（Entity → Detail DTO）
[Mapper]
public partial class BillPaymentToDetailMapper : MapperBase<BillPayment, BillPaymentDetailDto>
{
    public override partial BillPaymentDetailDto Map(BillPayment source);

    public override partial void Map(BillPayment source, BillPaymentDetailDto destination);
}

#endregion BillPayments

#region BillRefunds

[Mapper]
public partial class BillRefundMapper : TwoWayMapperBase<BillRefundCreateDto, BillRefund>
{
    public override partial BillRefund Map(BillRefundCreateDto source);

    public override partial void Map(BillRefundCreateDto source, BillRefund destination);

    public override partial BillRefundCreateDto ReverseMap(BillRefund source);

    public override partial void ReverseMap(BillRefund source, BillRefundCreateDto destination);
}

[Mapper]
public partial class BillRefundUpdateMapper : TwoWayMapperBase<BillRefundUpdateDto, BillRefund>
{
    public override partial BillRefund Map(BillRefundUpdateDto source);

    public override partial void Map(BillRefundUpdateDto source, BillRefund destination);

    public override partial BillRefundUpdateDto ReverseMap(BillRefund source);

    public override partial void ReverseMap(BillRefund source, BillRefundUpdateDto destination);
}

[Mapper]
public partial class BillRefundDtoMapper : TwoWayMapperBase<BillRefundDto, BillRefund>
{
    public override partial BillRefund Map(BillRefundDto source);

    public override partial void Map(BillRefundDto source, BillRefund destination);

    public override partial BillRefundDto ReverseMap(BillRefund source);

    public override partial void ReverseMap(BillRefund source, BillRefundDto destination);
}

#endregion BillRefunds

#region JsSdkWeChatPayParameters

// GetJsSdkWeChatPayParametersResult → JsSdkWeChatPayParameterDto 是单向
[Mapper]
public partial class JsSdkWeChatPayParameterMapper : MapperBase<GetJsSdkWeChatPayParametersResult, JsSdkWeChatPayParameterDto>
{
    public override partial JsSdkWeChatPayParameterDto Map(GetJsSdkWeChatPayParametersResult source);

    public override partial void Map(GetJsSdkWeChatPayParametersResult source, JsSdkWeChatPayParameterDto destination);
}

#endregion JsSdkWeChatPayParameters