// 用户支付类型
declare type UserPayType = {
	sourceIds: string[];
	paymentCode: string;
	paymentType: number;
	params: object;
};

// 支付单明细
declare type BillPaymentDetailType = {
	id: string;
	no: string;
	sourceId: string;
	money: number;
	type: number;
	paymentCode: string;
};

// 支付订单详情
declare type PaymentOrderDetailType = {
	orderId: string;
	orderNo: string;
	paymentId: string;
	paymentNo: string;
	money: number;
	paymentCode: string;
	parameters: string;
	payedMsg: string;
	tradeNo: string;
	paymentType: number;
	paymentStatus: number;
	orderPayStatus: number;
	orderShipStatus: number;
	orderStatus: number;
	creationTime: string;
};

// 微信支付参数
declare type JsSdkWeChatPayParameterType = {
	nonceStr: string;
	timeStamp: number;
	package: string;
	signType: string;
	paySign: string;
};

// 微信支付参数(响应)结果
declare type WeChatPayParameterResultType = {
	detail: BillPaymentDetailType;
	payParameter: JsSdkWeChatPayParameterType;
};