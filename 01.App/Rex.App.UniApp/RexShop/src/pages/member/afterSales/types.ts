// 售后单明细
export interface AfterSaleDetailType {
	id: string;
	sn: string;
	bn: string;
	name: string;
	price: number;
	imageUrl: string;
	nums: number;
	amount: number;
	promotionAmount: number;
	addon: string;
    checked: boolean;
	disabled: boolean;
}