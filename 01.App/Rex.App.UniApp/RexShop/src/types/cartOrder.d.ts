// 订单
declare type OrderType = {
	id: string;
	tenantId: string | null;

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
	logisticsId: string | null;
	logisticsName: string;
	costFreight: number;
	userId: string;
	sellerId: string;
	confirmStatus: number;
	confirmTime: string;
	storeId: string | null;
	shipAreaId: number;
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
	objectId: string | null;
	orderItems: OrderItemType[];

	concurrencyStamp: string;
	creationTime: string;
};

// 订单明细
declare type OrderItemType = {
	id: string;
	tenantId: string | null;

	orderId: string;
	order: OrderType | null;
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
	addon: string;

	concurrencyStamp: string;
	creationTime: string;
}

declare type UserPointExchangeType = {
	isUsePoint: boolean;
	point: number;
	canUsePoint: number;
	money: number;
}

declare type UserCouponExchangeType = {
	id: string;
	isUseCoupon: boolean;
	couponCode: string;
	couponName: string;
	code: string;
	parameters: string;
	money: number;
	startTime: string;
	endTime: string;
}

// 购物车订单
declare type CartOrderType = {
	// userShip: UserShipType;
	shoppingCarts: ShoppingCartType[];
	goodAmount: number;
	orderAmount: number;
	goodGradeMoney: number;
	goodPromotionMoney: number;
	goodSeckillMoney: number;
	orderPromotionMoney: number;
	couponPromotionMoney: number;
	pointExchangeMoney: number;
	costFreight: number;
	productNums: number;
	weight: number;
	orderNo: string;
	memo: string;
	pointExchangeItem: UserPointExchangeType | null;
	couponExchanges: UserCouponExchangeType[] | null;
};

// 创建用户订单
declare type UserOrderCreateType = {
	cartIds: string[];
	orderType: number;
	receiptType: number;
	paymentCode: string;
	storeId: string | null;
	shipAreaId: number;
	point: number;
	coupon: string;
	couponCodes: string[];
	memo: string;
	objectId: string | null;
	userShipId: string | null;
	ladingName: string;
	ladingMobile: string;
};

// 确认订单类型
declare type OrderConfirmType = {
	cartIds: string[];
	orderType: number;
	receiptType: number;
	paymentCode: string;
	shipAreaId: number;
	couponIds: string[] | null;
	isUsePoint: boolean;
	userShipId: string;
	objectId: string | null;
	orderNo: string | null;
	memo: string | null;
};