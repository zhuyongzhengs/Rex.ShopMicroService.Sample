<template>
  <div class="goods-create-container layout-padding">
    <goods-handle ref="goodsHandleRef" />
  </div>
</template>

<script setup lang="ts" name="goodsCreate">
import { defineAsyncComponent } from "vue";
// 引入组件
const GoodsHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/goodsHandle.vue")
);
</script>

<style scoped lang="scss">
.goods-create-container {
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
