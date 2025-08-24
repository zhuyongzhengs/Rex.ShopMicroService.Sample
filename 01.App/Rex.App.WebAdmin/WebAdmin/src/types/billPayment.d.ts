// billPayment
declare type RowBillPaymentType = {
	id: string;
	tenantId: string;

	no: string;
	sourceId: string;
	money: number;
	userId: string;
	userName: string;
	type: number;
	status: number;
	statusDisplay: string;
	paymentCode: string;
	ip: string;
	parameters: string;
	payedMsg: string;
	tradeNo: string;
	
	concurrencyStamp: string;
	creationTime: string;
};

interface BillPaymentTableType extends TableType {
	data: RowBillPaymentType[];
}

declare interface BillPaymentState {
	tableData: BillPaymentTableType;
}
