// label
declare type RowLabelType = {
	id: string;
	tenantId: string;
	
	name: string;
	style: string;

	concurrencyStamp: string;
	creationTime: string;
};

interface LabelTableType extends TableType {
	data: RowLabelType[];
}

declare interface LabelState {
	tableData: LabelTableType;
}
