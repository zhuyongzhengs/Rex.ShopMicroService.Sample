// 测试Pinia
import { ref } from 'vue';
import { defineStore } from 'pinia';
 
export const useCounterPersistStore = defineStore('counterPersist',
	() => {
		const countVal = ref(0)
    	return { countVal }
	},
	{
		// persist: true,
		persist: {
			// 调整为兼容多端的API
			storage: {
				setItem(key, value) {
					uni.setStorageSync(key, value);
				},
				getItem(key) {
					return uni.getStorageSync(key);
				},
			}
		},
	}
);