// orderLog
declare type RowOrderLogType = {
	id: string;
	tenantId: string;

	orderId: string;
	userId: string;
	type: number;
	typeDisplay: string;
	msg: string;
	data: string;
	
	concurrencyStamp: string;
	creationTime: string;
};

interface OrderLogTableType extends TableType {
	data: RowOrderLogType[];
}

declare interface OrderLogState {
	tableData: OrderLogTableType;
}
