// storeClerks
declare type RowStoreClerkType = {
	id: string;
	tenantId: string;
	
	storeId: string;
	store: RowStoreType;
	userId: string;
	userName: string;
	avatar: string;
	phoneNumber: string;

	concurrencyStamp: string;
	creationTime: string;
};

interface StoreClerkTableType extends TableType {
	data: RowStoreClerkType[];
}

declare interface StoreClerkState {
	tableData: StoreClerkTableType;
}
