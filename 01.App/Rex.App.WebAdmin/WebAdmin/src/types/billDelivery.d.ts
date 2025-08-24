// billDeliveryUpdate
declare type BillDeliveryUpdateType = {
	logiCode: string;
	logiName: string;
	logiNo: string;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	memo: string;
};

// billDelivery
declare type RowBillDeliveryType = {
	id: string;
	tenantId: string;

	no: string;
	orderId: string;
	order: RowOrderType;
	logiCode: string;
	logiName: string;
	logiNo: string;
	logiInformation: string;
	logiStatus: boolean;
	shipAreaId: number;
	displayArea: string;
	shipAddress: string;
	shipName: string;
	shipMobile: string;
	status: number;
	statusDisplay: string;
	memo: string;
	confirmTime: string;
	billDeliveryItems: RowBillDeliveryItemType[];
	
	concurrencyStamp: string;
	creationTime: string;
};

// billDeliveryItem
declare type RowBillDeliveryItemType = {
	id: string;
	tenantId: string;

	no: string;
	orderId: string;
	deliveryId: string;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	nums: number;
	weight: number;
	addon: number;
	
	concurrencyStamp: string;
	creationTime: string;
};

interface BillDeliveryTableType extends TableType {
	data: RowBillDeliveryType[];
}

declare interface BillDeliveryState {
	tableData: BillDeliveryTableType;
}
