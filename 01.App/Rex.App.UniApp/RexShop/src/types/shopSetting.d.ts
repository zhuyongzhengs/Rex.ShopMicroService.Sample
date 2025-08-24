// 商城服务描述
declare type ServiceDescriptionType = {
	id: string;
	tenantId: string;

	title: string;
	type: number;
	description: string;
	isShow: boolean;
	sort: number;

	concurrencyStamp: string;
};