// stores
declare type RowStoreType = {
	id: number;
	tenantId: string;
	
	storeName: string;
	mobile: string;
	linkMan: string;
	logoImage: string;
	areaId: number;
	address: string;
	coordinate: string;
	latitude: string;
	longitude: string;
	isDefault: boolean;
	distance: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface StoreTableType extends TableType {
	data: RowStoreType[];
}

declare interface StoreState {
	tableData: StoreTableType;
}
