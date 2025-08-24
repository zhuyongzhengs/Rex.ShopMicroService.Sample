
// 区域
declare type AreaType = {
	id: number;
	tenantId: string;
	
	parentId: number;
	depth: number;
	name: string;
	postalCode: string;
	sort: number;
	parentArea: AreaType;

	concurrencyStamp: string;
	creationTime: string;
};

// treeAreas
declare type AreaTreeType = {
	id: number;
	tenantId: string;
	
	parentId: number;
	depth: number;
	name: string;
	postalCode: string;
	sort: number;
	children: AreaTreeType[];

	concurrencyStamp: string;
	creationTime: string;
};