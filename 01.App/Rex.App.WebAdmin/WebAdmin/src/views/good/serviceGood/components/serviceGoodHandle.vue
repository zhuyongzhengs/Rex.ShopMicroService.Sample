<template>
  <div class="serviceGood-handle-container">
    <el-form
      ref="serviceGoodFormRef"
      class="form-item-content"
      :model="state.ruleForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="100px"
      :style="'height:' + itemHeight + 'px'"
    >
      <el-row :gutter="0">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="项目名称" prop="title">
            <el-input
              placeholder="请输入项目名称"
              maxlength="40"
              v-model="state.ruleForm.title"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="售价" prop="money">
            <el-input
              v-model="state.ruleForm.money"
              size="default"
              placeholder="售价"
              @input="
                state.ruleForm.money = verifyNumberIntegerAndFloat(state.ruleForm.money)
              "
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="18" :xl="24" class="mb20">
          <el-form-item label="项目缩略图" prop="thumbnail">
            <el-input
              placeholder="请输入项目缩略图链接(后续换成上传)"
              v-model="state.ruleForm.thumbnail"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="项目概述" prop="description">
            <el-input
              placeholder="请输入项目概述（一句话概括即可）"
              maxlength="500"
              v-model="state.ruleForm.description"
              :autosize="{ minRows: 2, maxRows: 4 }"
              type="textarea"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="允许会员" prop="allowedMemberships">
            <el-select
              v-model="state.ruleForm.allowedMemberships"
              multiple
              placeholder="请选择"
              clearable
              class="w100"
            >
              <el-option
                v-for="(allowedMembership, index) in state.allowedMembershipItems"
                :label="allowedMembership.title"
                :value="allowedMembership.id"
                :key="index"
              ></el-option>
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="核销门店" prop="consumableStores">
            <el-select
              v-model="state.ruleForm.consumableStores"
              multiple
              placeholder="请选择"
              clearable
              class="w100"
            >
              <el-option
                v-for="(
                  allowedConsumableStore, index
                ) in state.allowedConsumableStoreItems"
                :label="allowedConsumableStore.storeName"
                :value="allowedConsumableStore.id"
                :key="index"
              ></el-option>
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="项目状态" prop="status">
            <el-select
              size="default"
              v-model="state.ruleForm.status"
              placeholder="请选择项目状态"
            >
              <el-option
                v-for="(status, index) in serviceGoodStatusItems"
                :key="index"
                :label="status.description"
                :value="status.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="重复购买数" prop="maxBuyNumber">
            <el-input-number
              v-model="state.ruleForm.maxBuyNumber"
              :precision="0"
              :min="0"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="可销售数量" prop="amount">
            <el-input-number v-model="state.ruleForm.amount" :precision="0" :min="0" />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="服务券数量" prop="ticketNumber">
            <el-input-number
              v-model="state.ruleForm.ticketNumber"
              :precision="0"
              :min="0"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb20">
          <el-form-item label="可购买时间" prop="startTimeToEndTimes">
            <el-date-picker
              v-model="state.ruleForm.startTimeToEndTimes"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="14" :xl="24" class="mb20">
          <div class="form-tip">说明：在此时间段内，前端才能进行显示并购买.</div>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
          <el-form-item label="有效期类型" prop="validityType">
            <el-select
              size="default"
              v-model="state.ruleForm.validityType"
              placeholder="请选择有效期类型"
            >
              <el-option
                v-for="(validityType, index) in serviceGoodValidityTypeItems"
                :key="index"
                :label="validityType.description"
                :value="validityType.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col
          v-show="state.ruleForm.validityType == '2'"
          :xs="24"
          :sm="24"
          :md="24"
          :lg="10"
          :xl="24"
          class="mb20"
        >
          <el-form-item label="核销时间段" prop="validityStartTimeToValidityEndTimes">
            <el-date-picker
              v-model="state.ruleForm.validityStartTimeToValidityEndTimes"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-form-item>
        </el-col>
        <el-col
          v-show="state.ruleForm.validityType == '2'"
          :xs="24"
          :sm="24"
          :md="24"
          :lg="8"
          :xl="24"
          class="mb20"
        >
          <el-form-item label-width="15">
            <el-button
              size="default"
              type="info"
              plain
              @click="
                state.ruleForm.validityStartTimeToValidityEndTimes = getThreeMonthsLater(
                  'YYYY-mm-dd HH:MM:SS'
                )
              "
              >三月内</el-button
            >
            <el-button
              size="default"
              type="info"
              plain
              @click="
                state.ruleForm.validityStartTimeToValidityEndTimes = getSixMonthsLater(
                  'YYYY-mm-dd HH:MM:SS'
                )
              "
              >半年内</el-button
            >
            <el-button
              size="default"
              type="info"
              plain
              @click="
                state.ruleForm.validityStartTimeToValidityEndTimes = getOneYearLater(
                  'YYYY-mm-dd HH:MM:SS'
                )
              "
              >一年内</el-button
            >
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="详细说明" prop="contentBody">
            <editor v-model:get-html="state.ruleForm.contentBody" />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <el-row :gutter="0">
      <el-col :span="24">
        <div class="card-submit-content">
          <el-button
            v-if="!props.isLook"
            type="success"
            class="mr10"
            @click="submitValidate(serviceGoodFormRef)"
            >保存</el-button
          >
          <el-button type="default" @click="serviceGoodCancel">返回</el-button>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang="ts" name="serviceGoodHandleDialog">
import { defineAsyncComponent, reactive, ref, onMounted, defineProps } from "vue";
import { ElMessage, ElLoading } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import { useRoute } from "vue-router";
import mittBus from "/@/utils/mitt";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import {
  getThreeMonthsLater,
  getSixMonthsLater,
  getOneYearLater,
} from "/@/utils/formatTime";
import { useServiceGoodApi } from "/@/api/good/serviceGood/index";
import { useUserGradeApi } from "/@/api/system/userGrade/index";
import { useGoodStoreApi } from "/@/api/shopSetting/store/index";

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

// 引入组件
const Editor = defineAsyncComponent(() => import("/@/components/editor/index.vue"));

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 定义变量内容
const serviceGoodApi = useServiceGoodApi();
const userGradeApi = useUserGradeApi();
const goodStoreApi = useGoodStoreApi();
const route = useRoute();
const serviceGoodFormRef = ref();
const serviceGoodStatusItems = ref([] as EnumValueType[]);
const serviceGoodValidityTypeItems = ref([] as EnumValueType[]);
const itemHeight = ref(0);
const state = reactive({
  allowedMembershipItems: [] as RowUserGradeType[],
  allowedConsumableStoreItems: [] as RowStoreType[],
  ruleForm: {
    title: null,
    thumbnail: null,
    description: null,
    contentBody: "",
    allowedMemberships: [],
    consumableStores: [],
    status: "",
    maxBuyNumber: 0,
    amount: 999,
    startTimeToEndTimes: [] as string[],
    startTime: "",
    endTime: "",
    validityType: "",
    validityStartTimeToValidityEndTimes: [] as string[],
    validityStartTime: "",
    validityEndTime: "",
    ticketNumber: 5,
    money: "",
    concurrencyStamp: null,
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  title: [{ required: true, message: "请输入项目名称！", trigger: "blur" }],
  money: [{ required: true, message: "请输入售价！", trigger: "blur" }],
  thumbnail: [{ required: true, message: "请输入项目缩略图！", trigger: "blur" }],
  description: [{ required: true, message: "请输入项目概述！", trigger: "blur" }],
  allowedMemberships: [{ required: true, message: "请选择允许会员！", trigger: "blur" }],
  consumableStores: [{ required: true, message: "请选择核销门店！", trigger: "blur" }],
  status: [{ required: true, message: "请选择项目状态！", trigger: "blur" }],
  maxBuyNumber: [{ required: true, message: "请输入重复购买数！", trigger: "blur" }],
  amount: [{ required: true, message: "请输入可销售数量！", trigger: "blur" }],
  ticketNumber: [{ required: true, message: "请输入服务卷数量！", trigger: "blur" }],
  startTimeToEndTimes: [
    { required: true, message: "请选择可购买时间！", trigger: "blur" },
  ],
  validityType: [
    { required: true, message: "请选择有效期类型！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          state.ruleForm.validityType == "2" &&
          state.ruleForm.validityStartTimeToValidityEndTimes.length < 1
        ) {
          callback(new Error("请选择核销时间段！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  contentBody: [{ required: true, message: "请输入详细说明！", trigger: "blur" }],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      if (!props.id) addServiceGood();
      else updateServiceGood();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 生成表单数据
const generateServiceGoodForm = () => {
  let formItem = _.cloneDeep(state.ruleForm) as any;
  if (formItem.startTimeToEndTimes.length > 1) {
    formItem.startTime = formItem.startTimeToEndTimes[0];
    formItem.endTime = formItem.startTimeToEndTimes[1];
    delete formItem.startTimeToEndTimes;
  }
  if (state.ruleForm.validityType != "2") {
    formItem.validityStartTimeToValidityEndTimes = [];
    formItem.validityStartTime = null;
    formItem.validityEndTime = null;
  }
  if (formItem.validityStartTimeToValidityEndTimes.length > 1) {
    formItem.validityStartTime = formItem.validityStartTimeToValidityEndTimes[0];
    formItem.validityEndTime = formItem.validityStartTimeToValidityEndTimes[1];
    delete formItem.validityStartTimeToValidityEndTimes;
  }
  return formItem;
};

// 添加商品
const addServiceGood = () => {
  let addFormItem = generateServiceGoodForm();
  serviceGoodApi
    .addServiceGood(addFormItem)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        setTimeout(() => {
          serviceGoodCancel();
          onServiceGoodSearch();
        }, 3 * 1000);
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 修改商品
const updateServiceGood = () => {
  let updateFormItem = generateServiceGoodForm();
  serviceGoodApi
    .updateServiceGood(props.id, updateFormItem)
    .then((result) => {
      if (result) {
        ElMessage.success("修改成功！");
        setTimeout(() => {
          serviceGoodCancel();
          onServiceGoodSearch();
        }, 3 * 1000);
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 触发商品查询
const onServiceGoodSearch = () => {
  setTimeout(() => {
    let serviceGoodSearchBtnEle = document.getElementById("serviceGood_search_btn");
    if (serviceGoodSearchBtnEle) {
      serviceGoodSearchBtnEle.click();
    }
  }, 500);
};

// 返回
const serviceGoodCancel = () => {
  mittBus.emit(
    "onCurrentContextmenuClick",
    Object.assign({}, { contextMenuClickId: 1, ...route })
  );
};

// 查询商品信息
const getServiceGoodById = (id: string) => {
  const loading = ElLoading.service({
    lock: true,
    text: "Loading",
    background: "rgba(0, 0, 0, 0.1)",
  });
  serviceGoodApi
    .getServiceGoodById(id)
    .then((result) => {
      if (result) {
        if (result.startTime && result.endTime) {
          result.startTimeToEndTimes = [result.startTime, result.endTime];
        }
        if (result.validityStartTime && result.validityEndTime) {
          result.validityStartTimeToValidityEndTimes = [
            result.validityStartTime,
            result.validityEndTime,
          ];
        }
        state.ruleForm = result;
      }
      loading.close();
    })
    .catch((err: any) => {
      loading.close();
      console.error("查询服务商品信息出错：", err);
    });
};

// 获取项目状态
const getServiceGoodStatuItems = () => {
  serviceGoodApi
    .getServiceGoodStatus()
    .then((result) => {
      if (result.length > 0) state.ruleForm.status = result[0].value;
      serviceGoodStatusItems.value = result;
    })
    .catch((err: any) => {
      console.error("查询状态出错：", err);
    });
};

// 获取服务核销类型
const getServiceGoodValidityTypeItems = () => {
  serviceGoodApi
    .getServiceValidityTypes()
    .then((result) => {
      if (result.length > 0) state.ruleForm.validityType = result[0].value;
      serviceGoodValidityTypeItems.value = result;
    })
    .catch((err) => {
      console.error("查询服务核销类型出错：", err);
    });
};

// 查询用户等级
const getUserGrade = () => {
  userGradeApi
    .getUserGradeList({
      title: "",
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 100,
    })
    .then((result) => {
      if (result) {
        state.allowedMembershipItems = result.items;
      }
    })
    .catch((err: any) => {
      console.error("查询用户等级出错：", err);
    });
};

// 查询商品门店
const getGoodStore = () => {
  goodStoreApi
    .getGoodStoreAll()
    .then((result) => {
      if (result) {
        state.allowedConsumableStoreItems = result;
      }
    })
    .catch((err: any) => {
      console.error("查询商品门店出错：", err);
    });
};

onMounted(() => {
  itemHeight.value = window.innerHeight - 180;
  if (props.id) getServiceGoodById(props.id);
  getServiceGoodStatuItems();
  getServiceGoodValidityTypeItems();
  getUserGrade();
  getGoodStore();
});

// 暴露变量
defineExpose({
  // ..
});
</script>
<style scoped lang="scss">
.serviceGood-handle-container {
  background-color: white;
  .form-item-content {
    border: 1px #e4e7ed solid;
    padding: 15px;
    // height: 550px;
    overflow-y: auto;
    scroll-behavior: smooth;
    transition: scroll-behavior 0.3s;
  }
}

.form-tip {
  height: 32px;
  line-height: 32px;
  color: #999;
  margin-left: 15px;
  background-color: #fafafa;
  padding-left: 10px;
}

.card-submit-content {
  height: 65px;
  line-height: 65px;
  background-color: white;
  text-align: center;
}
</style>
