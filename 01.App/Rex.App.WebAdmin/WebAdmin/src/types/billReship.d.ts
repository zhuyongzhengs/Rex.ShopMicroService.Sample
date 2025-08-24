// billReship
declare type RowBillReshipType = {
	id: string;
	tenantId: string;

	no: string;
	orderId: string;
	order: RowOrderType | null;
	aftersalesId: string;
	aftersales: RowBillAftersalesType | null;
	userId: string;
	logiCode: string;
	logiNo: string;
	status: number;
	statusDisplay: string;
	memo: string;
	billReshipItems: RowBillReshipItemType[] | null;
	
	concurrencyStamp: string;
	creationTime: string;
	lastModificationTime: string;
};

// billReshipMany
declare type RowBillReshipManyType = RowBillReshipType & {
	userName: string;
	orderNo: string;
	aftersalesNo: string;
};

declare type RowBillReshipItemType = {
	id: string;
	tenantId: string;

	reshipId: string;
	reship: RowBillReshipType | null;
	orderItemId: string;
	goodId: string;
	productId: string;
	sn: string;
	bn: string;
	name: string;
	imageUrl: string;
	nums: number;
	addon: string;

	concurrencyStamp: string;
	creationTime: string;
}

interface BillReshipTableType extends TableType {
	data: RowBillReshipManyType[];
}

declare interface BillReshipState {
	tableData: BillReshipTableType;
}
