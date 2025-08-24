// userWeChat
declare type RowUserWeChatType = {
	id: string;
	tenantId: string;
	type: number;
	userId: string;
	userName: string;
	openId: string;
	sessionKey: string;
	unionId: string;
	avatar: string;
	nickName: string;
	gender: number;
	city: string;
	province: string;
	country: string;
	countryCode: string;
	mobile: string;
	creationTime: string;
	concurrencyStamp: string;
};

interface MemberUserWeChatTableType extends TableType {
	data: RowUserWeChatType[];
}

declare interface MemberUserWeChatState {
	tableData: MemberUserWeChatTableType;
}
