// articleType
declare type RowArticleTypeType = {
	id: string;
	tenantId: string;

	name: string;
	parentId: string;
	sort: number;

	concurrencyStamp: string;
	creationTime: string;
};

interface ArticleTypeTableType extends TableType {
	data: RowArticleTypeType[];
}

declare interface ArticleTypeState {
	tableData: ArticleTypeTableType;
}
