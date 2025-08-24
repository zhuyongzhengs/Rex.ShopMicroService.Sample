import { ref } from 'vue';
import { defineStore } from 'pinia';
import { http } from "@/utils/http";
 
export const useUserLoginStore = defineStore('userLogin', {
	state: () => {
		const showMpLoginTip = ref(false); // 显示[小程序]登录框
		const goSmsAuthLogin = ref(false); // 进入[短信授权]登录页
		const hasLogin = ref(false); // 用于验证是否登录
		const userAuthToken = ref<LoginResultType | undefined>(); // 用户授权令牌
		const currentSysUser = ref<CurrentSysUserType | undefined>(); // 用户信息
    	return { 
			showMpLoginTip,
			goSmsAuthLogin,
			hasLogin,
			userAuthToken,
			currentSysUser
		};
	},
	actions: {
		// 显示[授权]登录
		showAuthLogin() {
			// #ifdef MP-WEIXIN
			this.openWxLoginTip();
			// #endif
			// #ifdef H5
			this.jumpSmsLogin();
			// #endif
		},
		// 打开[微信小程序]登录框
		openWxLoginTip() {
			this.showMpLoginTip = true;
		},
		// 关闭[微信小程序]登录框
		closeWxLoginTip() {
			this.showMpLoginTip = false;
		},
		// 跳转到短信登录页
		jumpSmsLogin() {
			uni.navigateTo({
				url: '/pages/login/index'
			});
		},
		// 获取用户信息
		async getCurrentSysUser() {
			const uInfo = await http<CurrentSysUserType>({
				method: "GET",
				url: "/api/base/common/current-user",
			});
			if (uInfo) {
				this.currentSysUser = uInfo;
			}
		},
		// 刷新用户信息
		async refreshCurrentSysUser(delay: number = 0) {
			let me = this;
			if(!delay || delay <= 0) delay = 50;
			setTimeout(async () => {
				const uInfo = await http<CurrentSysUserType>({
					method: "PUT",
					url: "/api/base/common/current-user",
				});
				if (uInfo) {
					me.currentSysUser = uInfo;
				}
			}, delay);
		},
		// 退出登录
		logout() {
			this.hasLogin = false;
			this.userAuthToken = undefined;
			this.currentSysUser = undefined;
		}
	},
	persist: { // 开启持久化 --> persist: true
		// 调整为兼容多端的API
		storage: {
			setItem(key, value) {
				uni.setStorageSync(key, value);
			},
			getItem(key) {
				return uni.getStorageSync(key);
			},
		}
	}
});