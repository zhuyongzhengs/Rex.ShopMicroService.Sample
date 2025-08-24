// serviceDescription
declare type RowServiceDescriptionType = {
	id: string;
	tenantId: string;

	title: string;
	type: number;
	description: string;
	isShow: boolean;
	sort: number;

	concurrencyStamp: string;
};

interface ServiceDescriptionTableType extends TableType {
	data: RowServiceDescriptionType[];
}

declare interface ServiceDescriptionState {
	tableData: ServiceDescriptionTableType;
}
