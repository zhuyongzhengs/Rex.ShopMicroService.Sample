<template>
  <view class="notice-body">
    <template v-if="noticeData">
      <view class="notice-header-box">
        <view class="notice-title">
          <up-text size="32rpx" align="center" :text="noticeData.title"></up-text>
        </view>
        <view class="notice-date">
          <up-text
            type="info"
            size="24rpx"
            align="center"
            :text="noticeData.creationTime"
          ></up-text>
        </view>
      </view>
      <up-line></up-line>
      <view class="notice-content-box">
        <up-parse :content="noticeData.contentBody"></up-parse>
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
const noticeData = ref<NoticeType>();

// 获取通知内容信息
const getContent = (id: string) => {
  http<NoticeType>({
    method: "GET",
    url: `/api/good/common/${id}/notice-by-id`,
  })
    .then((result) => {
      if (!result) return;
      noticeData.value = result;
    })
    .catch((err: any) => {
      console.error("获取通知内容信息出错：", err);
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
.notice-body {
  background-color: white;

  .notice-header-box {
    padding: 15rpx;
    margin: 0rpx 20rpx;
    .notice-date {
      margin-top: 10rpx;
    }
  }
  .notice-content-box {
    padding: 10rpx;
  }
}
</style>
