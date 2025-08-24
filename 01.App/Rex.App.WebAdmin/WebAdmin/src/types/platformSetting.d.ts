// SettingValue
declare type SettingValueType = {
	label: string;
	name: string;
	value: string;
};

// PlatformSetting
declare type PlatformSettingType = {
	specialSwitchs: SettingValueType[];
	platformSettings: SettingValueType[];
	shareSettings: SettingValueType[];
	usersSettings: SettingValueType[];
	goodsSettings: SettingValueType[];
	orderSettings: SettingValueType[];
	pointsSettings: SettingValueType[];
	cashSettings: SettingValueType[];
	inviteFriendSettings: SettingValueType[];
	fileStorageSettings: SettingValueType[];
	weChatMiniPrograms: SettingValueType[];
	weChatPays: SettingValueType[];
	otherSettings: SettingValueType[];
};
