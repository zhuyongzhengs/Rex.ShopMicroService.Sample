// 空白图框
const default_banner = "/src/assets/design/empty-banner.png";
const default_empty = "/src/assets/design/empty.png";

/**
 * 默认商品值
 */
export function getDefaultGoods() {
	return [
		{
		  image: default_banner,
		  name: "",
		  price: "",
		},
		{
		  image: default_banner,
		  name: "",
		  price: "",
		},
		{
		  image: default_banner,
		  name: "",
		  price: "",
		},
		{
		  image: default_banner,
		  name: "",
		  price: "",
		}
	]
}

/**
 * 媒体组件
 */
export function getMediaComponents() {
	return [
		{
			type: "imgSlide",
			name: "图片轮播",
			value: {
				duration: 2500,
				height: 175,
				list: [
					{
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					}
				]
			},
			icon: "iconfont icon-tupianlunbo"
		},
		{
			type: "imgSingle",
			name: "图片",
			value: {
				list: [{
					image: default_banner,
					linkType: '',
					linkValue: '',
					buttonShow: false,
					buttonText: 'Go',
					buttonColor: "#409eff",
					textColor: "#ffffff",
					showLinkValue: '',
					placeholder: 'http开头为webview跳转，其他为站内页面跳转',
					readonly: false
				}]
			},
			icon: "iconfont icon-tupian"
		},
		{
			type: "imgWindow",
			name: "图片分组",
			value: {
				style: 2,  // 0 橱窗  2 两列 3三列 4四列
				margin: 4,
				list: [
					{
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					}, {
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_banner,
						linkType: '',
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					}
				]
			},
			icon: "iconfont icon-tupianfenzu"
		},
		{
			type: "video",
			name: "视频组",
			value: {
				autoplay: false,
				list: [{
					image: default_banner,
					url: "",
					linkType: '',
					linkValue: ''
				}]
			},
			icon: "iconfont icon-shipinzu"
		},
		{
			type: "article",
			name: "文章组",
			value: {
				list: [
					{
						id: null,
						coverImage: '',
						brief: '',
						title: ''
					}
				]
			},
			icon: "iconfont icon-wenzhangguanli"
		},
		{
			type: "articleClassify",
			name: "文章分类",
			value: {
				limit: 3,
				articleClassifyId: ''
			},
			icon: "iconfont icon-wenzhangfenlei"
		}
	]
}

/**
 * 商城组件
 */
export function getShopComponents() {
	return [
		{
			type: "search",
			name: "搜索框",
			value: {
				keywords: '请输入关键字搜索',
				style: 'round' // round:圆弧 radius:圆角 square:方形
			},
			icon: "iconfont icon-wxbsousuotuiguang"
		},
		{
			type: "notice",
			name: "公告组",
			value: {
				type: 'auto', //choose手动选择， auto 自动获取
				color: '#f9ae3d', // 文字颜色
				bgColor: '#fdf6ec', // 背景颜色
				list: [
					{
						title: "这里是第一条公告的标题",
						content: "",
						id: ''
					}
				]
			},
			icon: "iconfont icon-gonggao"
		},
		{
			type: "navBar",
			name: "导航组",
			value: {
				limit: 4,
				list: [
					{
						image: default_empty,
						text: "按钮1",
						linkType: 1,
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_empty,
						text: "按钮2",
						linkType: 1,
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_empty,
						text: "按钮3",
						linkType: 1,
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					},
					{
						image: default_empty,
						text: "按钮4",
						linkType: 1,
						linkValue: '',
						showLinkValue: '',
						placeholder: 'http开头为webview跳转，其他为站内页面跳转',
						readonly: false
					}
				]
			},
			icon: "iconfont icon-daohanglanmoshi01"
		},
		{
			type: "goods",
			name: "商品组",
			icon: "iconfont icon-shangpinzu",
			value: {
				title: '商品组名称',
				lookMore: true,
				type: 'auto', //auto自动获取  choose 手动选择
				classifyId: '', //所选分类id
				brandId: '', //所选品牌id
				limit: 10,
				display: 'list', //list , slide
				column: 2, //分裂数量
				list: [
					{
						image: default_banner,
						name: '',
						price: ''
					},
					{
						image: default_banner,
						name: '',
						price: ''
					},
					{
						image: default_banner,
						name: '',
						price: ''
					},
					{
						image: default_banner,
						name: '',
						price: ''
					}
				]
			},
		},
		{
			type: "goodTabBar",
			name: "商品选项卡",
			icon: "iconfont icon-xuanxiangka",
			value: {
				isFixedHead: true,//是否固定头部
				list: [
					{
						title: '选项卡名称一',
						subTitle: '子标题一',
						type: 'auto', //auto自动获取  choose 手动选择
						classifyId: '', //所选分类id
						brandId: '', //所选品牌id
						limit: 10,
						column: 2, //分裂数量
						isShow:true,
						list: [
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							}
						],
						hasChooseGoods: [],
					},
					{
						title: '选项卡名称二',
						subTitle: '子标题二',
						type: 'auto', //auto自动获取  choose 手动选择
						classifyId: '', //所选分类id
						brandId: '', //所选品牌id
						limit: 10,
						column: 2, //分裂数量
						isShow: true,
						list: [
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							},
							{
								image: default_banner,
								name: '',
								price: ''
							}
						],
						hasChooseGoods: [],
					}
				]
			},
		},
		{
			type: "groupPurchase",
			name: "商品秒杀",
			value: {
				title: '活动名称',
				limit: '10',
				list: [
					{
						id: '',
						image: default_banner,
						name: '',
						price: ''
					},
					{
						id: '',
						image: default_banner,
						name: '',
						price: ''
					},
				]
			},
			icon: "iconfont icon-yingxiaogongju-tuangoumiaosha1"
		},
		/*
		{
			type: "pinTuan",
			name: "拼团",
			value: {
				title: '活动名称',
				limit: '10',
				list: [
					{
						goodsImage: default_banner,
						name: '',
						price: ''
					},
					{
						goodsImage: default_banner,
						name: '',
						price: ''
					},
				]
			},
			icon: "iconfont icon-pintuan"
		},
		*/
		{
			type: "coupon",
			name: "优惠券组",
			value: {
				limit: 2
			},
			icon: "iconfont icon-youhuijuan"
		},
		/*
		{
			type: "service",
			name: "服务组",
			value: {
				title: '推荐服务卡',
				limit: '10',
				list: [
					{
						thumbnail: default_banner,
						title: '',
						money: ''
					},
					{
						thumbnail: default_banner,
						title: '',
						money: ''
					},
				]
			},
			icon: "iconfont icon-fuwuqi"
		},
		{
			type: "record",
			name: "购买记录",
			value: {
				style: {
					top: 20,
					left: 0
				}
			},
			icon: "iconfont icon-goumaijilu"
		}
		*/
	]
}

/**
 * 工具组件
 */
export function getUtilsComponents() {
	return [
        {
            type: "blank",
            name: "辅助空白",
            icon: 'iconfont icon-fuzhukongbai',
            value: {
                height: 20,
                backgroundColor: "#FFFFFF"
            },
        },
        {
            type: "textarea",
            name: "文本域",
            value: '',
            icon: 'iconfont icon-wenbenyu',
        }
	];
}

/**
 * 图片分组布局
 */
export function getImgWindowStyle() {
	return [
		{
			title: '1行2个',
			value: 2,
			image: "/src/assets/design/image-one-column.png"
		},
		{
			title: '1行3个',
			value: 3,
			image: "/src/assets/design/image-three-column.png"
		},
		{
			title: '1行4个',
			value: 4,
			image: "/src/assets/design/image-four-column.png"
		},
		{
			title: '1左3右',
			value: 0,
			image: "/src/assets/design/image-one-left.png"
		}
	]
}

/**
 * 获取选择的名称
 * @param type 类型
 * @returns 类型名称
 */
export function getSelectWgName(type: string) {
	let wgName = "";
	switch (type) {
	  case "imgSlide":
		wgName = "图片轮播";
		break;
	  case "imgSingle":
		wgName = "图片";
		break;
	  case "imgWindow":
		wgName = "图片分组";
		break;
	  case "video":
		wgName = "视频组";
		break;
	  case "article":
		wgName = "文章组";
		break;
	  case "articleClassify":
		wgName = "文章分类";
		break;
	  case "search":
		wgName = "搜索框";
		break;
	  case "notice":
		wgName = "公告组";
		break;
	  case "navBar":
		wgName = "导航组";
		break;
	  case "goods":
		wgName = "商品组";
		break;
	  case "goodTabBar":
		wgName = "商品选项卡";
		break;
	  case "groupPurchase":
		wgName = "商品秒杀";
		break;
	  case "pinTuan":
		wgName = "拼团";
		break;
	  case "service":
		wgName = "服务组";
		break;
	  case "coupon":
		wgName = "优惠券组";
		break;
	  case "record":
		wgName = "购买记录";
		break;
	  case "blank":
		wgName = "辅助空白";
		break;
	  case "textarea":
		wgName = "文本域";
		break;
	}
	return wgName;
}

/**
 * 统一批量导出
 * @method defaultBanner 默认空白横图框
 * @method defaultEmpty 默认空白图框
 * @method defaultGoods 默认商品
 * @method mediaComponents 媒体组件
 * @method shopComponents 商城组件
 * @method utilsComponents 工具组件
 * @method imgWindowStyle 图片分组布局
 * @method selectWgName 选择的名称
 */
const design = {
	defaultBanner: () => {
		return default_banner;
	},
	defaultEmpty: () => {
		return default_empty;
	},
	defaultGoods: () => {
		return getDefaultGoods();
	},
	mediaComponents: () => {
		return getMediaComponents();
	},
	shopComponents: () => {
		return getShopComponents();
	},
	utilsComponents: () => {
		return getUtilsComponents();
	},
	imgWindowStyle: () => {
		return getImgWindowStyle();
	},
	selectWgName: (type: string) => {
		return getSelectWgName(type);
	}
};

// 统一批量导出
export default design;
