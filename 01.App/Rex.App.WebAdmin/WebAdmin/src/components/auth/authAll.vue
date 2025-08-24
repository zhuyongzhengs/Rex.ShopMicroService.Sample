<template>
	<slot v-if="getUserAuthBtnList" />
</template>

<script setup lang="ts" name="authAll">
import { computed } from 'vue';
import { storeToRefs } from 'pinia';
import { useCurrentSysUser } from '/@/stores/currentSysUser';
import { judementSameArr } from '/@/utils/arrayOperation';

// 定义父组件传过来的值
const props = defineProps({
	value: {
		type: Array,
		default: () => []
	}
});

// 定义变量内容
const stores = useCurrentSysUser();
const { currentSysUsers } = storeToRefs(stores);

// 获取 pinia 中的用户权限
const getUserAuthBtnList = computed(() => {
	return judementSameArr(props.value, currentSysUsers.value.authBtnList);
});
</script>
