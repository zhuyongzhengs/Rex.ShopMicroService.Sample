<template>
  <view class="after-sale-container">
    <view class="op-title">请选择要退货的商品</view>
    <view class="order-product-box">
      <view class="order-product-title">商品信息</view>
      <checkbox-group @change="afterSaleChange">
        <template v-for="afterSaleDetail in afterSaleDetails" :key="afterSaleDetail.Id">
          <view class="product-list-item">
            <up-row customStyle="margin: 0rpx">
              <up-col span="1">
                <checkbox
                  :value="afterSaleDetail.id"
                  :checked="afterSaleDetail.checked"
                  :disabled="afterSaleDetail.disabled"
                />
              </up-col>
              <up-col span="3">
                <view class="product-image">
                  <up-image
                    width="164rpx"
                    height="164rpx"
                    radius="6rpx"
                    :show-loading="true"
                    :src="afterSaleDetail.imageUrl"
                  />
                </view>
              </up-col>
              <up-col span="8">
                <view class="product-detail">
                  <view class="product-title">{{ afterSaleDetail.name }}</view>
                  <view class="product-spec">{{ afterSaleDetail.addon }}</view>
                  <view class="product-money">
                    <view class="product-price f24 float-l">{{
                      afterSaleDetail.amount
                    }}</view>
                    <view class="product-num float-r">× {{ afterSaleDetail.nums }}</view>
                    <view class="clear-both"></view>
                  </view>
                </view>
              </up-col>
            </up-row>
          </view>
        </template>
      </checkbox-group>
    </view>
    <view class="after-type-box">
      <view class="type-name">是否发货</view>
      <view class="type-content">
        <up-radio-group v-model="typeValue">
          <up-radio
            labelSize="28rpx"
            :customStyle="{ marginLeft: '10px' }"
            v-for="(rType, index) in radioTypes"
            :key="index"
            :label="rType.name"
            :name="rType.name"
            :disabled="rType.disabled"
          >
          </up-radio>
        </up-radio-group>
      </view>
      <view class="clear-both"></view>
    </view>
    <view class="return-money-box">
      <view class="money-name">退款金额</view>
      <view class="money-value">{{
        afterSaleDetails.reduce((accumulator, currentValue) => {
          let val = currentValue.checked ? currentValue.amount : 0;
          return accumulator + val;
        }, 0)
      }}</view>
      <view class="clear-both"></view>
    </view>
    <view class="voucher-upload-box">
      <view class="vu-title">上传凭证</view>
      <view class="vu-upload-content">
        <up-upload
          :fileList="fileImages"
          multiple
          :maxCount="10"
          :previewFullImage="true"
          @afterRead="afterRead"
          @delete="deletePic"
        ></up-upload>
      </view>
    </view>
    <view class="description-box">
      <view class="desc-title">问题描述</view>
      <view class="desc-content">
        <up-textarea
          placeholder="请输入描述内容"
          border="none"
          autoHeight
          :count="true"
          :maxlength="200"
          v-model="reason"
        ></up-textarea>
      </view>
    </view>
    <view class="footer-btn-box">
      <view class="submit-btn" @tap="onSubmit">提交</view>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { formatOrderStatus } from "@/utils/other";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { uploadFileConfig } from "@/utils/other";
import type { AfterSaleDetailType } from "./types";

const uToastRef = ref();
const fileImages = ref<FileListType[]>([]);
const radioTypes = reactive([
  {
    name: "未发货",
    disabled: false,
  },
  {
    name: "已发货",
    disabled: false,
  },
]);
const typeValue = ref("");
const reason = ref("");
// 读取文件
const afterRead = async (event: FileReadType) => {
  let fileList = new Array<FileListType>();
  let fileListLen = fileImages.value.length;
  event.file.map((item) => {
    fileList.push({
      ...item,
      status: "uploading",
      message: "上传中",
    });
  });
  for (let i = 0; i < fileList.length; i++) {
    const result = await uploadFilePromise(fileList[i].url);
    let item = fileImages.value[fileListLen];
    fileImages.value.splice(fileListLen, 1, {
      ...item,
      status: "success",
      message: "",
      url: result,
    });
    fileListLen++;
  }
};

// 上传图片
const uploadFilePromise = (url: string): Promise<string> => {
  return new Promise((resolve, reject) => {
    uni.uploadFile({
      url: uploadFileConfig().action,
      filePath: url,
      name: "file",
      headers: uploadFileConfig().headers,
      success: (res) => {
        resolve(res.data);
      },
    });
  });
};

// 删除图片
const deletePic = (event: any) => {
  fileImages.value.splice(event.index, 1);
};

const afterSaleDetails = ref<AfterSaleDetailType[]>([]);
const uOrder = ref<UserOrderType>();
onLoad((evt: any) => {
  if (!evt.orderId) {
    uni.navigateBack();
    return;
  }
  loadUserOrder(evt.orderId);
});

// 售后单选择切换
const afterSaleChange = (e: any) => {
  for (let i = 0; i < afterSaleDetails.value.length; i++) {
    const afterSaleDetail = afterSaleDetails.value[i];
    afterSaleDetail.checked = e.detail.value.includes(afterSaleDetail.id);
  }
};

// 提交
const onSubmit = () => {
  let submitData = afterSaleDetails.value.filter((x) => x.checked);
  if (!uOrder.value || !submitData || submitData.length < 1) {
    uToastRef.value.warning("您还未选择退货的商品信息！");
    return;
  }
  if (!reason.value) {
    uToastRef.value.warning("请输入问题描述信息！");
    return;
  }
  let refundAmount = afterSaleDetails.value.reduce((accumulator, currentValue) => {
    let val = currentValue.checked ? currentValue.amount : 0;
    return accumulator + val;
  }, 0);
  let rData = {
    orderId: uOrder.value.id,
    userId: useUserLoginStore().currentSysUser?.id,
    type: typeValue.value == "未发货" ? 1 : 2,
    refundAmount,
    orderItemIds: submitData.map((x) => x.id),
    images: fileImages.value.map((x) => x.url),
    reason: reason.value,
  };
  http<boolean>({
    method: "POST",
    url: "/api/order/bill-aftersales",
    data: rData,
  })
    .then((result) => {
      if (!result) return;
      uToastRef.value.show({
        message: "提交成功！",
        type: "success",
        complete: function () {
          uni.navigateBack();
        },
      });
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 查询用户订单
const loadUserOrder = (orderId: string) => {
  http<UserOrderType>({
    url: "/api/order/orders/" + orderId + "/user-order",
    method: "GET",
  })
    .then((result) => {
      if (!result) return;
      uOrder.value = result;
      afterSaleDetails.value = result.orderItems.map((item) => ({
        ...item,
        checked: false,
      }));
      let tVal = formatOrderStatus(result);
      switch (tVal) {
        case "待付款":
        case "待发货":
        case "部分发货":
        case "已取消":
          radioTypes[1].disabled = true;
          typeValue.value = "未发货";
          break;
        default:
          typeValue.value = "已发货";
          break;
      }
    })
    .catch((err) => {
      console.error(err);
    });
};
</script>

<style lang="scss" scoped>
.after-sale-container {
  padding-bottom: 100rpx;
  .op-title {
    padding: 10px 5px;
    background-color: white;
    text-align: center;
    font-size: 30rpx;
  }
  .order-product-box {
    margin: 20rpx 0rpx;
    border-radius: 10rpx;
    background-color: white;
    .order-product-title {
      font-size: 28rpx;
      margin-bottom: 10rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .product-list-item {
      padding: 20rpx 15rpx;
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
          -webkit-line-clamp: 2;
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
  .return-money-box {
    margin: 20rpx 0px;
    background-color: white;
    padding: 20rpx 15rpx;
    font-size: 28rpx;
    border-radius: 10rpx;
    .money-name {
      float: left;
    }
    .money-value {
      float: right;
    }
    .money-value::before {
      content: "￥";
    }
  }
  .after-type-box {
    margin: 20rpx 0px;
    background-color: white;
    padding: 15rpx;
    font-size: 28rpx;
    border-radius: 10rpx;
    .type-name {
      float: left;
      line-height: 54rpx;
    }
    .type-content {
      float: right;
    }
  }
  .voucher-upload-box {
    margin: 20rpx 0px;
    background-color: white;
    font-size: 28rpx;
    border-radius: 10rpx;
    .vu-title {
      font-size: 28rpx;
      padding-bottom: 10rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .vu-upload-content {
      padding: 10rpx;
    }
  }
  .description-box {
    margin: 20rpx 0px;
    background-color: white;
    font-size: 30rpx;
    border-radius: 10rpx;
    .desc-title {
      font-size: 28rpx;
      padding-bottom: 10rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .desc-content {
      padding: 10rpx;
    }
  }
  .footer-btn-box {
    width: 100%;
    position: fixed;
    bottom: 0;
    z-index: 999;
    .submit-btn {
      background-color: #f56c6c;
      text-align: center;
      height: 80rpx;
      line-height: 80rpx;
      color: white;
    }
  }
}
</style>
