// billRefund
declare type RowBillRefundType = {
	id: string;
	tenantId: string;

	no: string;
	billAftersalesId: string;
	money: number;
	userId: string;
	sourceId: string;
	type: number;
	typeDisplay: string;
	paymentCode: string;
	tradeNo: string;
	status: number;
	statusDisplay: string;
	memo: string;
	
	concurrencyStamp: string;
	creationTime: string;
};

// billRefundMany
declare type RowBillRefundManyType = RowBillRefundType &{
	billAftersalesNo: string;
	sourceNo: string;
	userName: string;
};

interface BillRefundTableType extends TableType {
	data: RowBillRefundManyType[];
}

declare interface BillRefundState {
	tableData: BillRefundTableType;
}
