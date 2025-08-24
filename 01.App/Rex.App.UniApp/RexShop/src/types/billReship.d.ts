// 退货单
declare type BillReshipType = {
	id: string;
	tenantId: string;
	
	no: string;
	orderId: string;
	aftersalesId: string;
	userId: string;
	logiCode: string;
	logiNo: string;
	status: number;
	statusDisplay: string;
	memo: string;

	concurrencyStamp: string;
	creationTime: string;
};