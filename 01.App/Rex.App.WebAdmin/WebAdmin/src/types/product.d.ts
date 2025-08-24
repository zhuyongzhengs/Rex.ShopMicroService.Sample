declare type RowProductDistributionType = {
	id: string;
	tenantId: string;

	productId: string;
	// product: RowProductType;
	productSn: string;
	levelOne: number;
	levelTwo: number;
	levelThree: number;
}

declare type RowProductStockDetailType = {
	id: string;
	goodId: string;
	barCode: string;
	sn: string;
	price: number;
	costPrice: number;
	mktPrice: number;
	weight: number;
	stock: number;
	freezeStock: number;
	remainingStock: number;
	spesDesc: string;
	isDefault: boolean;
	images: string;
}

// product
declare type RowProductType = {
	id: string;
	tenantId: string;
	
	goodId: string;
	// good: RowGoodsType;
	barCode: string;
	isDefault: boolean;
	images: string;
	sn: string;
	spesDesc: string;
	weight: number;
	stock: number;
	freezeStock: number;
	price: number;
	costPrice: number;
	mktPrice: number;
	marketable: boolean;
	productDistribution: RowProductDistributionType;
	concurrencyStamp: string;
	creationTime: string;
	
	// start：Ext
	// bn: string;
	// name: string;
	// isMarketable: boolean;
	// unit: string;
	// totalStock: number;
	// gradePrice: RowGoodGradeType[];
	// gradeInfo: any;
	// amount: number;
	// promotionAmount: number;
	// buyPinTuanCount: number;
	// buyPromotionCount: number;
	// pinTuanRule: RowPinTuanRuleType;
	levelOne: number;
	levelTwo: number;
	levelThree: number;
	// end：Ext
};

interface ProductTableType extends TableType {
	data: RowProductType[];
}

declare interface ProductState {
	tableData: ProductTableType;
}
