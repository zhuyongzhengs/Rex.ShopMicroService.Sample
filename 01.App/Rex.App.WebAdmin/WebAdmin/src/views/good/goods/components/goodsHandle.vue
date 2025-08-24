<template>
  <div class="good-handle-container">
    <el-row>
      <el-col :span="24">
        <div class="good-handle-box">
          <el-tabs
            type="border-card"
            v-model="activeName"
            class="ur-tabs"
            id="good_tabs"
            :style="'height:' + tabsHeight + 'px'"
          >
            <el-tab-pane label="基础信息" name="BasicInfo">
              <basic-info-handle
                ref="basicInfoHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.basicInfo = $event"
              />
            </el-tab-pane>
            <el-tab-pane label="图集/视频" name="ImageOrVideo">
              <image-or-video-handle
                ref="imageOrVideoHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.imageOrVideo = $event"
              />
            </el-tab-pane>
            <el-tab-pane label="SKU/货品设置" name="SetSkuProduct">
              <set-sku-product-handle
                ref="setSkuProductHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.setSkuProduct = $event"
                @openSkuChange="onOpenSkuChange"
              />
            </el-tab-pane>
            <el-tab-pane label="参数设置" name="SetParam">
              <set-param-handle
                ref="paramHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.setParam = $event"
              />
            </el-tab-pane>
            <el-tab-pane label="会员折扣" name="VipDiscount">
              <grade-handle
                ref="gradeHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.goodGrades = $event"
              />
            </el-tab-pane>
            <el-tab-pane label="商品详情" name="ShopDetail">
              <intro-handle
                ref="introHandleRef"
                :isLook="props.isLook"
                @submitFormData="state.ruleForm.intro = $event"
              />
            </el-tab-pane>
          </el-tabs>
        </div>
      </el-col>
      <el-col :span="24">
        <div class="card-submit-content">
          <el-button
            v-if="!props.isLook"
            type="success"
            class="mr10"
            @click="saveFormData"
            >保存</el-button
          >
          <el-button type="default" @click="goodCancel">返回</el-button>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang="ts" name="goodHandleDialog">
import { defineAsyncComponent, reactive, ref, onMounted, defineProps } from "vue";
import { ElMessage, ElLoading } from "element-plus";
import _ from "lodash";
import { useRoute } from "vue-router";
import mittBus from "/@/utils/mitt";
import { useGoodsApi } from "/@/api/good/goods/index";

// 引入组件
const BasicInfoHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/basicInfoHandle.vue")
);
const ImageOrVideoHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/imageOrVideoHandle.vue")
);
const SetSkuProductHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/setSkuProductHandle.vue")
);
const SetParamHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/setParamHandle.vue")
);
const GradeHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/gradeHandle.vue")
);
const IntroHandle = defineAsyncComponent(
  () => import("/@/views/good/goods/components/introHandle.vue")
);

// 定义父组件传过来的值
const props = defineProps({
  // 商品ID
  id: {
    type: String,
    default: () => null,
  },
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const goodsApi = useGoodsApi();
const route = useRoute();
const tabsHeight = ref(0);
const activeName = ref("BasicInfo"); // 默认选中[基础信息]
const basicInfoHandleRef = ref();
const imageOrVideoHandleRef = ref();
const setSkuProductHandleRef = ref();
const paramHandleRef = ref();
const gradeHandleRef = ref();
const introHandleRef = ref();
const state = reactive({
  ruleForm: {
    basicInfo: {}, // 基础信息
    imageOrVideo: {}, // 图集/视频
    setSkuProduct: {}, // SKU/货品设置
    setParam: {}, // 参数设置
    goodGrades: [], // 会员等级
    intro: null, // 商品明细
  },
});

// 保存表单信息
const saveFormData = async () => {
  // 验证
  if (!(await submitBasicInfoValidAsync())) return;
  if (!(await submitImageOrVideoValidAsync())) return;
  if (!(await submitSetSkuProductValidAsync())) return;
  if (!(await submitSetParamValidAsync())) return;
  if (!(await submitGradeValidAsync())) return;
  if (!(await submitIntroValidAsync())) return;

  // 验证成功 ==> 提交
  if (props.id) updateGoodDetail();
  else addGoodDetail();
};

// 添加商品
const addGoodDetail = () => {
  goodsApi
    .addGoods(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        setTimeout(() => {
          goodCancel();
          onGoodSearch();
        }, 3 * 1000);
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 修改商品
const updateGoodDetail = () => {
  goodsApi
    .updateGoods(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("修改成功！");
        setTimeout(() => {
          goodCancel();
          onGoodSearch();
        }, 3 * 1000);
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 触发商品查询
const onGoodSearch = () => {
  setTimeout(() => {
    let goodSearchBtnEle = document.getElementById("good_search_btn");
    if (goodSearchBtnEle) {
      goodSearchBtnEle.click();
    }
  }, 500);
};

// 提交[基础信息]验证
const submitBasicInfoValidAsync = async () => {
  const basicInfoFormRef = basicInfoHandleRef.value.basicInfoFormRef;
  const basicInfoValidResult = await basicInfoHandleRef.value.submitValidateAsync(
    basicInfoFormRef
  );
  if (!basicInfoValidResult) {
    ElMessage.warning("【基础信息】还有尚未填写的信息！");
    activeName.value = "BasicInfo";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[基础信息]验证成功。", state.ruleForm.basicInfo);
  return await new Promise((resolve) => resolve(true));
};

// 提交[图集/视频]验证
const submitImageOrVideoValidAsync = async () => {
  const imageOrVideoFormRef = imageOrVideoHandleRef.value.imageOrVideoFormRef;
  const imageOrVideoValidResult = await imageOrVideoHandleRef.value.submitValidateAsync(
    imageOrVideoFormRef
  );
  if (!imageOrVideoValidResult) {
    ElMessage.warning("【图集/视频】还有尚未填写的信息！");
    activeName.value = "ImageOrVideo";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[图集/视频]验证成功。", state.ruleForm.imageOrVideo);
  return await new Promise((resolve) => resolve(true));
};

// 提交[SKU/货品]验证
const submitSetSkuProductValidAsync = async () => {
  const setSkuProductFormRef = setSkuProductHandleRef.value.setSkuProductFormRef;
  const setSkuProductValidResult = await setSkuProductHandleRef.value.submitValidateAsync(
    setSkuProductFormRef
  );
  if (!setSkuProductValidResult) {
    ElMessage.warning("【SKU/货品】还有尚未填写的信息！");
    activeName.value = "SetSkuProduct";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[SKU/货品]验证成功。", state.ruleForm.setSkuProduct);
  return await new Promise((resolve) => resolve(true));
};

// 提交[参数设置]验证
const submitSetParamValidAsync = async () => {
  const paramFormRef = paramHandleRef.value.paramFormRef;
  const paramValidResult = await paramHandleRef.value.submitValidateAsync(paramFormRef);
  if (!paramValidResult) {
    ElMessage.warning("【参数设置】还有尚未填写的信息！");
    activeName.value = "SetParam";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[参数设置]验证成功。", state.ruleForm.setParam);
  return await new Promise((resolve) => resolve(true));
};

// 提交[会员折扣]验证
const submitGradeValidAsync = async () => {
  const gradeFormRef = gradeHandleRef.value.gradeFormRef;
  const gradeValidResult = await gradeHandleRef.value.submitValidateAsync(gradeFormRef);
  if (!gradeValidResult) {
    ElMessage.warning("【会员折扣】还有尚未填写的信息！");
    activeName.value = "VipDiscount";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[会员折扣]验证成功。", state.ruleForm.goodGrades);
  return await new Promise((resolve) => resolve(true));
};

// 提交[商品详情]验证
const submitIntroValidAsync = async () => {
  const introFormRef = introHandleRef.value.introFormRef;
  const introValidResult = await introHandleRef.value.submitValidateAsync(introFormRef);
  if (!introValidResult) {
    ElMessage.warning("【商品详情】还有尚未填写的信息！");
    activeName.value = "VipDiscount";
    return await new Promise((resolve) => resolve(false));
  }
  console.log("验证完成，[商品详情]验证成功。", state.ruleForm.intro);
  return await new Promise((resolve) => resolve(true));
};

// 返回
const goodCancel = () => {
  mittBus.emit(
    "onCurrentContextmenuClick",
    Object.assign({}, { contextMenuClickId: 1, ...route })
  );
};

// 查询商品信息
const getGoodById = (id: string) => {
  const loading = ElLoading.service({
    lock: true,
    text: "Loading",
    background: "rgba(0, 0, 0, 0.1)",
  });
  goodsApi
    .getGoodById(id)
    .then((result) => {
      loading.close();
      if (result) {
        state.ruleForm = result;
        setGoodDetail();
      }
    })
    .catch((err: any) => {
      loading.close();
      console.error("查询商品信息出错：", err);
    });
};

// 设置商品信息值
const setGoodDetail = () => {
  // 基本信息
  let intervalBasicInfo = setInterval(() => {
    if (basicInfoHandleRef.value) {
      window.clearInterval(intervalBasicInfo);
      basicInfoHandleRef.value.setInitValue(state.ruleForm.basicInfo);
    }
  }, 500);

  // 图集视频
  let intervalImageOrVideo = setInterval(() => {
    if (imageOrVideoHandleRef.value) {
      window.clearInterval(intervalImageOrVideo);
      imageOrVideoHandleRef.value.setInitValue(state.ruleForm.imageOrVideo);
    }
  }, 500);

  // SKU/货品
  let intervalSetSkuProduct = setInterval(() => {
    if (setSkuProductHandleRef.value) {
      window.clearInterval(intervalSetSkuProduct);
      setSkuProductHandleRef.value.setInitValue(state.ruleForm.setSkuProduct);
    }
  }, 500);

  // 参数设置
  let intervalSetParam = setInterval(() => {
    if (paramHandleRef.value) {
      window.clearInterval(intervalSetParam);
      paramHandleRef.value.setInitValue(state.ruleForm.setParam);
    }
  }, 500);

  // 会员折扣
  let intervalGoodGrades = setInterval(() => {
    if (gradeHandleRef.value) {
      window.clearInterval(intervalGoodGrades);
      gradeHandleRef.value.setInitValue(state.ruleForm.goodGrades);
    }
  }, 500);

  // 商品详情
  let intervalIntro = setInterval(() => {
    if (introHandleRef.value) {
      window.clearInterval(intervalIntro);
      introHandleRef.value.setInitValue(state.ruleForm.intro);
    }
  }, 500);
};

// 商品SKU切换
const onOpenSkuChange = (val: boolean) => {
  if (paramHandleRef.value) paramHandleRef.value.setIsOpenSku(val);
};

onMounted(() => {
  tabsHeight.value = window.innerHeight - 180;
  if (props.id) getGoodById(props.id);
});

// 暴露变量
defineExpose({
  // ..
});
</script>
<style lang="scss">
// .good-handle-container {
//   .el-tabs__content {
//     padding-left: 0px !important;
//     padding-right: 0px !important;
//   }
// }
</style>
<style scoped lang="scss">
.good-handle-container {
  .ur-tabs {
    overflow-y: auto;
    scroll-behavior: smooth;
    transition: scroll-behavior 0.3s;
  }
}

.card-submit-content {
  height: 65px;
  line-height: 65px;
  background-color: white;
  text-align: center;
}
</style>
