// Logistics
declare type RowLogisticsType = {
	id: string;
	tenantId: string;

	logiCode: string;
	logiName: string;
	imgUrl: string;
	phone: string;
	url: string;
	sort: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface LogisticsTableType extends TableType {
	data: RowLogisticsType[];
}

declare interface LogisticsState {
	tableData: LogisticsTableType;
}
