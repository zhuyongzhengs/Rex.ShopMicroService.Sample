<template>
	<slot v-if="getUserAuthBtnList" />
</template>

<script setup lang="ts" name="auth">
import { computed } from 'vue';
import { storeToRefs } from 'pinia';
import { useCurrentSysUser } from '/@/stores/currentSysUser';

// 定义父组件传过来的值
const props = defineProps({
	value: {
		type: String,
		default: () => ''
	}
});

// 定义变量内容
const stores = useCurrentSysUser();
const { currentSysUsers } = storeToRefs(stores);

// 获取 pinia 中的用户权限
const getUserAuthBtnList = computed(() => {
	return currentSysUsers.value.authBtnList.some((v: string) => v === props.value);
});
</script>
