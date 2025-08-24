// layoutItem
declare type LayoutItem = {
	type: string;
	value: any;
};

// pageDesignItem
declare type RowPageDesignItemType = {
	id: string;
	tenantId: string?;

	widgetCode: string;
	pageCode: string;
	positionId: number;
	sort: number;
	parameters: any;

	concurrencyStamp: string;
};

interface PageDesignItemTableType extends TableType {
	data: RowPageDesignItemType[];
}

declare interface PageDesignItemState {
	tableData: PageDesignItemTableType;
}
