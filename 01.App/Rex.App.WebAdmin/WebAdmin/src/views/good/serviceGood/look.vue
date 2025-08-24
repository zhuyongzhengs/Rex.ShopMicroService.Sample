<template>
  <div class="serviceGood-look-container layout-padding">
    <service-good-handle :id="goodId" :isLook="true" ref="serviceGoodHandleRef" />
  </div>
</template>

<script setup lang="ts" name="serviceGoodCreate">
import { ref, defineAsyncComponent, onBeforeMount } from "vue";
import { useRoute } from "vue-router";
// 定义变量内容
const route = useRoute();

// 引入组件
const ServiceGoodHandle = defineAsyncComponent(
  () => import("/@/views/good/serviceGood/components/serviceGoodHandle.vue")
);

// 商品ID
let goodId = ref("");
onBeforeMount(() => {
  if (route.query.id) goodId.value = String(route.query.id);
});
</script>

<style scoped lang="scss">
.serviceGood-look-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
}
</style>
