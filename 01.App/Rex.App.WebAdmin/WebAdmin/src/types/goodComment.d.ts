// goodComment
declare type RowGoodCommentType = {
	id: string;
	tenantId: string;
	commentId: string;
	score: number;
	userId: string;
	userName: string;
	avatar: string;
	phoneNumber: string;
	goodId: string;
	goodName: string;
	orderId: string;
	orderNo: string;
	shipName: string;
	shipMobile: string;
	addon: string;
	images: string;
	contentBody: string;
	sellerContent: string;
	isDisplay: boolean;
	
	concurrencyStamp: string;
	creationTime: string;
};

interface GoodCommentTableType extends TableType {
	data: RowGoodCommentType[];
}

declare interface GoodCommentState {
	tableData: GoodCommentTableType;
}
