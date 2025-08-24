// goodBrand
declare type RowGoodBrandType = {
	id: string;
	tenantId: string;
	name: string;
	logoImageUrl: string;
	sort: number;
	isShow: boolean;
	concurrencyStamp: string;
	creationTime: string;
};

interface GoodBrandTableType extends TableType {
	data: RowGoodBrandType[];
}

declare interface GoodBrandState {
	tableData: GoodBrandTableType;
}
