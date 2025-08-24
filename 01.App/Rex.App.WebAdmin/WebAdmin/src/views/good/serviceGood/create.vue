<template>
  <div class="serviceGood-create-container layout-padding">
    <service-good-handle ref="serviceGoodHandleRef" />
  </div>
</template>

<script setup lang="ts" name="serviceGoodCreate">
import { defineAsyncComponent } from "vue";
// 引入组件
const ServiceGoodHandle = defineAsyncComponent(
  () => import("/@/views/good/serviceGood/components/serviceGoodHandle.vue")
);
</script>

<style scoped lang="scss">
.serviceGood-create-container {
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
