<template>
  <view class="aftersales-detail-body">
    <view class="aftersales-logistics-box">
      <view class="aftersales-logistics-title">售后单进度</view>
      <view class="logistics-detail-item">
        <up-row>
          <up-col span="12">
            <view class="aftersales-status"
              >当前状态：
              <text
                class="t-status-success"
                v-if="aftersales?.status == 2 && aftersales?.billRefund?.status == 2"
                >退款成功.</text
              >
              <text v-else :class="formatStatusColor(aftersales?.status)">{{
                aftersales?.statusDisplay
              }}</text></view
            >
          </up-col>
        </up-row>
        <up-row v-if="aftersales?.status == 3">
          <up-col span="12">
            <view class="aftersales-status"
              >拒绝原因：<text class="t-status-info">{{ aftersales?.mark }}</text></view
            >
          </up-col>
        </up-row>
      </view>
    </view>

    <view class="aftersales-product-box">
      <view class="aftersales-product-title">商品信息</view>
      <template
        v-for="aftersalesItem in aftersales?.billAftersalesItems"
        :key="aftersalesItem.Id"
      >
        <view
          class="product-list-item"
          @click="routeHelper.goGoodsDetail(aftersalesItem.goodId)"
        >
          <up-row customStyle="margin: 0rpx">
            <up-col span="3">
              <view class="product-image">
                <up-image
                  width="164rpx"
                  height="164rpx"
                  radius="6rpx"
                  :show-loading="true"
                  :src="aftersalesItem.imageUrl"
                />
              </view>
            </up-col>
            <up-col span="9">
              <view class="product-detail">
                <view class="product-title">{{ aftersalesItem.name }}</view>
                <up-row>
                  <up-col span="10">
                    <view class="product-spec">{{ aftersalesItem.addon }}</view>
                  </up-col>
                  <up-col span="2">
                    <view class="product-num float-r">× {{ aftersalesItem.nums }}</view>
                  </up-col>
                </up-row>
              </view>
            </up-col>
          </up-row>
        </view>
      </template>
    </view>
    <view class="aftersales-status-box">
      <view class="aftersales-status-title">售后信息</view>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="aftersales-status-name">售后单号</view>
        </up-col>
        <up-col span="6">
          <up-copy notice="售后单号复制成功！" :content="aftersales?.no">
            <view class="aftersales-status-value">{{ aftersales?.no }}</view>
          </up-copy>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="aftersales-status-name">订单编号</view>
        </up-col>
        <up-col span="6">
          <up-copy notice="订单号复制成功！" :content="aftersales?.order?.no">
            <view class="aftersales-status-value">{{ aftersales?.order?.no }}</view>
          </up-copy>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="aftersales-status-name">收货状态</view>
        </up-col>
        <up-col span="6">
          <view class="aftersales-status-value">{{ aftersales?.typeDisplay }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="aftersales-status-name">退款金额</view>
        </up-col>
        <up-col span="6">
          <view class="aftersales-status-value">￥{{ aftersales?.refundAmount }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx">
        <up-col span="6">
          <view class="aftersales-status-name">申请时间</view>
        </up-col>
        <up-col span="6">
          <view class="aftersales-status-value">{{ aftersales?.creationTime }}</view>
        </up-col>
      </up-row>
    </view>
    <view class="aftersales-detail-box">
      <view class="aftersales-detail-title">图片凭证</view>
      <view class="aftersales-image-item">
        <up-grid
          v-if="
            aftersales?.billAftersalesImagess &&
            aftersales?.billAftersalesImagess.length > 0
          "
        >
          <up-grid-item
            v-for="(image, uIndex) in aftersales?.billAftersalesImagess"
            :key="uIndex"
          >
            <view class="aftersales-img-content">
              <up-image
                width="215rpx"
                height="200rpx"
                mode="scaleToFill"
                :src="image.imageUrl"
                :show-loading="true"
                :lazy-load="true"
              ></up-image>
            </view>
          </up-grid-item>
        </up-grid>
        <up-text v-else type="info" size="28rpx" margin="15rpx" text="无."></up-text>
        <br />
      </view>
    </view>
    <view class="aftersales-detail-box">
      <view class="aftersales-detail-title">问题描述</view>
      <up-row customStyle="padding: 10rpx 15rpx">
        <up-col span="12">
          <up-text type="info" size="28rpx" :text="aftersales?.reason"></up-text>
        </up-col>
      </up-row>
    </view>

    <view class="aftersales-logi-box" v-if="showWriteLogi() || showLogi()">
      <view class="aftersales-logi-title">{{
        `${showWriteLogi() ? "填写" : ""}回邮商品物流信息`
      }}</view>
      <view class="aftersales-logi-content">
        <up-row customStyle="margin-bottom: 10rpx">
          <up-col span="2">
            <up-text align="right" size="default" text="快递公司"></up-text>
          </up-col>
          <up-col span="10">
            <up-input
              fontSize="28rpx"
              placeholder="请输入快递公司"
              border="bottom"
              maxlength="150"
              :readonly="!showWriteLogi()"
              v-model="logiData.logiCode"
              clearable
            ></up-input>
          </up-col>
        </up-row>
        <up-row customStyle="margin-bottom: 10rpx">
          <up-col span="2">
            <up-text align="right" size="default" text="物流单号"></up-text>
          </up-col>
          <up-col span="10">
            <up-input
              fontSize="28rpx"
              placeholder="请输入物流单号"
              border="bottom"
              maxlength="150"
              :readonly="!showWriteLogi()"
              v-model="logiData.logiNo"
              clearable
            ></up-input>
          </up-col>
        </up-row>
      </view>
    </view>
    <view class="aftersales-footer-btn" v-if="showWriteLogi()">
      <view class="aftersales-btn aftersales-logi">
        <up-button
          type="error"
          size="default"
          text="『提交物流信息』"
          @click="onLogiSubmit"
        ></up-button>
      </view>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onLoad, onShow } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const aftersalesId = ref("");
const aftersales = ref<BillAftersalesDetailType>();
const uToastRef = ref();
const logiData = ref({
  logiCode: "",
  logiNo: "",
  billReshipId: "",
});

// 查询用户售后单
const loadAftersales = (aftersalesId: string) => {
  http<BillAftersalesDetailType>({
    url: `/api/front/aggregation/billAftersales/${aftersalesId}`,
    method: "GET",
    data: {
      includeDetails: true,
    },
  })
    .then((result) => {
      if (!result) return;
      aftersales.value = result;
      if (aftersales.value?.billReship) {
        logiData.value.billReshipId = aftersales.value?.billReship.id;
        logiData.value.logiCode = aftersales.value?.billReship.logiCode;
        logiData.value.logiNo = aftersales.value?.billReship.logiNo;
      }
    })
    .catch((err) => {
      console.error(err);
    });
};

// 提交物流
const onLogiSubmit = () => {
  if (!logiData.value.logiCode) {
    uToastRef.value.warning("请输入[快递公司]信息！");
    return;
  }
  if (!logiData.value.logiNo) {
    uToastRef.value.warning("请输入[物料单号]信息！");
    return;
  }
  http<boolean>({
    url: `/api/order/bill-reship/${logiData.value.billReshipId}/logistics`,
    method: "PUT",
    data: {
      logiCode: logiData.value.logiCode,
      logiNo: logiData.value.logiNo,
    },
  })
    .then((result) => {
      if (!result) {
        uToastRef.value.error("提交失败！");
        return;
      }
      loadAftersales(aftersalesId.value);
      uToastRef.value.success("提交成功！");
    })
    .catch((err) => {
      console.error(err);
    });
};

// 显示物料信息
const showLogi = () => {
  if (
    aftersales.value?.status != 3 &&
    aftersales.value?.billReship &&
    (aftersales.value?.billReship?.logiCode || !aftersales.value?.billReship?.logiNo)
  ) {
    return true;
  }
  return false;
};

// 显示填写物料信息
const showWriteLogi = () => {
  if (
    aftersales.value?.status == 2 &&
    aftersales.value?.billReship?.status == 1 &&
    (!aftersales.value?.billReship?.logiCode || !aftersales.value?.billReship?.logiNo)
  ) {
    return true;
  }
  return false;
};

// 状态颜色
const formatStatusColor = (status: number | undefined) => {
  let colorVal = "t-status-info";
  switch (status) {
    case 1:
      colorVal = "t-status-warning";
      break;
    case 2:
      colorVal = "t-status-success";
      break;
    case 3:
      colorVal = "t-status-error";
      break;

    default:
      break;
  }
  return colorVal;
};

onLoad((evt: any) => {
  if (!evt.aftersalesId) {
    uni.navigateBack();
    return;
  }
  aftersalesId.value = evt.aftersalesId;
});

onShow(() => {
  loadAftersales(aftersalesId.value);
});
</script>

<style lang="scss" scoped>
.aftersales-detail-body {
  padding: 20rpx 0rpx;
  padding-bottom: 80rpx;
  .aftersales-steps-box {
    margin: 0rpx 20rpx;
    background-color: white;
    padding: 26rpx 0rpx;
    border-radius: 10rpx;
    .aftersales-item-tips {
      margin-top: 20rpx;
      text-align: center;
      .aftersales-item-desc {
        padding: 0px 10rpx;
      }
    }
  }

  .aftersales-logistics-box {
    margin: 0rpx 20rpx 0rpx 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .aftersales-status {
      font-size: 28rpx;
      padding: 10rpx 0rpx;
      .t-status-primary {
        color: #2979ff;
      }
      .t-status-error {
        color: #fa3534;
      }
      .t-status-success {
        color: #19be6b;
      }
      .t-status-warning {
        color: #ff9900;
      }
      .t-status-info {
        color: #909399;
      }
    }
    .aftersales-logistics-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .logistics-detail-item {
      padding: 10rpx 18rpx;
      .logistics-dtl-icon {
        width: 100%;
        display: inline-block;
      }
      .dtl-text-align-l {
        text-align: left;
      }
      .dtl-text-align-r {
        text-align: right;
      }
      .receiving-user-info {
        font-size: 26rpx;
        color: #2c313b;
        .user-phone-number {
          margin-left: 15rpx;
        }
      }
      .full-address {
        font-size: 24rpx;
        color: #999999;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 3;
      }
    }
  }

  .aftersales-product-box {
    margin: 20rpx 20rpx 0rpx 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .aftersales-product-title {
      font-size: 28rpx;
      margin-bottom: 10rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .product-list-item {
      padding: 10rpx 18rpx;
      .product-detail {
        padding-left: 10rpx;
        .product-title {
          font-size: 26rpx;
          color: #2c313b;
          overflow: hidden;
          word-break: break-all;
          text-overflow: ellipsis;
          display: -webkit-box;
          -webkit-box-orient: vertical;
          -webkit-line-clamp: 3;
        }
        .product-spec {
          font-size: 24rpx;
          color: #aaaaaa;
          margin: 8rpx 0rpx;
        }
        .product-money {
          .product-num {
            font-size: 26rpx;
            color: #2c313b;
          }
        }
      }
    }
    .product-list-item:last-child {
      padding-bottom: 20rpx;
    }
  }
  .aftersales-status-box {
    margin: 20rpx 20rpx 0rpx 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .aftersales-status-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .aftersales-status-name {
      text-align: left;
      font-size: 26rpx;
    }
    .aftersales-status-value {
      text-align: right;
      font-size: 26rpx;
      color: #909399;
    }
    .amount-value {
      font-weight: bold;
      color: #2c313b;
    }
  }
  .aftersales-detail-box {
    margin: 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .aftersales-detail-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .aftersales-image-item {
      padding-bottom: 10rpx;
      .aftersales-img-content {
        margin: 10rpx;
      }
    }
    .aftersales-detail-name {
      text-align: left;
      font-size: 26rpx;
    }
    .aftersales-detail-value {
      text-align: right;
      font-size: 26rpx;
      color: #909399;
    }
  }
  .aftersales-logi-box {
    margin: 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .aftersales-logi-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .aftersales-logi-content {
      padding: 10rpx 15rpx;
    }
  }
  .aftersales-footer-btn {
    // border-top: 1px #f2f3f7 solid;
    background-color: #e37570;
    position: fixed;
    bottom: 0rpx;
    width: 100%;
    text-align: center;
    z-index: 999;
  }
}
</style>
