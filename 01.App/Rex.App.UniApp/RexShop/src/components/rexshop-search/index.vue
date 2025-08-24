<template>
  <view class="bg-white u-padding-15" :style="'border-radius: ' + radius">
    <u-search
      :placeholder="rexShopData.parameters.keywords"
      v-model="searchData.keyword"
      :shape="rexShopData.parameters.style"
      :show-action="false"
      action-text="搜索"
      bgColor="#F2F3F7"
      @custom="goSearch"
      @search="goSearch"
    />

    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts" name="rexShopSearch">
import { ref, onMounted, defineProps } from "vue";

const uToastRef = ref();

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
  radius: {
    type: String,
    default: () => "0rpx",
  },
});

// 查询
const searchData = ref({
  keyword: "",
  searchTop: 0,
  scrollTop: 0,
  searchFixed: false,
});

// 搜索
const goSearch = () => {
  if (!searchData.value.keyword) {
    uToastRef.value.warning("请输入查询关键字！");
    return;
  }
  uni.navigateTo({
    url: `/pages/category/list?name=${searchData.value.keyword}`,
  });
};

// 页面加载完时
onMounted(() => {
  //#ifdef H5
  // ...
  // #endif
});
</script>
