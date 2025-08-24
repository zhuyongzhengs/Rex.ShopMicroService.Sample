// pinTuanRecord
declare type RowPinTuanRecordType = {
	id: string;
	tenantId: string;
	
	teamId: string;
	userId: string;
	ruleId: string;
	goodId: string;
	status: number;
	orderId: string;
	parameters: string;
	closeTime: string;

	concurrencyStamp: string;
	creationTime: string;
};

interface PinTuanRecordTableType extends TableType {
	data: RowPinTuanRecordType[];
}

declare interface PinTuanRecordState {
	tableData: PinTuanRecordTableType;
}
