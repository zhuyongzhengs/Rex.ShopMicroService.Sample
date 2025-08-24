// billAftersalesDtl
declare type RowBillAftersalesDtlType = {
	id: string;

	no: string;
	orderId: string;
	orderNo: string;
	userId: string;
	userName: string;
	type: number;
	typeDisplay: string;
	refundAmount: string;
	status: number;
	statusDisplay: string;
	reason: string;
	mark: string;

	images: string[];
	productItems: RowBillAftersalesDtlItemType[];
};

// billAftersalesDtlItem
declare type RowBillAftersalesDtlItemType = {
	id: string;

	aftersalesId: string;
	orderItemId: string;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	imageUrl: string;
	amount: number;
	buyNums: number;
	sendNums: number;
	nums: number;
	addon: number;
	checked: number;
};