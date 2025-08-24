// 文章分类
declare type ArticleClassifyType = {
	id: string;
	tenantId: string;

	name: string;
	parentId: string;
	sort: number;
	
	concurrencyStamp: string;
	creationTime: string;
}

// 文章
declare type ArticleType = {
	id: string;
	tenantId: string;

	title: string;
	brief: string;
	coverImage: string;
	contentBody: string;
	typeId: string;
	articleType: ArticleClassifyType | null;
	sort: number;
	isPub: string;
	pv: number;
	
	concurrencyStamp: string;
	creationTime: string;
}
