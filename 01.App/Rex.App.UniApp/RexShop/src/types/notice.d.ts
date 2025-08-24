// 通知
declare type NoticeType = {
	id: string;
	tenantId: string;

	title: string;
	contentBody: string;
	type: number;
	sort: number;
	
	concurrencyStamp: string;
	creationTime: string;
}
