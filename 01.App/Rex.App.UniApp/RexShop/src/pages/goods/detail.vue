<template>
  <view class="goods-detail-body">
    <!--无网络组件-->
    <u-no-network></u-no-network>

    <view class="detail-box">
      <!-- 轮播图部分 -->
      <view class="detail-swiper">
        <u-swiper
          indicatorMode="dot"
          width="100%"
          height="750rpx"
          :displayMultipleItems="album.length > 0 ? 1 : 0"
          :radius="0"
          :indicator="true"
          :loading="album.length < 1"
          :list="album"
        />
      </view>

      <!-- 商品价格 -->
      <view class="goods-price-box">
        <up-row justify="space-between">
          <up-col span="4">
            <text class="good-price">{{ goods.price || "0.00" }}</text>
          </up-col>
          <up-col span="6">
            <view
              class="good-mkt-price rexshop-text-through"
              v-show="goods.price != goods.mktPrice"
              >原价￥{{ goods.mktPrice || "0.00" }}</view
            >
            <view class="good-buy-count">{{ goods.buyCount || "0" }} 人已购买</view>
          </up-col>
          <up-col span="2">
            <u-icon
              name="share"
              @click="goShare()"
              size="26"
              color="#fff"
              label-color="#fff"
              label-pos="bottom"
            ></u-icon>
          </up-col>
        </up-row>
      </view>

      <!-- 标题 -->
      <view class="goods-name-box">
        <view class="good-title-item">
          <view class="good-brand" v-if="goods.brand">
            <u-tag type="error" mode="dark" size="mini" :text="goods.brand.name" />
          </view>
          <text class="good-title u-font-lg">{{ goods.name }}</text>
        </view>
        <view class="good-brief" v-show="goods.brief">
          <text class="u-font-sm">{{ goods.brief }}</text>
        </view>
      </view>
    </view>

    <!-- 服务 -->
    <view class="good-service-box">
      <u-cell-group>
        <u-cell
          title="服务"
          rightIcon="arrow-right"
          :isLink="true"
          :titleStyle="cellItemTitleStyle"
          :rightIconStyle="cellItemRightIconStyle"
          @click="openServiceDescription(2)"
        >
          <template #value>
            <view class="product-spes-desc">
              <view
                v-for="(item, index) in serviceDescriptionItem.service"
                :key="index"
                class="desc-icon u-margin-right-20"
              >
                <u-icon
                  name="checkmark-circle"
                  size="20rpx"
                  label-size="24rpx"
                  color="#e54d42"
                  :label="item.title"
                ></u-icon>
              </view>
            </view>
          </template>
        </u-cell>
      </u-cell-group>
    </view>

    <!-- 发货/规格 -->
    <view class="good-delivery-box">
      <u-cell-group>
        <u-cell
          title="发货"
          :isLink="false"
          :titleStyle="cellItemTitleStyle"
          :rightIconStyle="cellItemRightIconStyle"
        >
          <template #value>
            <view class="product-spes-desc u-line-1 u-font-sm" style="width: 90%">
              <text
                v-for="(item, index) in serviceDescriptionItem.delivery"
                :key="index"
                >{{ item.description }}</text
              >
            </view>
          </template>
        </u-cell>
        <u-cell
          title="规格"
          rightIcon="arrow-right"
          :isLink="true"
          :titleStyle="cellItemTitleStyle"
          :rightIconStyle="cellItemRightIconStyle"
          @click="onOpenSpec(0)"
        >
          <template #value>
            <view class="product-spes-desc u-line-1 u-font-sm">{{
              selectedSpec.product?.spesDesc || "请选择"
            }}</view>
          </template>
        </u-cell>
      </u-cell-group>
    </view>

    <!-- 评价 -->
    <view class="good-comment-box" v-if="userGoodComments && userGoodComments.length > 0">
      <view class="comment-title-item">
        <up-row>
          <up-col span="12"><view class="comment-count">评价</view> </up-col>
          <!--
          <up-col span="2" textAlign="right">
            <view class="comment-more">
              <up-icon color="#aaaaaa" name="arrow-right"></up-icon>
            </view>
          </up-col>
          -->
        </up-row>
      </view>
      <up-line margin="6rpx 0px 0px 30rpx"></up-line>
      <view
        class="comment-content-item"
        v-for="uGoodComment in userGoodComments"
        :key="uGoodComment.id"
      >
        <up-row align="top">
          <up-col span="1.5">
            <up-avatar size="60rpx" :src="uGoodComment.avatar"></up-avatar>
          </up-col>
          <up-col span="10.5">
            <view class="comment-user-name">{{ uGoodComment.userName }}</view>
            <view class="comment-content">{{ uGoodComment.contentBody }}</view>
            <view class="comment-rate">
              <up-rate :readonly="true" v-model="uGoodComment.score" :count="5"></up-rate>
            </view>
            <view class="comment-date-addon">
              {{ uGoodComment.creationTime }}&nbsp;&nbsp;{{ uGoodComment.addon }}
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <!-- 商家==>推荐 -->
    <view class="good-recommend-box">
      <view class="good-merchant-item">
        <up-row>
          <up-col span="2">
            <up-avatar size="44" :src="merchantRecommendItem.platformLogo" />
          </up-col>
          <up-col span="8">
            <view class="good-shop-name">
              {{ merchantRecommendItem.shopName }}
            </view>
            <view class="good-share-title u-line-1">{{
              merchantRecommendItem.shareTitle
            }}</view>
          </up-col>
          <up-col span="2">
            <up-button
              type="error"
              size="mini"
              plain
              :customStyle="{ padding: '0px' }"
              @click="routeHelper.phoneCall(merchantRecommendItem.shopMobile)"
              >联系商家</up-button
            >
          </up-col>
        </up-row>
        <view class="merchant-position-item">
          <up-row>
            <up-col span="10">
              <view class="position-title">
                <view class="tag-position">
                  <u-tag text="已定位" plain mode="plain" size="mini" type="warning" />
                </view>
                <view class="position-content u-line-1">可直接获取商家地理位置信息</view>
              </view>
            </up-col>
            <up-col span="2">
              <up-button
                icon="map"
                type="warning"
                size="mini"
                shape="circle"
                :plain="true"
                @click="routeHelper.goShopMap(merchantRecommendItem.reshipCoordinate)"
              />
            </up-col>
          </up-row>
        </view>
      </view>
      <view class="good-recommend-item">
        <view class="recommend-title">本店推荐</view>
        <view class="recommend-scroll-box">
          <view class="recommend-scroll">
            <view class="goods-content" v-if="isGoodRecommendList">
              <u-grid col="3" :border="false" align="left">
                <u-grid-item
                  bg-color="transparent"
                  :custom-style="{ padding: '0rpx' }"
                  v-for="(good, index) in merchantRecommendItem.goodRecommendation"
                  :key="index"
                  @click="routeHelper.goGoodsDetail(good.id)"
                >
                  <view class="good-item-box">
                    <view class="good-content">
                      <image
                        class="good-img"
                        mode="scaleToFill"
                        :src="good.image"
                        :lazy-load="true"
                      />
                      <view class="good-title u-line-2">
                        {{ good.name }}
                      </view>
                      <up-row>
                        <up-col span="6">
                          <view class="product-price u-margin-top-10 align-l">{{
                            good.price
                          }}</view>
                        </up-col>
                        <up-col span="6" v-show="good.price != good.mktPrice">
                          <view class="product-mkt-price u-margin-top-10 align-c">{{
                            good.mktPrice
                          }}</view>
                        </up-col>
                      </up-row>
                    </view>
                  </view>
                </u-grid-item>
              </u-grid>
            </view>
          </view>
        </view>
      </view>
    </view>

    <!-- 商品详情 -->
    <view class="good-detail-box">
      <view class="detail-title">
        <view class="float-l">
          <u-icon name="gift" size="24" />
        </view>
        商品详情</view
      >
      <view class="detail-param" v-if="isGoodParam">
        <view
          class="detail-param-item"
          v-for="(param, index) in goodParamItems"
          :key="index"
        >
          <text class="param-name">{{ param.name }}</text>
          <text class="param-value">{{ param.value }}</text>
        </view>
      </view>
      <view class="detail-content" v-if="goods.intro">
        <u-parse
          :content="goods.intro"
          :selectable="true"
          :lazyLoad="true"
          :scrollTable="true"
        />
      </view>
    </view>

    <!-- 常见说明 -->
    <view class="common-desc-box">
      <view class="desc-title">
        <view class="float-l">
          <u-icon name="question-circle" size="20" />
        </view>
        &nbsp;常见说明</view
      >
      <up-row
        customStyle="margin-bottom: 15rpx"
        v-for="(question, index) in serviceDescriptionItem.commonQuestion"
        :key="index"
      >
        <up-col span="3">
          <view class="question-title">
            <up-button type="info" size="mini" plain :text="question.title" />
          </view>
        </up-col>
        <up-col span="9">
          <view class="question-desc">{{ question.description }}</view>
        </up-col>
      </up-row>
      <view class="desc-more">
        <text class="text-more">查看更多问题</text>
      </view>
    </view>

    <!-- 为您推荐 -->
    <view class="good-other-recommend-box" v-if="isOtherGoodRecommendList">
      <view class="recommend-divider">
        <u-divider text="为您推荐" />
      </view>
      <view class="good-other-recommend-item">
        <view class="goods-content">
          <u-grid col="2" :border="false" align="left">
            <u-grid-item
              bg-color="transparent"
              :custom-style="{ padding: '0rpx' }"
              v-for="(good, index) in otherGoodRecommends"
              :key="index"
              @click="routeHelper.goGoodsDetail(good.id)"
            >
              <view class="good-item-box">
                <view class="good-content">
                  <image
                    class="good-img"
                    mode="scaleToFill"
                    :src="good.image"
                    :lazy-load="true"
                  />
                  <view class="good-title u-line-2">
                    {{ good.name }}
                  </view>
                  <up-row>
                    <up-col span="6">
                      <view class="product-price u-margin-top-10 align-l">{{
                        good.price
                      }}</view>
                    </up-col>
                    <up-col span="6" v-show="good.price != good.mktPrice">
                      <view class="product-mkt-price u-margin-top-10 align-r">{{
                        good.mktPrice
                      }}</view>
                    </up-col>
                  </up-row>
                </view>
              </view>
            </u-grid-item>
          </u-grid>
        </view>
      </view>
    </view>

    <!-- 底部操作 -->
    <view class="navigation-box">
      <view class="left">
        <view class="item">
          <u-icon name="server-fill" :size="20" color="#606266"></u-icon>
          <view class="text u-line-1">客服</view>
        </view>
        <view class="item" @tap="onGoodCollection">
          <view class="fav-icon">
            <u-icon
              :name="goods.isFav ? 'star-fill' : 'star'"
              :size="20"
              align="center"
              color="#606266"
            ></u-icon>
          </view>
          <view class="text u-line-1">{{ goods.isFav ? "已收藏" : "收藏" }}</view>
        </view>
        <view class="item car" @tap="goCart">
          <view class="car-num">
            <up-badge type="error" max="99" :value="cartNumber" />
          </view>
          <view class="car-icon">
            <u-icon name="shopping-cart" :size="20" color="#606266"></u-icon>
          </view>
          <view class="text u-line-1">购物车</view>
        </view>
      </view>
      <view class="right">
        <view class="cart btn u-line-1" @tap="onOpenSpec(1)">加入购物车</view>
        <view class="buy btn u-line-1" @tap="onOpenSpec(2)">立即购买</view>
      </view>
    </view>

    <!-- 服务弹出层 -->
    <u-popup
      mode="bottom"
      height="60%"
      :closeable="false"
      :closeOnClickOverlay="true"
      :show="serviceDescriptionPopup.open"
      @close="serviceDescriptionPopup.open = false"
    >
      <view class="service-description-popup">
        <view class="desc-title"
          >说明
          <view class="float-r">
            <u-icon
              name="close"
              color="#909399"
              size="22"
              @click="serviceDescriptionPopup.open = false"
            ></u-icon>
          </view>
        </view>
        <u-collapse>
          <u-collapse-item
            v-for="(serviceDescription, index) in serviceDescriptionPopup.data"
            :key="index"
            :name="serviceDescription.id"
            :title="serviceDescription.title"
            :clickable="false"
          >
            <text class="desc-content">{{ serviceDescription.description }}</text>
          </u-collapse-item>
        </u-collapse>
      </view>
    </u-popup>

    <!-- 选择规格弹出层 -->
    <u-popup
      mode="bottom"
      height="60%"
      :closeable="false"
      :closeOnClickOverlay="true"
      :show="selectedSpec.open"
      @close="selectedSpec.open = false"
    >
      <view class="selected-spec-popup">
        <view class="spec-title"
          >{{ selectedSpec.title }}
          <view class="float-r">
            <u-icon
              name="close"
              color="#909399"
              size="22"
              @click="selectedSpec.open = false"
            ></u-icon>
          </view>
        </view>
        <view class="good-spec-content">
          <!-- 显示商品信息 -->
          <view class="spec-good-item">
            <up-row customStyle="margin-bottom: 10px">
              <up-col span="2">
                <up-image
                  width="95rpx"
                  height="95rpx"
                  :show-loading="true"
                  :src="selectedSpec.product.images"
                  :radius="4"
                />
              </up-col>
              <up-col span="10">
                <view class="spec-price-item">
                  <text class="spec-price">{{ selectedSpec.product.price }}</text>
                  <text
                    class="spec-mkt-price rexshop-text-through u-margin-left-24 rexshop-text-gray"
                    v-show="selectedSpec.product.price != selectedSpec.product.mktPrice"
                    >{{ selectedSpec.product.mktPrice }}</text
                  >
                  <text class="spec-stock float-r">库存：{{ availableStock }}</text>
                </view>
                <view class="spec-desc-selected u-line-1"
                  >已选：{{ selectedSpec.product.spesDesc || goods.name }}</view
                >
              </up-col>
            </up-row>
          </view>

          <!-- 商品规格 -->
          <rexshop-spec
            ref="rexShopSpecRef"
            v-if="goods.spesDesc"
            :spesDesc="goods.spesDesc"
            :selectedProduct="selectedSpec.product"
            @change="onSpecChange"
          />

          <!-- 购买数量 -->
          <view class="buy-number-item">
            <view class="buy-num-name"><u-line margin="0rpx 0rpx 14rpx 0rpx" />数量</view>
            <u-number-box
              :step="1"
              :min="1"
              v-model="myCart.nums"
              :max="availableStock"
            />
          </view>

          <!-- 确定操作 -->
          <view class="buy-submit-item">
            <u-button
              type="error"
              size="default"
              shape="circle"
              v-if="availableStock > 0"
              @click="onSpecSubmitHandle"
              >确定</u-button
            >
            <u-button type="info" size="default" shape="circle" :disabled="true" v-else
              >已售罄</u-button
            >
          </view>
        </view>
      </view>
    </u-popup>

    <!-- 分享 -->
    <rexshop-share :contentHeight="420" ref="rexShopShareRef" />
    <u-toast ref="uToastRef" />
    <!-- #ifdef MP-WEIXIN -->
    <rexshop-login-mp v-if="useUserLoginStore().showMpLoginTip" />
    <!-- #endif -->

    <up-back-top :scroll-top="scrollTop" :duration="500"></up-back-top>
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import _ from "lodash";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { onLoad, onShow, onPageScroll } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { getApplicationConfigurationAsync } from "@/utils/other";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const scrollTop = ref(0);
const rexShopShareRef = ref();
const rexShopSpecRef = ref();
const uToastRef = ref();
const cellItemTitleStyle = ref({
  width: "85rpx",
  fontSize: "26rpx",
  color: "#aaaaaa",
});
const cellItemRightIconStyle = ref({
  fontSize: "26rpx",
  color: "#aaaaaa",
});
const serviceDescriptionPopup = ref({
  open: false,
  data: [] as ServiceDescriptionType[],
});
// 商品
const goods = ref({} as GoodsType);
// 商城服务描述
const serviceDescriptions = ref([] as ServiceDescriptionType[]);
// 商家推荐
const merchantRecommendItem = ref({
  platformLogo: null,
  shopName: null,
  shopMobile: null,
  shareTitle: null,
  reshipCoordinate: null,
  goodRecommendation: [] as GoodsType[],
});
// 商品参数
const goodParamItems = ref([] as any);
// 其它商品推荐数据
const otherGoodRecommends = ref([] as GoodsType[]);
// 选择规格
const selectedSpec = ref({
  open: false,
  type: 2,
  title: "选择规格",
  product: {} as ProductType,
});
// 购物车数量
const cartNumber = ref(0);
// 购物车
const myCart = reactive<CartType>({
  userId: "",
  productId: "",
  nums: 1,
  type: 1,
  objectId: "",
});
// 商品评价
const userGoodComments = ref<UserGoodCommentType[]>();

// 是否包含商品推荐数据
const isGoodRecommendList = computed(() => {
  return merchantRecommendItem.value.goodRecommendation.length > 0;
});

// 是否包含其它商品推荐数据
const isOtherGoodRecommendList = computed(() => {
  return otherGoodRecommends.value.length > 0;
});

// 图集
const album = computed(() => {
  if (goods.value && goods.value.images) {
    return goods.value.images.split(";");
  }
  return [];
});

// 服务项
const serviceDescriptionItem = computed(() => {
  let commonQuestion = serviceDescriptions.value.filter((p) => p.type == 1);
  let service = serviceDescriptions.value.filter((p) => p.type == 2);
  let delivery = serviceDescriptions.value.filter((p) => p.type == 3);
  let serviceItem = {
    commonQuestion,
    service,
    delivery,
  };
  return serviceItem;
});

// 商品参数
const isGoodParam = computed(() => {
  return goodParamItems.value && goodParamItems.value.length > 0;
});

// 显示服务信息
const openServiceDescription = (type: number) => {
  switch (type) {
    case 1:
      serviceDescriptionPopup.value.data = serviceDescriptionItem.value.commonQuestion;
      break;
    case 2:
      serviceDescriptionPopup.value.data = serviceDescriptionItem.value.service;
      break;
    case 3:
      serviceDescriptionPopup.value.data = serviceDescriptionItem.value.delivery;
      break;

    default:
      break;
  }
  serviceDescriptionPopup.value.open = true;
};

// 分享
const goShare = () => {
  rexShopShareRef.value.toggleMask();
};

// 商品规格切换
const onSpecChange = (spesDesc: string) => {
  let product = goods.value.products.find((p) => p.spesDesc == spesDesc);
  if (product) {
    myCart.nums = 1;
    selectedSpec.value.product = product;
  }
};

// 加载商品明细
const loadGoodsDetail = (id: string) => {
  uni.showLoading({
    title: "加载中",
  });
  http<GoodsType>({
    method: "GET",
    url: "/api/front/aggregation/common/goods/" + id,
  })
    .then((result) => {
      if (!result) {
        uni.navigateBack();
        return;
      }
      goods.value = result;
      let defaultProduct = goods.value.products.find((p) => p.isDefault);
      if (defaultProduct) selectedSpec.value.product = _.cloneDeep(defaultProduct);
      if (result.goodParamsIds) {
        let paramIds = result.goodParamsIds.split(",");
        loadGoodParam(paramIds);
      }
    })
    .catch((err: any) => {
      uToastRef.value.warning("该商品已下架！");
      setTimeout(() => {
        uni.navigateBack();
      }, 3000);
      console.error("查询商品明细出错：", err);
    });
};

// 可用库存
const availableStock = computed(() => {
  if (!selectedSpec.value.product) return 0;
  return selectedSpec.value.product.stock - selectedSpec.value.product.freezeStock;
});

// 加载用户商品评价
const loadUserGoodComment = (id: string) => {
  uni.showLoading({
    title: "加载中",
  });
  http<TableResultType<UserGoodCommentType[]>>({
    method: "GET",
    url: "/api/front/aggregation/common/userGoodComments/" + id,
    data: {
      skipCount: 0,
      maxResultCount: 5,
    },
  })
    .then((result) => {
      if (!result || !result.items) return;
      userGoodComments.value = result.items;
    })
    .catch((err: any) => {
      console.error("加载用户商品评价出错：", err);
    });
};

// 加载商品参数
const loadGoodParam = (paramIds: string[]) => {
  http<GoodParamType[]>({
    method: "POST",
    url: "/api/front/aggregation/common/goods/param",
    data: paramIds,
  })
    .then((result) => {
      if (!result) return;
      let goodParameters = result;
      let parameters = goods.value.parameters.split("|");
      for (let i = 0; i < parameters.length; i++) {
        const id = parameters[i].split(":")[0];
        const value = parameters[i].split(":")[1];
        let param = goodParameters.find((p) => p.id == id);
        if (param) {
          goodParamItems.value.push({
            name: param.name,
            value: value,
          });
        }
      }
    })
    .catch((err: any) => {
      console.error("查询商品参数出错：", err);
    });
};

// 加载商品服务描述
const loadServiceDescription = () => {
  http<ServiceDescriptionType[]>({
    method: "GET",
    url: "/api/front/aggregation/common/serviceDescriptionAll",
  })
    .then((result) => {
      if (!result) return;
      serviceDescriptions.value = result;
    })
    .catch((err: any) => {
      console.error("查询商品明细出错：", err);
    });
};

// 加载Base服务配置信息
const loadBaseServiceConfig = () => {
  getApplicationConfigurationAsync("setting").then((appConfig) => {
    if (!appConfig) return;
    let settingValues = appConfig.values;
    merchantRecommendItem.value.platformLogo =
      settingValues["BaseService.PlatformSetting.PlatformLogo"];
    merchantRecommendItem.value.shopName =
      settingValues["BaseService.PlatformSetting.ShopName"];
    merchantRecommendItem.value.shareTitle =
      settingValues["BaseService.ShareSetting.ShareTitle"];
    merchantRecommendItem.value.shopMobile =
      settingValues["BaseService.UsersSetting.ShopMobile"];
    merchantRecommendItem.value.reshipCoordinate =
      settingValues["BaseService.OrderSetting.ReshipCoordinate"];
  });
};

// 获取商品推荐数据
const loadGoodRecommendations = () => {
  http<GoodsType[]>({
    method: "GET",
    url: "/api/front/aggregation/common/goods/recommend",
    data: {
      limit: 10,
      isRecommend: true,
    },
  })
    .then((result) => {
      if (!result) return;
      merchantRecommendItem.value.goodRecommendation = result;
    })
    .catch((err: any) => {
      console.error("获取商品推荐数据出错：", err);
    });
};

// 获取[其它]商品推荐数据
const loadOtherGoodRecommendations = () => {
  http<GoodsType[]>({
    method: "GET",
    url: "/api/front/aggregation/common/goods/recommend",
    data: {
      limit: 10,
      isRecommend: false,
    },
  })
    .then((result) => {
      if (!result) return;
      otherGoodRecommends.value = result;
    })
    .catch((err: any) => {
      console.error("获取[其它]商品推荐数据出错：", err);
    });
};

// 收藏
const onGoodCollection = () => {
  if (!useUserLoginStore().hasLogin) {
    useUserLoginStore().showAuthLogin();
    return;
  }
  http<boolean>({
    method: "POST",
    url: "/api/good/common/good-collection/" + goods.value.id,
    data: myCart,
  })
    .then((result) => {
      if (!result) return;
      goods.value.isFav = !goods.value.isFav;
    })
    .catch((err: any) => {
      console.error("商品[收藏]出错：", err);
    });
};

// 跳转到购物车
const goCart = () => {
  uni.switchTab({
    url: "/pages/cart/index",
  });
};

// 选择规格
const onOpenSpec = (type: number) => {
  selectedSpec.value.type = type;
  selectedSpec.value.open = true;
};

// 选择规格确定
const onSpecSubmitHandle = () => {
  // 验证是否登陆
  if (!useUserLoginStore().hasLogin) {
    useUserLoginStore().showAuthLogin();
    selectedSpec.value.open = false;
    return;
  }
  selectedSpec.value.open = false;
  myCart.userId = useUserLoginStore().currentSysUser?.id ?? "";
  myCart.productId = selectedSpec.value.product.id;

  // 添加到购物车
  addCart();
};

// 添加购物车
const addCart = () => {
  http<UserShipType>({
    method: "POST",
    url: "/api/order/cart",
    data: myCart,
  })
    .then((result) => {
      if (!result) return;
      loadCartNumber();
      if (selectedSpec.value.type == 2) {
        // 立即购买
        uni.navigateTo({
          url: "/pages/placeOrder/index?cartIds=" + JSON.stringify([result.id]),
        });
      }
    })
    .catch((err: any) => {
      console.error("添加[购物车]出错：", err);
    });
};

// 获取购物车数量
const loadCartNumber = () => {
  http<number>({
    method: "GET",
    url: "/api/order/cart/number?type=1",
  })
    .then((result) => {
      if (!result || !_.isNumber(result)) return;
      cartNumber.value = result;
    })
    .catch((err: any) => {
      console.error("获取购物车数量出错：", err);
    });
};

// 加载数据
onLoad((opt: any) => {
  if (!opt.id) {
    uni.navigateBack();
    return;
  }

  // 获取商品服务描述
  loadServiceDescription();

  // 获取Base服务配置信息
  loadBaseServiceConfig();

  // 获取商品信息
  loadGoodsDetail(opt.id);

  // 获取用户评价
  loadUserGoodComment(opt.id);

  // 获取商家-商品推荐
  loadGoodRecommendations();

  // 获取其它商品推荐
  loadOtherGoodRecommendations();
});

onShow(() => {
  // 获取购物车数量
  if (useUserLoginStore().hasLogin) {
    loadCartNumber();
  }
});

onPageScroll((e) => {
  scrollTop.value = e.scrollTop;
});
</script>

<style lang="scss">
.recommend-divider .u-divider {
  margin: 0px !important;
}
</style>

<style lang="scss" scoped>
.goods-detail-body {
  padding-bottom: 100rpx;
  .goods-price-box {
    position: relative;
    background-image: url("/static/images/good/titlebg.png");
    background-repeat: no-repeat;
    background-size: 100% 100%;
    padding: 10rpx 0rpx 10rpx 20rpx;
    color: #ffffff;
    width: 100%;
    background-color: #e54d42;
    color: #ffffff;
    .good-price {
      font-size: 40rpx;
    }
    .good-price::before {
      content: "¥";
      font-size: 80%;
      margin-right: 4rpx;
    }
    .good-mkt-price,
    .good-buy-count {
      font-size: 22rpx;
    }
  }

  .goods-name-box {
    padding: 20rpx 30rpx;
    background-color: white;
    .good-title-item {
      .good-brand {
        display: inline-block;
      }
      .good-title {
        font-weight: bold;
        margin-left: 10rpx;
      }
    }
    .good-brief {
      margin-top: 10rpx;
      padding: 10rpx 15rpx;
      border-radius: 5rpx;
      color: #e54d42;
      background-color: #fadbd9;
    }
  }

  .product-spes-desc {
    font-size: 24rpx;
    width: 85%;
    color: #666666;
    .desc-icon {
      float: left;
    }
  }

  .good-service-box {
    margin-top: 15rpx;
    background-color: white;
  }

  .good-delivery-box {
    margin-top: 15rpx;
    background-color: white;
  }

  .good-comment-box {
    margin-top: 15rpx;
    background-color: white;
    .comment-title-item {
      padding: 15rpx 30rpx;
      .comment-count {
        font-size: 28rpx;
      }
      .comment-more {
        float: right;
      }
    }
    .comment-content-item {
      padding: 15rpx 30rpx;
      color: #9f9f9f;
      .comment-user-name {
        text-align: left;
        font-size: 28rpx;
        padding: 5rpx;
      }
      .comment-content {
        text-align: left;
        font-size: 26rpx;
        padding: 8rpx;
      }
      .comment-date-addon {
        text-align: left;
        padding: 8rpx;
        font-size: 22rpx;
        color: #b4b4b4;
      }
    }
  }

  .good-recommend-box {
    padding: 20rpx 30rpx;
    margin-top: 15rpx;
    background-color: white;

    .good-merchant-item {
      .good-shop-name {
        font-size: 26rpx;
        margin-bottom: 5rpx;
      }
      .good-share-title {
        font-size: 24rpx;
      }
      .merchant-position-item {
        margin-top: 15rpx;
        padding: 10rpx 0rpx;
        border-top: 1px #eaebeb solid;
        border-bottom: 1px #eaebeb solid;
        .position-title {
          font-size: 24rpx;
          display: inline-block;
          text {
            float: left;
            margin-top: 5rpx;
          }
          .tag-position {
            margin-right: 10rpx;
            float: left;
          }
          .position-content {
            padding-top: 6rpx;
          }
        }
      }
    }

    .good-recommend-item {
      .recommend-title {
        font-size: 28rpx;
        padding: 15rpx 0rpx 8rpx 0rpx;
      }
      .recommend-scroll-box {
        .recommend-scroll {
          .goods-content {
            .good-item-box {
              width: 100%;

              .good-content {
                background-color: white;
                margin: 0rpx 6rpx;
                // padding: 10rpx;
                margin-top: 10rpx;
                border-radius: 10rpx;
                .good-img {
                  width: 100%;
                  height: 220rpx;
                  border-radius: 8rpx;
                }

                .good-title {
                  font-size: 26rpx;
                  margin-top: 5px;
                  height: 68rpx;
                  color: $u-main-color;
                }
                .good-price {
                  font-size: 30rpx;
                  color: $u-error;
                  margin-top: 5px;
                }
              }
            }
          }
        }
      }
    }
  }

  .good-detail-box {
    margin-top: 15rpx;
    background-color: white;
    .detail-title {
      font-size: 28rpx;
      color: #666666;
      padding-top: 10rpx;
      height: 50rpx;
      line-height: 50rpx;
    }
    .detail-param {
      padding: 0rpx 8rpx;
      .detail-param-item {
        font-size: 26rpx;
        .param-name {
          color: #aaaaaa;
        }
        .param-name::after {
          content: "：";
        }
        .param-value {
          color: #666666;
        }
      }
    }
    .detail-content {
      display: contents;
    }
  }

  .common-desc-box {
    margin-top: 15rpx;
    background-color: white;
    padding: 10rpx 20rpx;
    display: block;
    .desc-title {
      font-size: 26rpx;
      color: #666666;
      height: 40rpx;
      line-height: 40rpx;
    }
    .question-title {
      padding: 0rpx 10%;
      text-align: center;
    }
    .question-desc {
      font-size: 26rpx;
      color: #666666;
    }
    .desc-more {
      width: 100%;
      padding: 10rpx 0rpx 5rpx 0rpx;
      border-top: 1rpx #eeeeee solid;
      .text-more {
        display: block;
        margin: 0rpx auto;
        width: 160rpx;
        color: #2b85e4;
        font-size: 26rpx;
      }
    }
  }

  .good-other-recommend-box {
    margin-top: 15rpx;

    .recommend-divider {
      margin: 0rpx !important;
      background-color: white;
      height: 70rpx;
      line-height: 70rpx;
      padding: 0rpx 20rpx;
    }

    .good-other-recommend-item {
      padding: 5rpx 15rpx 10rpx 15rpx;
      .goods-content {
        .good-item-box {
          width: 100%;

          .good-content {
            background-color: white;
            margin: 0rpx 6rpx;
            margin-top: 10rpx;
            border-radius: 10rpx;
            padding: 10rpx;
            .good-img {
              width: 100%;
              height: 340rpx;
              border-radius: 8rpx;
            }

            .good-title {
              font-size: 26rpx;
              margin-top: 5px;
              height: 68rpx;
              color: $u-main-color;
            }
            .good-price {
              font-size: 30rpx;
              color: $u-error;
              margin-top: 5px;
            }
          }
        }
      }
    }
  }

  .navigation-box {
    display: flex;
    position: fixed;
    bottom: 0;
    width: 100%;
    z-index: 1;
    margin-top: 100rpx;
    border: solid 2rpx #f2f2f2;
    background-color: #ffffff;
    padding: 16rpx 10rpx;
    .left {
      display: flex;
      font-size: 20rpx;
      .item {
        margin: 0 20rpx;
        text-align: center;
        .fav-icon {
          display: inline-block;
        }
        &.car {
          text-align: center;
          position: relative;
          .car-num {
            position: absolute;
            top: -10rpx;
            right: -18rpx;
          }
          .car-icon {
            display: inline-block;
          }
        }
      }
    }
    .right {
      display: flex;
      font-size: 28rpx;
      align-items: center;
      position: relative;
      right: 20rpx;
      .btn {
        line-height: 66rpx;
        padding: 0 40rpx;
        border-radius: 36rpx;
        color: #ffffff;
      }
      .cart {
        background-color: #ed3f14;
        margin: 0rpx 26rpx;
      }
      .buy {
        background-color: #ff7900;
      }
    }
  }
}

.recommend-scroll-box .recommend-scroll {
  .u-grid-item:nth-child(1) .good-item-box .good-content,
  .u-grid-item:nth-child(4) .good-item-box .good-content {
    margin-left: 0rpx !important;
  }
  .u-grid-item:nth-child(3) .good-item-box .good-content,
  .u-grid-item:nth-child(6) .good-item-box .good-content {
    margin-right: 0rpx !important;
  }
}

.good-other-recommend-box .good-other-recommend-item {
  .u-grid-item:nth-child(odd) .good-item-box .good-content {
    margin-left: 0rpx !important;
  }

  .u-grid-item:nth-child(even) .good-item-box .good-content {
    margin-right: 0rpx !important;
  }
}

.service-description-popup {
  min-height: 510rpx;
  .desc-title {
    font-size: 30rpx;
    padding: 20rpx;
    text-align: center;
    background-color: #f2f4f8;
  }
  .desc-content {
    color: #909193;
    font-size: 26rpx;
  }
}

.selected-spec-popup {
  min-height: 510rpx;
  .spec-title {
    font-size: 30rpx;
    padding: 20rpx;
    text-align: center;
    background-color: #f2f4f8;
  }
  .good-spec-content {
    .spec-good-item {
      padding: 15rpx 20rpx;
      color: #333333;

      .spec-price-item {
        font-size: 30rpx;
        .spec-price {
          color: #f56c6c;
        }
        .spec-price::before,
        .spec-mkt-price::before {
          content: "¥";
          font-size: 22rpx;
          text-decoration: none;
        }
        .spec-stock {
          font-size: 26rpx;
        }
      }
      .spec-desc-selected {
        font-size: 26rpx;
      }
    }

    .buy-number-item {
      padding: 0rpx 26rpx 14rpx 26rpx;
      .buy-num-name {
        font-size: 26rpx;
        margin-bottom: 10rpx;
      }
    }

    .buy-submit-item {
      margin: 14rpx 26rpx;
    }
  }
}
</style>
