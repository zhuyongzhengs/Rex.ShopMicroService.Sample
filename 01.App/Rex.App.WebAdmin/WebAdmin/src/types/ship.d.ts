// AreaFee
declare type RowAreaFeeType = {
	areas: string;
	displayArea: any;
	areaFirstunitPrice: string;
	areaContinueunitPrice: string;
};

// Ship
declare type RowShipType = {
	id: string;
	tenantId: string;

	name: string;
	isCashOnDelivery: boolean;
	firstUnit: number;
	continueUnit: number;
	isdefaultAreaFee: boolean;
	areaType: number;
	firstunitPrice: number;
	continueunitPrice: number;
	exp: string;
	logiName: string;
	logiCode: string;
	isDefault: boolean;
	sort: number;
	status: number;
	isfreePostage: boolean;
	areaFees: RowAreaFeeType[];
	goodsMoney: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface ShipTableType extends TableType {
	data: RowShipType[];
}

declare interface ShipState {
	tableData: ShipTableType;
}
