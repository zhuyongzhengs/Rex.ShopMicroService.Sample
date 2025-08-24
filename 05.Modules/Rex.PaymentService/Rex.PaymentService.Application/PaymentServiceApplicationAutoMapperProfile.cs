using AutoMapper;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using Rex.PaymentService.Commons;
using Rex.PaymentService.Payments;

namespace Rex.PaymentService;

public class PaymentServiceApplicationAutoMapperProfile : Profile
{
    public PaymentServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Payments

        CreateMap<PaymentCreateDto, Payment>();
        CreateMap<PaymentUpdateDto, Payment>();
        CreateMap<PaymentDto, Payment>();
        CreateMap<Payment, PaymentDto>();

        #endregion Payments

        #region BillPayments

        CreateMap<BillPaymentCreateDto, BillPayment>();
        CreateMap<BillPaymentUpdateDto, BillPayment>();
        CreateMap<BillPaymentDto, BillPayment>();
        CreateMap<BillPaymentDto, BillPaymentDetailDto>();
        CreateMap<BillPayment, BillPaymentDto>();
        CreateMap<BillPayment, BillPaymentDetailDto>();

        #endregion BillPayments

        #region BillRefunds

        CreateMap<BillRefundCreateDto, BillRefund>();
        CreateMap<BillRefundUpdateDto, BillRefund>();
        CreateMap<BillRefundDto, BillRefund>();
        CreateMap<BillRefund, BillRefundDto>();

        #endregion BillRefunds

        #region JsSdkWeChatPayParameters

        CreateMap<GetJsSdkWeChatPayParametersResult, JsSdkWeChatPayParameterDto>();

        #endregion JsSdkWeChatPayParameters
    }
}