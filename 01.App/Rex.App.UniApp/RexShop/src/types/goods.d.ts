// 品牌
declare type GoodBrandType = {
	id: string;
	tenantId: string;
	name: string;
	logoImageUrl: string;
	sort: number;
	isShow: boolean;
	concurrencyStamp: string;
	creationTime: string;
};

// 商品分类
declare type GoodCategoryType = {
	tenantId: string;
	id: string;
	parentId: string;
	menuType: number;
	name: string;
	typeId: string;
	sort: number;
	imageUrl: string;
	isShow: boolean;
	concurrencyStamp: string;
	creationTime: string;
	children: GoodCategoryType[];
};

// 货品三级佣金
declare type ProductDistributionType = {
	id: string;
	tenantId: string;

	productId: string;
	productSn: string;
	levelOne: number;
	levelTwo: number;
	levelThree: number;
}

// 货品
declare type ProductType = {
	id: string;
	tenantId: string;
	
	goodId: string;
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
	productDistribution: ProductDistributionType;
	concurrencyStamp: string;
	creationTime: string;
	
	// start：Ext
	levelOne: number;
	levelTwo: number;
	levelThree: number;
	// end：Ext
};

// 商品参数
declare type GoodParamType = {
	id: string;
	tenantId: string;
	name: string;
	values: string[];
	type: string;
	concurrencyStamp: string;
	creationTime: string;
};

// 用户商品评价
declare type UserGoodCommentType = {
	id: string;
	userId: string;
	userName: string;
	avatar: string;
	score: number;
	goodId: string;
	addon: string;
	images: string;
	contentBody: string;
	creationTime: string;
};

// 商品
declare type GoodsType = {
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
	goodCategory: GoodCategoryType;
	goodTypeId: string;
	goodSkuIds: string;
	goodParamsIds: string;
	brandId: string;
	brand: GoodBrandType;
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
	products: ProductType[]
	// end

	// ext：start
	price: number;
	costPrice: number;
	mktPrice: number;
	isFav: boolean;
	// ext：end

	concurrencyStamp: string;
	lastModificationTime: string;
	creationTime: string;
};

interface GoodsTableType extends TableType {
	data: GoodsType[];
}

declare interface GoodsState {
	tableData: GoodsTableType;
}