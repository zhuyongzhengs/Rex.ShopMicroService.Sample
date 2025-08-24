// 门店[列表]
declare type StoreType = {
	id: string;
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
	distanceDisplay: string;

	concurrencyStamp: string;
	creationTime: string;
};

interface StoreTableType extends TableType {
	data: StoreType[];
}

declare interface StoreState {
	tableData: StoreTableType;
}