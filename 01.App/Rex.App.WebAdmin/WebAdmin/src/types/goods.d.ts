// goods
declare type RowGoodsType = {
	id: string;
	tenantId: string;
	
	// start
	barCode: string;
	name: string;
	brief: string;
	image: string;
	images: string;
	video: string;
	productsDistributionType: number;
	goodCategoryId: string;
	goodCategory: RowGoodCategoryType;
	goodTypeId: string;
	goodSkuIds: string;
	goodParamsIds: string;
	brandId: string;
	brand: RowGoodBrandType;
	isNomalVirtual: boolean;
	isMarketable: boolean;
	unit: string;
	intro: string;
	spesDesc: string;
	parameters: string;
	commentsCount: number;
	viewCount: number;
	buyCount: number;
	uptime: string;
	downtime: string;
	sort: number;
	labelIds: string;
	newSpec: string;
	openSpec: number;
	isRecommend: boolean;
	isHot: boolean;
	products: RowProductType[]
	// end

	// ext：start
	// sn: string;
	price: number;
	costPrice: number;
	mktPrice: number;
	// stock: number;
	// freezeStock: number;
	// weight: number;
	// album: string[];
	// isFav: string;
	// pinTuanRule: RowPinTuanRuleType;
	// pinTuanPrice: number;
	// pinTuanRecord: RowPinTuanRecordType;
	// pinTuanRecordNums: number;
	// buyPinTuanCount: number;
	// buyPromotionCount: number;
	// labels: RowLabelType[];
	// groupId: string;
	// groupType: number;
	// groupStatus: boolean;
	// groupTime: string;
	// groupStartTime: string;
	// groupEndTime: string;
	// groupTimestamp: number;
	// ext：end

	concurrencyStamp: string;
	lastModificationTime: string;
	creationTime: string;
};

interface GoodsTableType extends TableType {
	data: RowGoodsType[];
}

declare interface GoodsState {
	tableData: GoodsTableType;
}
