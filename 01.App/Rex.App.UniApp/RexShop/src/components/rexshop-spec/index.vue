<template>
  <view class="spec-container">
    <view v-for="(spec, index) in specList" :key="index">
      <view class="spec-name"
        >{{ spec.name }}
        <u-line margin="14rpx 0rpx 0rpx 0rpx" />
      </view>
      <view class="spec-content">
        <template v-for="(value, childIndex) in spec.values" :key="childIndex">
          <view
            class="spec-value-item"
            :class="value.isSelected ? 'spec-value-active' : ''"
            @tap="onSpecChange(index, childIndex)"
            >{{ value.specValueVal }}</view
          >
        </template>
      </view>
    </view>
  </view>
</template>
<script setup lang="ts">
import { ref, onMounted } from "vue";
import _ from "lodash";
import { getWithSpecDesc } from "@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["change"]);

// 定义父组件传过来的值
const props = defineProps({
  spesDesc: {
    type: String,
    required: true,
    default: () => "",
  },
  selectedProduct: {
    type: Object,
    required: false,
    default: () => null,
  },
});
// 商品规格
const specList = ref([] as any);

// 页面加载时
onMounted(() => {
  let selectedProduct = props.selectedProduct as ProductType;
  specList.value = getWithSpecDesc(props.spesDesc, selectedProduct);
});

// 切换
const onSpecChange = (index: number, childIndex: number) => {
  if (specList.value[index].values[childIndex].isSelected) return;
  for (let i = 0; i < specList.value[index].values.length; i++) {
    specList.value[index].values[i].isSelected = false;
  }
  specList.value[index].values[childIndex].isSelected = true;
  let specVal = new Array();
  for (let i = 0; i < specList.value.length; i++) {
    for (let j = 0; j < specList.value[i].values.length; j++) {
      const valItem = specList.value[i].values[j];
      if (valItem.isSelected) {
        specVal.push(`${specList.value[i].name}:${valItem.specValueVal}`);
      }
    }
  }
  emit("change", specVal.join(","));
};
</script>
<style lang="scss" scoped>
.spec-container {
  .spec-name {
    padding: 0rpx 20rpx;
    font-size: 26rpx;
  }
  .spec-content {
    padding: 10rpx 20rpx;
    margin-bottom: 14rpx;
    .spec-value-item {
      display: inline-block;
      min-width: 100rpx;
      text-align: center;
      background-color: #f8f8f8;
      font-size: 20rpx;
      padding: 12rpx;
      border-radius: 60rpx;
      margin-top: 10rpx;
      margin-right: 24rpx;
    }
    .spec-value-item:last-child {
      margin-right: 0rpx !important;
    }
    .spec-value-active {
      border: 2rpx solid #ff0000;
      background-color: #fff5f6;
      color: #ff0000;
    }
  }
}
</style>
