// 用户订单
declare type UserOrderType = {
	id: string;
	no: string;
	goodAmount: number;
	payedAmount: number;
	orderAmount: number;
	payStatus: number;
	shipStatus: number;
	status: number;
	confirmStatus: number;
	orderType: number;
	pointMoney: number;
	gradeDiscountAmount: number;
	orderDiscountAmount: number;
	goodsDiscountAmount: number;
	couponDiscountAmount: number;
	costFreight: number;
	productNums: number;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	paymentCode: string;
	paymentTime: string;
	isComment: boolean;
	// aftersalesId: string;
	aftersales: BillAftersalesType[] | null;
	isAftersalesStatus: boolean;
	orderItems: UserOrderItemType[];
	creationTime: string;
};

// 用户订单明细
declare type UserOrderItemType = {
	id: string;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	price: number;
	imageUrl: string;
	nums: number;
	amount: number;
	promotionAmount: number;
	addon: string;
	disabled: boolean;
}

// 用户订单评价
declare type UserOrderCommentType = {
	imageUrl: string;
	name: string;
	nums: number;
	amount: number;
	userId: string;
	userName: string;
	avatar: string;
	goodId: string;
	addon: string;
	score: number;
	images: string;
	contentBody: string;

	imageFiles: FileListType[];
}