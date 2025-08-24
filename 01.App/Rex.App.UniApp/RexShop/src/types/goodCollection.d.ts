// 用户收藏
declare type GoodCollectionType = {
	id: string;

	goodId: string;
	userId: string;
	goodName: string;
	image: string;

	creationTime: string;
};

interface GoodCollectionTableType extends TableType {
	data: GoodCollectionType[];
}

declare interface GoodCollectionTypeState {
	tableData: GoodCollectionTableType;
}