// payment
declare type RowPaymentType = {
	id: string;
	tenantId: string;
	code: string;
	name: string;
	isOnline: boolean;
	parameters: string;
	sort: number;
	memo: string;
	isEnable: boolean;
	
	concurrencyStamp: string;
	creationTime: string;
};

interface PaymentTableType extends TableType {
	data: RowPaymentType[];
}

declare interface PaymentState {
	tableData: PaymentTableType;
}
