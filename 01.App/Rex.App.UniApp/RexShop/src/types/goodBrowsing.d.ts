// 用户足迹
declare type GoodBrowsingType = {
	id: string;

	goodId: string;
	userId: string;
	goodName: string;
	image: string;

	creationTime: string;
};

interface GoodBrowsingTableType extends TableType {
	data: GoodBrowsingType[];
}

declare interface GoodBrowsingTypeState {
	tableData: GoodBrowsingTableType;
}