// article
declare type RowArticleType = {
	id: string;
	tenantId: string;

	title: string;
	brief: string;
	coverImage: string;
	contentBody: string;
	typeId: string;
	articleType: RowArticleTypeType;
	sort: number;
	isPub: boolean;
	pv: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface ArticleTableType extends TableType {
	data: RowArticleType[];
}

declare interface ArticleState {
	tableData: ArticleTableType;
}
