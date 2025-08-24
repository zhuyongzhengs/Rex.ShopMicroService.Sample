<template>
  <view class="comment-container">
    <view class="order-product-box">
      <template
        v-if="userComments"
        v-for="(uComment, index) in userComments"
        :key="index"
      >
        <view class="order-product-title">商品信息</view>
        <view
          class="product-list-item"
          @click="routeHelper.goGoodsDetail(uComment.goodId)"
        >
          <up-row customStyle="margin: 0rpx">
            <up-col span="3">
              <view class="product-image">
                <up-image
                  width="164rpx"
                  height="164rpx"
                  radius="6rpx"
                  :show-loading="true"
                  :src="uComment.imageUrl"
                />
              </view>
            </up-col>
            <up-col span="9">
              <view class="product-detail">
                <view class="product-title">{{ uComment.name }}</view>
                <view class="product-spec">{{ uComment.addon }}</view>
                <view class="product-money">
                  <view class="product-price f24 float-l">{{ uComment.amount }}</view>
                  <view class="product-num float-r">× {{ uComment.nums }}</view>
                  <view class="clear-both"></view>
                </view>
              </view>
            </up-col>
          </up-row>
        </view>

        <view class="comment-rate-box">
          <view class="cr-title">商品评分</view>
          <view class="cr-rate-content">
            <up-rate :count="5" v-model="uComment.score"></up-rate>
          </view>
        </view>

        <view class="comment-desc-box">
          <view class="desc-title">评价内容</view>
          <view class="desc-content">
            <up-textarea
              placeholder="请输入评价内容…"
              border="none"
              autoHeight
              :maxlength="200"
              v-model="uComment.contentBody"
            ></up-textarea>
          </view>
        </view>

        <view class="comment-upload-box">
          <view class="vu-title">上传凭证</view>
          <view class="vu-upload-content">
            <up-upload
              :fileList="uComment.imageFiles"
              multiple
              :maxCount="10"
              :previewFullImage="true"
              @afterRead="afterRead($event, index)"
              @delete="deletePic($event, index)"
            ></up-upload>
          </view>
        </view>
      </template>
    </view>

    <template v-if="userComments">
      <view class="footer-btn-box">
        <view class="submit-btn" @tap="onSubmit">提交</view>
      </view>
    </template>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import routeHelper from "@/utils/routeHelper";
import { uploadFileConfig } from "@/utils/other";

const uToastRef = ref();
const userComments = ref<UserOrderCommentType[]>([]);
const orderId = ref<string>();
// 读取文件
const afterRead = async (event: FileReadType, index: number) => {
  let fileList = new Array<FileListType>();
  let fileListLen = userComments.value[index].imageFiles.length;
  event.file.map((item) => {
    fileList.push({
      ...item,
      status: "uploading",
      message: "上传中",
    });
  });
  for (let i = 0; i < fileList.length; i++) {
    const result = await uploadFilePromise(fileList[i].url);
    let item = userComments.value[index].imageFiles[fileListLen];
    userComments.value[index].imageFiles.splice(fileListLen, 1, {
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
const deletePic = (event: any, index: number) => {
  userComments.value[index].imageFiles.splice(event.index, 1);
};

onLoad((evt: any) => {
  if (!evt.orderId) {
    uni.navigateBack();
    return;
  }
  orderId.value = evt.orderId;
  loadUserOrder(evt.orderId);
});

// 提交
const onSubmit = () => {
  for (let i = 0; i < userComments.value.length; i++) {
    userComments.value[i].images = userComments.value[i].imageFiles
      .map((x) => x.url)
      .join(";");
  }
  http<boolean>({
    method: "POST",
    url: "/api/good/good-comment/user-comment/" + orderId.value,
    data: userComments.value,
  })
    .then((result) => {
      if (!result) return;
      uToastRef.value.show({
        message: "提交成功！",
        type: "success",
        complete: function () {
          uni.$emit("userGoodComment", true);
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
      const userId = useUserLoginStore().currentSysUser?.id;
      const uName = useUserLoginStore().currentSysUser?.name;
      const uAvatar = useUserLoginStore().currentSysUser?.avatar;
      for (let i = 0; i < result.orderItems.length; i++) {
        let orderItem = result.orderItems[i];
        userComments.value.push({
          imageUrl: orderItem.imageUrl,
          name: orderItem.name,
          nums: orderItem.nums,
          amount: orderItem.amount,
          userId: String(userId),
          userName: String(uName),
          avatar: String(uAvatar),
          goodId: orderItem.goodId,
          addon: orderItem.addon,
          score: 5,
          images: "",
          contentBody: "",
          imageFiles: [],
        });
      }
    })
    .catch((err) => {
      console.error(err);
    });
};
</script>

<style lang="scss" scoped>
.comment-container {
  padding-bottom: 100rpx;
  .order-product-box {
    margin-bottom: 20rpx;
    .order-product-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
      background-color: white;
    }
    .product-list-item {
      padding: 20rpx 15rpx;
      background-color: white;
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

  .comment-rate-box {
    margin-top: 1rpx;
    background-color: white;
    font-size: 28rpx;
    border-radius: 10rpx;
    .cr-title {
      font-size: 28rpx;
      padding-bottom: 10rpx;
      padding: 10rpx 15rpx 0rpx 15rpx;
    }
    .cr-rate-content {
      padding: 10rpx;
    }
  }

  .comment-desc-box {
    margin-top: 1rpx;
    background-color: white;
    font-size: 28rpx;
    border-radius: 10rpx;
    .desc-title {
      font-size: 28rpx;
      padding-bottom: 10rpx;
      padding: 10rpx 15rpx 0rpx 15rpx;
    }
    .desc-content {
      padding: 10rpx;
    }
  }

  .comment-upload-box {
    margin-top: 1rpx;
    margin-bottom: 20rpx;
    background-color: white;
    font-size: 28rpx;
    border-radius: 10rpx;
    .vu-title {
      font-size: 28rpx;
      padding-bottom: 10rpx;
      padding: 10rpx 15rpx 0rpx 15rpx;
    }
    .vu-upload-content {
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
