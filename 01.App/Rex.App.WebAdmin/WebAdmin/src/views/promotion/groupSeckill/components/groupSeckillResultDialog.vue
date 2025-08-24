<template>
  <div class="groupSeckill-result-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="650px"
    >
      <el-form
        ref="groupSeckillResultDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
        class="result-form-body"
      >
        <el-row :gutter="10">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="促销结果" prop="resultType">
              <el-select
                v-model="groupSeckillResultType"
                size="default"
                style="width: 100%"
                placeholder="请选择促销结果"
                :disabled="state.dialog.submitTxt == '修 改'"
              >
                <el-option
                  v-for="(resultType, index) in props.resultTypes"
                  :key="index"
                  :value="resultType.key"
                  :label="resultType.description"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'GOODS_REDUCE'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">指定商品减固定金额</div>
            </el-form-item>
            <el-form-item label="金额" prop="goodsReduce">
              <el-input
                v-model="state.goodsReduceParameter.money"
                size="default"
                placeholder="请输入商品优惠金额"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsReduceParameter.money = verifyNumberIntegerAndFloat(
                    state.goodsReduceParameter.money
                  )
                "
              />
              <span class="result-desc">商品优惠的金额</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'GOODS_DISCOUNT'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">指定商品打X折</div>
            </el-form-item>
            <el-form-item label="折扣" prop="goodsDiscount">
              <el-input
                v-model="state.goodsDiscountParameter.discount"
                size="default"
                placeholder="请输入商品折扣"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsDiscountParameter.discount = verifyNumberIntegerAndFloat(
                    state.goodsDiscountParameter.discount
                  )
                "
              />
              <span class="result-desc">大于0小于10的数字</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'GOODS_ONE_PRICE'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">指定商品一口价</div>
            </el-form-item>
            <el-form-item label="金额" prop="goodsOnePrice">
              <el-input
                v-model="state.goodsOnePriceParameter.money"
                size="default"
                placeholder="请输入商品固定价格"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsOnePriceParameter.money = verifyNumberIntegerAndFloat(
                    state.goodsOnePriceParameter.money
                  )
                "
              />
              <span class="result-desc">商品的固定价格</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'ORDER_REDUCE'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">订单减指定金额</div>
            </el-form-item>
            <el-form-item label="金额" prop="goodsOrderReduce">
              <el-input
                v-model="state.goodsOrderReduceParameter.money"
                size="default"
                placeholder="请输入指定金额"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsOrderReduceParameter.money = verifyNumberIntegerAndFloat(
                    state.goodsOrderReduceParameter.money
                  )
                "
              />
              <span class="result-desc">订单总价减××钱</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'ORDER_DISCOUNT'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">订单打X折</div>
            </el-form-item>
            <el-form-item label="折扣" prop="goodsOrderDiscount">
              <el-input
                v-model="state.goodsOrderDiscountParameter.discount"
                size="default"
                placeholder="请输入订单折扣"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsOrderDiscountParameter.discount = verifyNumberIntegerAndFloat(
                    state.goodsOrderDiscountParameter.discount
                  )
                "
              />
              <span class="result-desc">大于0小于10的数字</span>
            </el-form-item>
          </el-col>
          <el-col
            v-show="groupSeckillResultType == 'GOODS_HALF_PRICE'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="结果名称">
              <div class="result-desc">指定商品每第几件减去指定金额</div>
            </el-form-item>
            <el-form-item label="第几件" prop="goodsHalfPriceNum">
              <el-input-number
                v-model="state.goodsHalfPriceParameter.num"
                :precision="0"
                :min="1"
                class="mr20"
              />
              <span class="result-desc">每第几件商品</span>
            </el-form-item>
            <el-form-item label="优惠金额" prop="goodsHalfPriceMoney">
              <el-input
                v-model="state.goodsHalfPriceParameter.money"
                size="default"
                placeholder="请输入优惠金额"
                style="width: 40%"
                class="mr20"
                clearable
                @input="
                  state.goodsHalfPriceParameter.money = verifyNumberIntegerAndFloat(
                    state.goodsHalfPriceParameter.money
                  )
                "
              />
              <span class="result-desc">减去的固定价格</span>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(groupSeckillResultDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="groupSeckillResultDialog">
import { nextTick, reactive, ref, defineProps } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import { getGuidEmpty } from "/@/utils/other";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["result-change"]);
const props = defineProps({
  // 商品秒杀结果类型
  resultTypes: {
    type: Object,
    default: () => [{ key: null, value: null, description: null }],
  },
});

// 引入用户 Api 请求接口

// 定义变量内容
const groupSeckillResultDialogFormRef = ref();
const groupSeckillResultType = ref("");
const state = reactive({
  ruleForm: {
    id: getGuidEmpty(),
    groupSeckillId: "",
    code: "",
    parameters: "",
    concurrencyStamp: "",
  },
  /**
   * 参数：指定商品减固定金额
   */
  goodsReduceParameter: {
    money: "",
  },
  /**
   * 参数：指定商品打X折
   */
  goodsDiscountParameter: {
    discount: "",
  },
  /**
   * 参数：指定商品一口价
   */
  goodsOnePriceParameter: {
    money: "",
  },
  /**
   * 参数：订单减指定金额
   */
  goodsOrderReduceParameter: {
    money: "",
  },
  /**
   * 参数：订单打X折
   */
  goodsOrderDiscountParameter: {
    discount: "",
  },
  /**
   * 参数：指定商品每第几件减指定金额
   */
  goodsHalfPriceParameter: {
    num: 1,
    money: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

// 选择商品秒杀结果切换
const resultTypeChange = () => {
  state.ruleForm.code = groupSeckillResultType.value;
  let parameters = "";
  switch (state.ruleForm.code) {
    case "GOODS_REDUCE":
      parameters = JSON.stringify(state.goodsReduceParameter);
      break;
    case "GOODS_DISCOUNT":
      parameters = JSON.stringify(state.goodsDiscountParameter);
      break;
    case "GOODS_ONE_PRICE":
      parameters = JSON.stringify(state.goodsOnePriceParameter);
      break;
    case "ORDER_REDUCE":
      parameters = JSON.stringify(state.goodsOrderReduceParameter);
      break;
    case "ORDER_DISCOUNT":
      parameters = JSON.stringify(state.goodsOrderDiscountParameter);
      break;
    case "GOODS_HALF_PRICE":
      parameters = JSON.stringify(state.goodsHalfPriceParameter);
      break;
  }
  state.ruleForm.parameters = parameters;
};

// 选择商品秒杀结果还原
const resultTypeRevert = () => {
  let parameters = JSON.parse(state.ruleForm.parameters);
  switch (state.ruleForm.code) {
    case "GOODS_REDUCE":
      state.goodsReduceParameter = parameters;
      break;
    case "GOODS_DISCOUNT":
      state.goodsDiscountParameter = parameters;
      break;
    case "GOODS_ONE_PRICE":
      state.goodsOnePriceParameter = parameters;
      break;
    case "ORDER_REDUCE":
      state.goodsOrderReduceParameter = parameters;
      break;
    case "ORDER_DISCOUNT":
      state.goodsOrderDiscountParameter = parameters;
      break;
    case "GOODS_HALF_PRICE":
      state.goodsHalfPriceParameter = parameters;
      break;
  }
};

// 表单验证规则
const formRules = reactive<FormRules>({
  resultType: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!groupSeckillResultType.value) {
          callback(new Error("请选择商品秒杀结果！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsReduce: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsReduceParameter.money ||
            Number(state.goodsReduceParameter.money) < 1) &&
          groupSeckillResultType.value == "GOODS_REDUCE"
        ) {
          callback(new Error("商品优惠金额不为空，并且大于0！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsDiscount: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsDiscountParameter.discount ||
            Number(state.goodsDiscountParameter.discount) < 1 ||
            Number(state.goodsDiscountParameter.discount) > 9) &&
          groupSeckillResultType.value == "GOODS_DISCOUNT"
        ) {
          callback(new Error("折扣不能为空，并且不能小于1，不能大于10的数字！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsOnePrice: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsOnePriceParameter.money ||
            Number(state.goodsOnePriceParameter.money) < 1) &&
          groupSeckillResultType.value == "GOODS_ONE_PRICE"
        ) {
          callback(new Error("请输入商品固定价格！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsOrderReduce: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsOrderReduceParameter.money ||
            Number(state.goodsOrderReduceParameter.money) < 1) &&
          groupSeckillResultType.value == "ORDER_REDUCE"
        ) {
          callback(new Error("请输入指定金额！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsOrderDiscount: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsOrderDiscountParameter.discount ||
            Number(state.goodsOrderDiscountParameter.discount) < 1 ||
            Number(state.goodsOrderDiscountParameter.discount) > 9) &&
          groupSeckillResultType.value == "ORDER_DISCOUNT"
        ) {
          callback(new Error("折扣不能为空，并且不能小于1，不能大于10的数字！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsHalfPriceNum: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsHalfPriceParameter.num ||
            Number(state.goodsHalfPriceParameter.num) < 1) &&
          groupSeckillResultType.value == "GOODS_HALF_PRICE"
        ) {
          callback(new Error("请输入第几件商品数量！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  goodsHalfPriceMoney: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (!state.goodsHalfPriceParameter.money ||
            Number(state.goodsHalfPriceParameter.money) < 1) &&
          groupSeckillResultType.value == "GOODS_HALF_PRICE"
        ) {
          callback(new Error("请输入优惠金额！"));
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
  resultTypeChange();
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开弹窗
const openDialog = (groupSeckillId: string, row: RowPromotionResultType) => {
  nextTick(() => {
    state.dialog.editId = "";
    resetForms();
    state.ruleForm.groupSeckillId = groupSeckillId;
    if (row) {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      groupSeckillResultType.value = state.ruleForm.code;
      resultTypeRevert();
      state.dialog.title = "修改促销结果";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增促销结果";
      state.dialog.submitTxt = "新 增";
    }
    state.dialog.isShowDialog = true;
  });
};

// 表单重置
const resetForms = () => {
  groupSeckillResultType.value = "";
  Object.assign(state.ruleForm, {
    id: getGuidEmpty(),
    groupSeckillId: "",
    code: "",
    parameters: "",
    concurrencyStamp: "",
  });
  Object.assign(state.goodsReduceParameter, {
    money: "",
  });
  Object.assign(state.goodsDiscountParameter, {
    discount: "",
  });
  Object.assign(state.goodsOnePriceParameter, {
    money: "",
  });
  Object.assign(state.goodsOrderReduceParameter, {
    money: "",
  });
  Object.assign(state.goodsOrderDiscountParameter, {
    money: "",
  });
  Object.assign(state.goodsHalfPriceParameter, {
    num: 1,
    money: "",
  });
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
  let resultForm = _.cloneDeep(state.ruleForm);
  emit("result-change", resultForm);
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style lang="scss" scoped>
.result-form-body {
  height: 350px;
  overflow-y: auto;
  overflow-x: hidden;
}

.result-desc {
  color: #999;
}
</style>
