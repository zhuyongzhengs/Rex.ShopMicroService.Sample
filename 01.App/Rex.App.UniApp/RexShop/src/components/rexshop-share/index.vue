<template>
  <view
    v-if="shareData.show"
    class="mask"
    @click="toggleMask"
    @touchmove.stop.prevent="stopPrevent"
    :style="{ backgroundColor: shareData.backgroundColor }"
  >
    <view
      class="mask-content"
      @click.stop.prevent="stopPrevent"
      :style="[
        {
          height: shareData.config.height,
          transform: shareData.transform,
        },
      ]"
    >
      <scroll-view class="view-content" scroll-y>
        <view class="share-header"> 分享到 </view>
        <view class="share-list">
          <view
            v-for="(item, index) in shareData.shareList"
            :key="index"
            class="share-item"
          >
            <button
              class="mp-share-btn"
              :open-type="item.type == 1 ? 'share' : ''"
              @click="shareToFriend(item.type)"
            >
              <rexshop-al-icon
                iconStyle="position:relative;bottom: 60rpx"
                :size="item.size"
                :color="item.color"
                :icon="item.icon"
              />
            </button>
            <text class="share-name">{{ item.text }}</text>
          </view>
        </view>
      </scroll-view>
      <view class="bottom" @click="toggleMask">取消</view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  contentHeight: {
    type: Number,
    default: () => 0,
  },
  hasTabbar: {
    type: Boolean,
    default: () => false,
  },

  // 分享信息
  goodsId: {
    type: String,
    default: () => "",
  },
  shareImg: {
    type: String,
    default: () => "",
  },
  shareTitle: {
    type: String,
    default: () => "",
  },
  shareContent: {
    type: String,
    default: () => "",
  },
  shareHref: {
    type: String,
    default: () => "",
  },
  objectId: {
    type: String,
    default: () => "",
  },
});

// 定义变量
const shareData = ref({
  transform: "translateY(50vh)",
  timer: 0,
  backgroundColor: "rgba(0,0,0,0)",
  show: false,
  shareList: [
    {
      type: 1,
      color: "#5FC156",
      icon: "custom-icon-weixin",
      size: "100rpx",
      text: "微信好友",
    },
    {
      type: 2,
      color: "#4C9DE2",
      icon: "custom-icon-erweimahaibao",
      size: "100rpx",
      text: "生成海报",
    },
  ],
  config: {} as any,
});

// 防止冒泡和滚动穿透
const stopPrevent = () => {};

// 切换展示
const toggleMask = () => {
  // 防止高频点击
  if (shareData.value.timer == 1) {
    return;
  }
  shareData.value.timer = 1;
  setTimeout(() => {
    shareData.value.timer = 0;
  }, 500);

  if (shareData.value.show) {
    shareData.value.transform = shareData.value.config.transform;
    shareData.value.backgroundColor = "rgba(0,0,0,0)";
    setTimeout(() => {
      shareData.value.show = false;
      props.hasTabbar && uni.showTabBar();
    }, 200);
    return;
  }

  shareData.value.show = true;
  // 等待mask重绘完成执行
  if (props.hasTabbar) {
    uni.hideTabBar({
      success: () => {
        setTimeout(() => {
          shareData.value.backgroundColor = shareData.value.config.backgroundColor;
          shareData.value.transform = "translateY(0px)";
        }, 10);
      },
    });
  } else {
    setTimeout(() => {
      shareData.value.backgroundColor = shareData.value.config.backgroundColor;
      shareData.value.transform = "translateY(0px)";
    }, 10);
  }
};

// 分享操作
const shareToFriend = (type: number) => {
  toggleMask();
  if (type != 2) return;
  uni.showToast({
    title: "开发中！",
    icon: "none",
  });
};

// 暴露变量
defineExpose({
  toggleMask,
});

// 创建页面完成时
onMounted(() => {
  const height = uni.upx2px(props.contentHeight) + "px";
  shareData.value.config = {
    height: height,
    transform: `translateY(${height})`,
    backgroundColor: "rgba(0,0,0,.4)",
  };
  shareData.value.transform = shareData.value.config.transform;
});
</script>

<style lang="scss" scoped>
.mask {
  position: fixed;
  left: 0;
  top: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: flex-end;
  z-index: 998;
  transition: 0.3s;
  .bottom {
    position: absolute;
    left: 0;
    bottom: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 90upx;
    background: #fff;
    z-index: 9;
    font-size: 30rpx;
    color: $shop-type-info-dark;
    border-top: 1rpx #f1f2f5 solid;
  }
}

.mask-content {
  width: 100%;
  height: 580upx;
  transition: 0.3s;
  background: #fff;
  &.has-bottom {
    padding-bottom: 90upx;
  }
  .view-content {
    height: 100%;
  }
}
.share-header {
  height: 110upx;
  font-size: 30rpx;
  color: #303133;
  display: flex;
  align-items: center;
  justify-content: center;
  padding-top: 10upx;
  &:before,
  &:after {
    content: "";
    width: 240upx;
    height: 0;
    border-top: 1px solid #e4e7ed;
    transform: scaleY(0.5);
    margin-right: 30upx;
  }
  &:after {
    margin-left: 30upx;
    margin-right: 0;
  }
}
.share-list {
  display: flex;
  flex-wrap: wrap;
  .share-name {
    display: inline-block;
    margin-top: 10rpx;
  }
}
.mp-share-btn {
  background-color: white;
  border: none;
}
.share-item {
  min-width: 50%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 180upx;
  image {
    width: 80upx;
    height: 80upx;
    margin-bottom: 16upx;
  }
  text {
    font-size: 28rpx;
    color: #606266;
  }
}
</style>
