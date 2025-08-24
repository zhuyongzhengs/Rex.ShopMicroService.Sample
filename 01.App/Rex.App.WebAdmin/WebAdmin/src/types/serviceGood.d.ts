// serviceGoods
declare type RowServiceGoodType = {
	id: string;
	tenantId: string;
	
	title: string;
	thumbnail: string;
	description: string;
	contentBody: string;
	allowedMemberships: string[];
	consumableStores: string[];
	status: number;
	maxBuyNumber: number;
	amount: number;
	startTime: string;
	endTime: string;
	validityType: number;
	validityStartTime: string;
	validityEndTime: string;
	ticketNumber: number;
	money: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface ServiceGoodTableType extends TableType {
	data: RowServiceGoodType[];
}

declare interface ServiceGoodState {
	tableData: ServiceGoodTableType;
}
