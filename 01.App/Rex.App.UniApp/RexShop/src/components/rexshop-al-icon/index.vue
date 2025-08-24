<!-- 图标组件 -->

<template>
  <text
    :class="[icon, extraClass]"
    :style="[iconStyle, { color: color, fontSize: size }]"
    @click="$emit('active')"
  />
</template>

<script>
/**
 * @description 阿里矢量图
 * @example <rexshop-al-icon color="图标颜色" size="图标大小" icon="矢量图标代码" iconStyle="图标样式" />
 *
 * @property {String}			icon				矢量图标代码
 * @property {String}			extraClass			图标样式class
 * @property {Object}			iconStyle			图标样式style
 * @property {String}			size				图标大小（默认 32rpx ）
 * @property {String}			color				图标颜色（默认 #919092 ）
 *
 * @event {Function} active			点击图标时触发
 */

export default {
  // 小程序样式穿透
  options: {
    styleIsolation: "shared",
  },
  name: "al-icon",
  props: {
    icon: {
      // 矢量图标代码
      type: String,
      default: "",
    },
    extraClass: {
      // 样式
      type: String,
      default: "custom-icon",
    },
    iconStyle: {
      // 样式
      type: Object,
      default: () => {},
    },
    size: {
      // 图标大小
      type: String,
      default: "32rpx",
    },
    color: {
      // 图标颜色
      type: String,
      default: "#919092",
    },
  },
};
</script>

<style lang="scss" scoped>
page {
  background: #f9f9f9;
  font-family: "微软雅黑", sans-serif;
  font-size: 28rpx;
  min-height: 100%;
  overflow-x: hidden;
  -webkit-overflow-scrolling: touch;
}

@import url("../../static/fonts/icons/iconfont.css");
</style>
