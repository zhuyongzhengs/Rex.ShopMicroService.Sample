<template>
  <view class="article-body">
    <template v-if="articleData">
      <view class="article-header-box">
        <view class="article-title">
          <up-text size="32rpx" align="center" :text="articleData.title"></up-text>
        </view>
        <view class="article-date">
          <up-text
            type="info"
            size="24rpx"
            align="center"
            :text="articleData.creationTime"
          ></up-text>
        </view>
      </view>
      <up-line></up-line>
      <view class="article-content-box">
        <up-parse :content="articleData.contentBody"></up-parse>
      </view>
    </template>
    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";

// 定义变量
const uToastRef = ref();
const articleData = ref<ArticleType>();

// 获取文章内容信息
const getContent = (id: string) => {
  http<ArticleType>({
    method: "GET",
    url: `/api/good/common/${id}/article-by-id`,
  })
    .then((result) => {
      if (!result) return;
      articleData.value = result;
    })
    .catch((err: any) => {
      console.error("获取文章内容信息出错：", err);
    });
};

onLoad((e: any) => {
  if (!e.id) {
    uni.navigateBack();
    return;
  }
  getContent(e.id);
});
</script>
<style lang="scss" scoped>
.article-body {
  background-color: white;

  .article-header-box {
    padding: 15rpx;
    margin: 0rpx 20rpx;
    .article-date {
      margin-top: 10rpx;
    }
  }
  .article-content-box {
    padding: 10rpx;
  }
}
</style>
