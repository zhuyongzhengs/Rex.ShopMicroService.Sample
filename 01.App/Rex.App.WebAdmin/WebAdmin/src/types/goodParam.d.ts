// param
declare type RowGoodParamType = {
	id: string;
	tenantId: string;
	name: string;
	values: string[];
	type: string;
	concurrencyStamp: string;
	creationTime: string;
};

interface GoodParamTableType extends TableType {
	data: RowGoodParamType[];
}

declare interface GoodParamState {
	tableData: GoodParamTableType;
}
