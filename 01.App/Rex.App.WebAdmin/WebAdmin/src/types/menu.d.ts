// (菜单)元信息
declare type MetaType = {
	title: string;
	icon: string;
	isHide: boolean;
	isKeepAlive: boolean;
	isAffix: boolean;
	link: string;
	isIframe: string;
	roles?: string[];
};

// 菜单
declare type RowMenuType = {
	tenantId: string;
	id: string;
	pId: string;
	menuType: number;
	name: string;
	component: string;
	componentAlias: string;
	isLink: boolean;
	menuSort: number;
	path: string;
	redirect: string;
	permissionIdentifying: string;
	concurrencyStamp: string;
	meta: MetaType;
	children: RowMenuType[];
};

interface SysMenuTableType extends TableType {
	data: RowMenuType[];
}

declare interface SysMenuState {
	tableData: SysMenuTableType;
}