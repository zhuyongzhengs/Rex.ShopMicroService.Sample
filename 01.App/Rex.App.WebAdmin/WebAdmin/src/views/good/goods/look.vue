<template>
  <div class="goods-look-container layout-padding">
    <goods-handle :id="goodId" :isLook="true" ref="goodsHandleRef" />
  </div>
</template>

<script setup lang="ts" name="goodsCreate">
import { ref, defineAsyncComponent, onBeforeMount } from "vue";
import { useRoute } from "vue-router";
// 定义变量内容
const route = useRoute();

// 引入组件
const GoodsHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/goodsHandle.vue")
);

// 商品ID
let goodId = ref("");
onBeforeMount(() => {
  if (route.query.id) goodId.value = String(route.query.id);
});
</script>

<style scoped lang="scss">
.goods-look-container {
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
