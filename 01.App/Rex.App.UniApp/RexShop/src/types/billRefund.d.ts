// 退款单
declare type BillRefundType = {
	id: string;
	tenantId: string;
	
	no: string;
	billAftersalesId: string;
	money: number;
	userId: string;
	sourceId: string;
	type: number;
	paymentCode: string;
	tradeNo: string;
	status: number;
	statusDisplay: string;
	memo: string;

	concurrencyStamp: string;
	creationTime: string;
};