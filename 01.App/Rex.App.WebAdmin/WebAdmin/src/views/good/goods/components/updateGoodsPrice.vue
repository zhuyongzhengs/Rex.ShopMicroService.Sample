<template>
  <div class="good-price-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="650px"
    >
      <el-form
        ref="priceDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <div class="warning-msg-content">
              <p>
                价格类型：是指具体要修改某一种类型的价格，包含会员价，调整类型：是在原有的基础上对价格进行修改，调整值是价格类型通过调整类型和调整值计算得到。
              </p>
              <p>
                例如：调整销售价统一设置为10，则价格类型选择销售价，调整类型选择=，调整值输入10即可。
                如想调整销售价上浮10%，则价格选择销售价，调整类型选择x，调整值输入1.1即可。
              </p>
              <br />
              注：会员等级价格调整时，是商品实际价格为商品价格减去会员价。
            </div>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="价格类型" prop="priceType">
              <el-select
                size="default"
                v-model="state.ruleForm.priceType"
                placeholder="请选择价格类型"
              >
                <el-option
                  v-for="(priceType, index) in state.priceTypeData"
                  :key="index"
                  :label="priceType.description"
                  :value="priceType.title"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="调整类型" prop="modifyType">
              <el-select
                size="default"
                v-model="state.ruleForm.modifyType"
                placeholder="请选择调整类型"
              >
                <el-option label="＋" value="+" />
                <el-option label="－" value="-" />
                <el-option label="＝" value="=" />
                <el-option label="×" value="*" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="调整值" prop="priceValue">
              <el-input
                v-model="state.ruleForm.priceValue"
                placeholder="请输入金额调整值"
                clearable
                maxlength="15"
                @input="
                  state.ruleForm.priceValue = verifyNumberIntegerAndFloat(
                    state.ruleForm.priceValue
                  )
                "
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <div class="sel-data">
            <label>当前选中&nbsp;{{ state.goodItem.length }}&nbsp;条数据</label>
          </div>
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(priceDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodPriceDialog">
import { nextTick, onMounted, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodsApi } from "/@/api/good/goods/index";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const goodsApi = useGoodsApi();

// 定义变量内容
const priceDialogFormRef = ref();
const state = reactive({
  goodItem: [] as RowGoodsType[],
  ruleForm: {
    goodIds: new Array(),
    priceType: "", // 价格类型
    modifyType: "+", // 调整类型
    priceValue: "", // 调整值
  },
  priceTypeData: [] as EnumValueType[],
  dialog: {
    isShowDialog: false,
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  priceType: [{ required: true, message: "请选择价格类型.", trigger: "blur" }],
  modifyType: [{ required: true, message: "请选择调整类型.", trigger: "blur" }],
  priceValue: [
    { required: true, message: "请输入金额调整值.", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!value || Number(value) < 1) {
          callback(new Error("金额不能为空，且不能小于0！"));
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
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开弹窗
const openDialog = (rows: RowGoodsType[]) => {
  nextTick(() => {
    state.goodItem = rows;
    state.dialog.title = "修改价格";
    state.dialog.submitTxt = "确 定";
    state.dialog.isShowDialog = true;
    state.ruleForm.goodIds = state.goodItem.map((p) => p.id);
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
  goodsApi
    .updatePrice(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("修改成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 获取商品价格类型
const getGoodPriceType = () => {
  goodsApi
    .getGoodPriceType()
    .then((result) => {
      if (result) {
        state.priceTypeData = result;
        // 默认选中
        state.ruleForm.priceType = state.priceTypeData[0].title;
      }
    })
    .catch((err: any) => {
      console.error("获取商品价格类型出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  // 获取品牌
  getGoodPriceType();
});
</script>

<style lang="scss" scoped>
.warning-msg-content {
  margin-top: 5px;
  border-radius: 5px;
  background-color: #ecf5ff;
  padding: 10px;

  p {
    text-indent: 2em;
  }
}

.dialog-footer {
  .sel-data {
    float: left;
    height: 32px;
    line-height: 32px;
  }
}
</style>
