// pageDesign
declare type RowPageDesignType = {
	id: string;
	tenantId: string;
	code: string;
	name: string;
	description: string;
	layout: number;
	type: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface PageDesignTableType extends TableType {
	data: RowPageDesignType[];
}

declare interface PageDesignState {
	tableData: PageDesignTableType;
}

// layoutDesign
declare type LayoutDesignType = {
	pageDesign: RowPageDesignType;
	brands: RowGoodBrandType[];
	goodCategoryTrees: RowGoodCategoryType[];
	articleTypes: RowArticleTypeType[];
};
