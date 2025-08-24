// orderDetail
declare type OrderDetailType = {
	id: string;
	no: string;
	orderType: number;
	orderTypeDisplay: string;
	source: number;
	sourceDisplay: string;
	paymentCode: string;
	status: number;
	statusDisplay: string;
	shipStatus: number;
	shipStatusDisplay: string;
	logisticsId: string;
	logisticsName: string;
	weight: number;
	goodAmount: number;
	costFreight: number;
	payedAmount: number;
	gradeDiscountAmount: number;
	seckillDiscountAmount: number;
	orderDiscountAmount: number;
	goodsDiscountAmount: number;
	couponDiscountAmount: number;
	pointMoney: number;
	orderAmount: number;
	payStatus: number;
	payStatusDisplay: string;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	confirmStatus: number;
	confirmStatusDisplay: string;
	confirmTime: string;
	memo: string;
	userId: string;
	userName: string;
	nickName: string;
	phoneNumber: string;
	userGradeName: string;
	mark: string;
	creationTime: string;
	orderItems: OrderItemType[];
	billPayments: RowBillPaymentType[];
	billRefunds: RowBillRefundType[];
	billDeliverys: RowBillDeliveryType[];
	billReships: RowBillReshipType[];
	coupons: RowCouponType[];
	orderLogs: RowOrderLogType[];
}

// orderDelivery
declare type OrderDeliveryType = {
	id: string;
	no: string;
	logisticsId: string;
	logisticsName: string;
	weight: number;
	costFreight: number;
	memo: string;
	userId: string;
	userName: string;
	nickName: string;
	phoneNumber: string;
	userGradeName: string;
	mark: string;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	logiCode: string;
	logiName: string;
	logiNo: string;
	deliveryMemo: string;
	orderItems: OrderItemType[];
}

// orderItem
declare type OrderItemType = {
	id: string;
	orderId: string;
	order: RowOrderType;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	price: number;
	costPrice: number;
	mktPrice: number;
	imageUrl: string;
	nums: number;
	amount: number;
	promotionAmount: number;
	promotionList: string;
	weight: number;
	sendNums: number;
	reshipNums: number;
	addon: string;
	concurrencyStamp: string;
	creationTime: string;
}

// order
declare type RowOrderType = {
	id: string;
	tenantId: string;

	no: string;
	goodAmount: number;
	payedAmount: number;
	orderAmount: number;
	payStatus: number;
	shipStatus: number;
	status: number;
	orderType: number;
	receiptType: number;
	paymentCode: string;
	paymentTime: string;
	logisticsId: string;
	logisticsName: string;
	costFreight: number;
	userId: string;
	sellerId: string;
	confirmStatus: number;
	confirmTime: string;
	storeId: string;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	weight: number;
	point: number;
	pointMoney: number;
	orderDiscountAmount: number;
	goodsDiscountAmount: number;
	couponDiscountAmount: number;
	coupon: string;
	promotionList: string;
	memo: string;
	ip: string;
	mark: string;
	source: number;
	isComment: boolean;
	objectId: string;

	concurrencyStamp: string;
	creationTime: string;
};

interface OrderTableType extends TableType {
	data: RowOrderType[];
}

declare interface OrderState {
	tableData: OrderTableType;
}
