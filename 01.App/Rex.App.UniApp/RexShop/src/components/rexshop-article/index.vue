<template>
  <view
    class="rexshop-index-article rexshop-cell-group"
    :style="'border-radius: ' + radius"
    v-if="rexShopData.parameters.list && isParamList"
  >
    <view
      class="rexshop-cell-item"
      v-for="item in rexShopData.parameters.list"
      :key="item.id"
      @click="routeHelper.goArticleDetail(item.id)"
    >
      <view class="rexshop-cell-item-bd">
        <view class="article-title">
          {{ item.title }}
        </view>
        <view class="article-des u-line-2">
          {{ item.brief }}
        </view>
      </view>
      <view class="cell-title-img">
        <image
          class="cover-image"
          height="100%"
          width="100%"
          mode="aspectFill"
          :src="item.coverImage"
          :lazy-load="true"
        />
      </view>
    </view>

    <u-toast ref="uToast" />
  </view>
</template>

<script setup lang="ts" name="rexShopArticle">
import { ref, onMounted, defineProps, computed } from "vue";
import routeHelper from "@/utils/routeHelper";

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

// 是否包含文章数据
const isParamList = computed(() => {
  return props.rexShopData.parameters.list.length > 0;
});

onMounted(() => {
  // console.log(props.rexShopData);
});
</script>

<style scoped lang="scss">
.rexshop-index-article {
  padding: 0rpx 15rpx;
  background: #ffffff !important;
  color: #333333 !important;
  overflow: hidden;
  .rexshop-cell-item {
    padding: 10rpx 0rpx 10rpx 0;
    float: left;
    width: 100%;
    border-bottom: 2rpx solid #f3f3f3;
    .rexshop-cell-item-bd {
      float: left;
      width: calc(100% - 200rpx);
      display: flex;
      flex-direction: column;
      align-items: flex-start;
      .article-title {
        font-size: 28rpx;
        color: #333;
        width: 100%;
        overflow: hidden;
        float: left;
        margin-bottom: 10rpx;
      }
      .article-des {
        font-size: 24rpx;
        color: #999;
        overflow: hidden;
        float: left;
        line-height: 40rpx;
      }
    }
    .cell-title-img {
      width: 190rpx;
      height: 160rpx;
      float: right;
      position: relative;
      top: 2rpx;
      .cover-image {
        width: 100%;
        height: 100%;
        border-radius: 10rpx;
      }
    }
  }
}
</style>
