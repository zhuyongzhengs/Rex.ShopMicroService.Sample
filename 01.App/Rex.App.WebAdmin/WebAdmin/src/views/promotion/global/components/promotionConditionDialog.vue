<template>
  <div class="promotion-condition-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="650px"
    >
      <el-form
        ref="promotionConditionDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
        class="condition-form-body"
      >
        <el-row :gutter="10">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="促销条件" prop="conditionType">
              <el-select
                v-model="promotionConditionType"
                size="default"
                style="width: 100%"
                placeholder="请选择促销条件"
                :disabled="state.dialog.submitTxt == '修 改'"
              >
                <el-option
                  v-for="(conditionType, index) in props.conditionTypes"
                  :key="index"
                  :value="conditionType.key"
                  :label="conditionType.description"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'GOODS_ALL'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <el-input v-model="state.goodsAllParameter.nums" type="hidden" />
              <div class="condition-desc">所有商品都满足条件</div>
            </el-form-item>
            <el-form-item label="条件">
              <div class="condition-desc">
                无需设置任何条件，直接点击“{{ state.dialog.submitTxt }}”吧。
              </div>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'GOODS_IDS'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <div class="condition-desc">指定商品ID</div>
            </el-form-item>
            <el-form-item label="商品" prop="goodsIds">
              <el-button
                type="info"
                size="default"
                plain
                :icon="Sell"
                @click="onOpenSelectGoods"
                >选择商品</el-button
              >
              <div class="selected-good-item">
                <el-tag
                  v-for="(good, index) in selectedGoodItems"
                  :key="index"
                  effect="light"
                  closable
                  @close="handleSelGoodClose(index)"
                >
                  {{ good.name }}
                </el-tag>
              </div>
            </el-form-item>
            <el-form-item label="商品数量" prop="goodsIdNums">
              <el-input-number
                v-model="state.goodsIdsParameter.nums"
                :precision="0"
                :min="1"
                class="mr20"
              />
              <span class="condition-desc">大于等于此商品数量才满足条件</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'GOODS_CATS'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <div class="condition-desc">指定商品分类</div>
            </el-form-item>
            <el-form-item label="商品分类" prop="goodsCategorys">
              <el-cascader
                :options="goodCategorData"
                :props="goodCategorProp"
                placeholder="请选择商品分类"
                clearable
                class="w100"
                size="default"
                v-model="state.goodsCatsParameter.catId"
                @change="goodCategoryChange"
              >
                <template #default="{ node, data }">
                  <span>{{ data.name }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
            <el-form-item label="商品数量" prop="goodsCategoryNums">
              <el-input-number
                v-model="state.goodsCatsParameter.nums"
                :precision="0"
                :min="1"
                class="mr20"
              />
              <span class="condition-desc">大于等于此商品数量才满足条件</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'GOODS_BRANDS'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <div class="condition-desc">指定商品品牌</div>
            </el-form-item>
            <el-form-item label="商品品牌" prop="goodsBrands">
              <el-select
                size="default"
                v-model="state.goodsBrandsParameter.brandId"
                placeholder="请选择品牌"
              >
                <el-option
                  v-for="(brand, index) in goodBrandData"
                  :key="index"
                  :label="brand.name"
                  :value="brand.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="商品数量" prop="goodsBrandNums">
              <el-input-number
                v-model="state.goodsBrandsParameter.nums"
                :precision="0"
                :min="1"
                class="mr20"
              />
              <span class="condition-desc">大于等于此商品数量才满足条件</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'ORDER_FULL'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <div class="condition-desc">满减</div>
            </el-form-item>
            <el-form-item label="满多少" prop="money">
              <el-input
                v-model="state.goodsOrderFullsParameter.money"
                size="default"
                placeholder="请输入满减金额"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsOrderFullsParameter.money = verifyNumberIntegerAndFloat(
                    state.goodsOrderFullsParameter.money
                  )
                "
              />
              <span class="condition-desc">订单金额满多少的时候，优惠</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="promotionConditionType == 'USER_GRADE'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="条件名称">
              <div class="condition-desc">指定用户等级</div>
            </el-form-item>
            <el-form-item label="用户等级" prop="userGrade">
              <el-select
                v-model="selectedUserGrades"
                multiple
                placeholder="请选择用户等级"
                style="width: 100%"
              >
                <el-option
                  v-for="userGrade in userGradeData"
                  :key="userGrade.id"
                  :label="userGrade.title"
                  :value="userGrade.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(promotionConditionDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
    <select-goods ref="selectGoodsRef" @selGoodsResult="onSelectGoodsResult" />
  </div>
</template>

<script setup lang="ts" name="promotionConditionDialog">
import {
  nextTick,
  reactive,
  ref,
  defineProps,
  defineAsyncComponent,
  onMounted,
} from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import { Sell } from "@element-plus/icons-vue";
import _ from "lodash";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { getGuidEmpty } from "/@/utils/other";
import { useGoodsApi } from "/@/api/good/goods/index";
import { useGoodCategoryApi } from "/@/api/good/category/index";
import { useGoodBrandApi } from "/@/api/good/brand/index";
import { useUserGradeApi } from "/@/api/system/userGrade/index";

const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);

// 定义子组件向父组件传值/事件
const emit = defineEmits(["condition-change"]);
const props = defineProps({
  // 促销条件类型
  conditionTypes: {
    type: Object,
    default: () => [{ key: null, value: null, description: null }],
  },
});

// 引入用户 Api 请求接口
const goodsApi = useGoodsApi();
const goodCategoryApi = useGoodCategoryApi();
const brandApi = useGoodBrandApi();
const userGradeApi = useUserGradeApi();

// 定义变量内容
const selectGoodsRef = ref();
const promotionConditionDialogFormRef = ref();
const promotionConditionType = ref("");
const selectedGoodItems = ref([] as RowGoodsType[]);
const maxSelectGoods = ref(10); // 选择商品最大数量
const goodCategorData = ref<RowGoodCategoryType[]>();
const goodBrandData = ref<RowGoodBrandType[]>();
const userGradeData = ref([] as RowUserGradeType[]);
const selectedUserGrades = ref([] as string[]);
const goodCategorProp = ref({
  checkStrictly: true,
  value: "id",
  label: "name",
});
const state = reactive({
  ruleForm: {
    id: getGuidEmpty(),
    promotionId: "",
    code: "",
    parameters: "",
    concurrencyStamp: "",
  },
  // 参数：所有商品满足条件
  goodsAllParameter: {
    nums: 1,
  },
  // 参数：指定某些商品满足条件
  goodsIdsParameter: {
    goods: "",
    nums: 1,
  },
  // 参数：指定商品分类满足条件
  goodsCatsParameter: {
    catId: "",
    nums: 1,
  },
  // 参数：指定商品品牌满足条件
  goodsBrandsParameter: {
    brandId: "",
    nums: 1,
  },
  // 参数：订单满××金额满足条件
  goodsOrderFullsParameter: {
    money: "",
  },
  // 参数：用户符合指定等级
  goodsUserGradesParameter: {
    grades: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

// 选择促销条件切换
const conditionTypeChange = () => {
  state.ruleForm.code = promotionConditionType.value;
  let parameters = "";
  switch (state.ruleForm.code) {
    case "GOODS_ALL":
      parameters = JSON.stringify(state.goodsAllParameter);
      break;
    case "GOODS_IDS":
      parameters = JSON.stringify(state.goodsIdsParameter);
      break;
    case "GOODS_CATS":
      parameters = JSON.stringify(state.goodsCatsParameter);
      break;
    case "GOODS_BRANDS":
      parameters = JSON.stringify(state.goodsBrandsParameter);
      break;
    case "ORDER_FULL":
      parameters = JSON.stringify(state.goodsOrderFullsParameter);
      break;
    case "USER_GRADE":
      parameters = JSON.stringify(state.goodsUserGradesParameter);
      break;
  }
  state.ruleForm.parameters = parameters;
};

// 选择促销条件还原
const conditionTypeRevert = () => {
  let parameters = JSON.parse(state.ruleForm.parameters);
  switch (state.ruleForm.code) {
    case "GOODS_ALL":
      state.goodsAllParameter = parameters;
      break;
    case "GOODS_IDS":
      state.goodsIdsParameter = parameters;
      getGoodByIds(state.goodsIdsParameter.goods.split(","));
      break;
    case "GOODS_CATS":
      state.goodsCatsParameter = parameters;
      break;
    case "GOODS_BRANDS":
      state.goodsBrandsParameter = parameters;
      break;
    case "ORDER_FULL":
      state.goodsOrderFullsParameter = parameters;
      break;
    case "USER_GRADE":
      state.goodsUserGradesParameter = parameters;
      selectedUserGrades.value = state.goodsUserGradesParameter.grades.split(",");
      break;
  }
};

// 根据商品ID查询商品
const getGoodByIds = (ids: string[]) => {
  goodsApi
    .getGoodByIds(ids)
    .then((result) => {
      if (result) {
        selectedGoodItems.value = result;
      }
    })
    .catch((err: any) => {
      console.error("查询商品信息出错：", err);
    });
};

// 移除选择的商品
const handleSelGoodClose = (index: number) => {
  selectedGoodItems.value.splice(index, 1);
  if (selectedGoodItems.value.length < 1) {
    state.goodsIdsParameter.goods = "";
    return;
  }
  state.goodsIdsParameter.goods = selectedGoodItems.value.map((p) => p.id).join(",");
};

// 打开选择商品
const onOpenSelectGoods = () => {
  selectGoodsRef.value.openDialog("选择商品");
};

// 选择的商品返回结果
const onSelectGoodsResult = (goods: RowGoodsType[]) => {
  if (goods.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择${maxSelectGoods.value}条商品！`);
    return;
  }
  for (let i = 0; i < goods.length; i++) {
    const good = goods[i];
    if (selectedGoodItems.value.filter((p) => p.id == good.id).length > 0) continue;
    selectedGoodItems.value.push(good);
  }
  state.goodsIdsParameter.goods = selectedGoodItems.value.map((p) => p.id).join(",");
};

// 商品分类切换
function goodCategoryChange(val: []): void {
  if (!val || val.length < 1) {
    state.goodsCatsParameter.catId = "";
    return;
  }
  state.goodsCatsParameter.catId = val[val.length - 1];
}

// 表单验证规则
const formRules = reactive<FormRules>({
  conditionType: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!promotionConditionType.value) {
          callback(new Error("请选择促销条件！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsIds: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          state.goodsIdsParameter.goods.length < 1 &&
          promotionConditionType.value == "GOODS_IDS"
        ) {
          callback(new Error("请选择商品！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsIdNums: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsIdsParameter.nums &&
          promotionConditionType.value == "GOODS_IDS"
        ) {
          callback(new Error("请输入商品数量！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsCategorys: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsCatsParameter.catId &&
          promotionConditionType.value == "GOODS_CATS"
        ) {
          callback(new Error("请选择商品分类！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsCategoryNums: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsCatsParameter.nums &&
          promotionConditionType.value == "GOODS_CATS"
        ) {
          callback(new Error("请输入商品数量！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsBrands: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsBrandsParameter.brandId &&
          promotionConditionType.value == "GOODS_BRANDS"
        ) {
          callback(new Error("请选择商品品牌！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsBrandNums: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsBrandsParameter.nums &&
          promotionConditionType.value == "GOODS_BRANDS"
        ) {
          callback(new Error("请输入商品数量！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  money: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !state.goodsOrderFullsParameter.money &&
          promotionConditionType.value == "ORDER_FULL"
        ) {
          callback(new Error("请输入满减金额！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  userGrade: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          selectedUserGrades.value.length < 1 &&
          promotionConditionType.value == "USER_GRADE"
        ) {
          callback(new Error("请选择用户等级！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  state.goodsUserGradesParameter.grades = selectedUserGrades.value.join(",");
  conditionTypeChange();
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开弹窗
const openDialog = (promotionId: string, row: RowPromotionConditionType) => {
  nextTick(() => {
    state.dialog.editId = "";
    resetForms();
    state.ruleForm.promotionId = promotionId;
    if (row) {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      promotionConditionType.value = state.ruleForm.code;
      conditionTypeRevert();
      state.dialog.title = "修改促销条件";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增促销条件";
      state.dialog.submitTxt = "新 增";
    }
    state.dialog.isShowDialog = true;
  });
};

// 表单重置
const resetForms = () => {
  promotionConditionType.value = "";
  selectedGoodItems.value = [] as RowGoodsType[];
  Object.assign(state.ruleForm, {
    id: getGuidEmpty(),
    promotionId: "",
    code: "",
    parameters: "",
    concurrencyStamp: "",
  });
  Object.assign(state.goodsIdsParameter, {
    goods: "",
    nums: 1,
  });
  Object.assign(state.goodsCatsParameter, {
    catId: "",
    nums: 1,
  });
  Object.assign(state.goodsBrandsParameter, {
    brandId: "",
    nums: 1,
  });
  Object.assign(state.goodsOrderFullsParameter, {
    money: "",
  });
  Object.assign(state.goodsUserGradesParameter, {
    grades: "",
  });
  selectedUserGrades.value = [] as string[];
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 提交
const onSubmit = () => {
  let conditionForm = _.cloneDeep(state.ruleForm);
  emit("condition-change", conditionForm);
  closeDialog();
};

// 获取商品分类
const getGoodCategorys = () => {
  goodCategoryApi
    .getGoodCategoryTree()
    .then((result) => {
      goodCategorData.value = result;
    })
    .catch((err) => {
      console.error("查询商品分类列表出错：", err);
    });
};

// 获取品牌
const getGoodBrands = () => {
  brandApi
    .getGoodBrandShow()
    .then((result) => {
      goodBrandData.value = result;
    })
    .catch((err: any) => {
      console.error("查询品牌出错：", err);
    });
};

// 获取用户等级
const getUserGrades = () => {
  userGradeApi
    .getUserGradeAll()
    .then((result) => {
      userGradeData.value = result;
    })
    .catch((err: any) => {
      console.error("查询用户等级出错：", err);
    });
};

// 页面加载完时
onMounted(() => {
  getGoodCategorys();
  getGoodBrands();
  getUserGrades();
});

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style lang="scss" scoped>
.condition-form-body {
  height: 350px;
  overflow-y: auto;
  overflow-x: hidden;
}

.condition-desc {
  color: #999;
}

.selected-good-item {
  width: 100%;
}
</style>
