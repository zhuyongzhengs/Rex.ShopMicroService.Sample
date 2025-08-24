// 购物车
declare type CartType = {
	userId: string;
	productId: string;
	nums: number;
	type: number;
	objectId: string | null;
};

// 购物车[列表]
declare type ShoppingCartType = {
	id: string;
	userId: string;
	goodId: string;
	goodName: string;
	productId: string;
	barCode: string;
	sn: string;
	price: number;
	stock: number;
	images: string | null;
	spesDesc: string | null;
	nums: number;
	type: number;
	objectId: string | null;
	isStockSufficient: boolean;
	isChecked: boolean;
};

interface CartsTableType extends TableType {
	data: CartsType[];
}

declare interface CartsState {
	tableData: CartsTableType;
}