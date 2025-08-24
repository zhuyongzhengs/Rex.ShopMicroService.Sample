// 退货单
declare type BillAftersalesType = {
	id: string;
	tenantId: string;
	
	no: string;
	orderId: string;
	order: OrderType | null;
	userId: string;
	type: number;
	typeDisplay: string;
	refundAmount: number;
	status: number;
	statusDisplay: string;
	reason: string;
	mark: string;
	billAftersalesItems: BillAftersalesItemType[] | null;
	billAftersalesImagess: BillAftersalesImagesType[] | null;

	concurrencyStamp: string;
	creationTime: string;
};

// 退货单图片关联
declare type BillAftersalesImagesType = {
	id: string;
	tenantId: string;
	
	aftersalesId: string;
	aftersales: BillAftersalesType | null;
	imageUrl: string;
	sort: number;

	concurrencyStamp: string;
	creationTime: string;
};

// 售后单明细
declare type BillAftersalesItemType = {
	id: string;
	tenantId: string;
	
	aftersalesId: string;
	aftersales: BillAftersalesType | null;
	orderItemId: string;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	imageUrl: string;
	nums: number;
	addon: string;

	concurrencyStamp: string;
	creationTime: string;
};

// 退货单信息
declare type BillAftersalesDetailType = {
	id: string;
	tenantId: string;
	
	no: string;
	orderId: string;
	order: OrderType | null;
	userId: string;
	type: number;
	typeDisplay: string;
	refundAmount: number;
	status: number;
	statusDisplay: string;
	reason: string;
	mark: string;
	billAftersalesItems: BillAftersalesItemType[] | null;
	billAftersalesImagess: BillAftersalesImagesType[] | null;
	billRefund: BillRefundType | null;
	billReship: BillReshipType | null;

	concurrencyStamp: string;
	creationTime: string;
};