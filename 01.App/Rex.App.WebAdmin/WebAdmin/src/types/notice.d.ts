// notice
declare type RowNoticeType = {
	id: string;
	tenantId: string;

	title: string;
	contentBody: string;
	type: number;
	sort: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface NoticeTableType extends TableType {
	data: RowNoticeType[];
}

declare interface NoticeState {
	tableData: NoticeTableType;
}
