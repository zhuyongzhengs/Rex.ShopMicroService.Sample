// 商品分类
declare type RowGoodCategoryType = {
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
	children: RowGoodCategoryType[]
};

interface GoodCategoryTableType extends TableType {
	data: RowGoodCategoryType[];
}

declare interface GoodCategoryState {
	tableData: GoodCategoryTableType;
}