// goodTypeSpecValue
declare type RowGoodTypeSpecValueType = {
	id: string?;
	tenantId: string;
	specId: string;
	value: string;
	sort: number;
};

// goodTypeSpec
declare type RowGoodTypeSpecType = {
	id: string;
	tenantId: string;
	name: string;
	sort: number;
	specValues: RowGoodTypeSpecValueType[];
	concurrencyStamp: string;
	creationTime: string;
};

interface GoodTypeSpecTableType extends TableType {
	data: RowGoodTypeSpecType[];
}

declare interface GoodTypeSpecState {
	tableData: GoodTypeSpecTableType;
}
