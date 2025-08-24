// treeAreas
declare type AreaTreeType = {
	id: number;
	tenantId: string;
	
	parentId: number;
	depth: number;
	name: string;
	postalCode: string;
	sort: number;
	leaf: boolean;
	children: AreaTreeType[];

	concurrencyStamp: string;
	creationTime: string;
};


// areas
declare type RowAreaType = {
	id: number;
	tenantId: string;
	
	parentId: number;
	depth: number;
	name: string;
	postalCode: string;
	sort: number;
	parentArea: RowAreaType;

	concurrencyStamp: string;
	creationTime: string;
};

interface AreaTableType extends TableType {
	data: RowAreaType[];
}

declare interface AreaState {
	tableData: AreaTableType;
}
