// 用户(收货)地址
declare type UserShipType = {
	id: string;
	tenantId: string | null;

	userId: string | undefined;
	areaId: number;
	displayArea: string;
	address: string;
	name: string;
	mobile: string;
	isDefault: boolean;

	concurrencyStamp: string;
};

interface UserShipTableType extends TableType {
	data: UserShipType[];
}

declare interface UserShipTypeState {
	tableData: UserShipTableType;
}