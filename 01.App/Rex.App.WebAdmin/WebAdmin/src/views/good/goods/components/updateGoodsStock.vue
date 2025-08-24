<template>
  <div class="good-stock-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="450px"
    >
      <el-form
        ref="stockDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <!-- <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <b>当前选中&nbsp;{{ state.goodItem.length }}&nbsp;条数据</b>
          </el-col> -->
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
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="调整值" prop="stockValue" label-width="65">
              <el-input
                v-model="state.ruleForm.stockValue"
                placeholder="请输入调整值"
                clearable
                maxlength="15"
                @input="
                  state.ruleForm.stockValue = verifiyNumberInteger(
                    state.ruleForm.stockValue
                  )
                "
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <div class="sel-data">
            <label>当前选中&nbsp;{{ state.goodItem.length }}&nbsp;条数据</label>
          </div>
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(stockDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </div>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodStockDialog">
import { nextTick, onMounted, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodsApi } from "/@/api/good/goods/index";
import { verifiyNumberInteger } from "/@/utils/toolsValidate";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const goodsApi = useGoodsApi();

// 定义变量内容
const stockDialogFormRef = ref();
const state = reactive({
  goodItem: [] as RowGoodsType[],
  ruleForm: {
    goodIds: new Array(),
    modifyType: "+", // 调整类型
    stockValue: "", // 调整值
  },
  dialog: {
    isShowDialog: false,
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  modifyType: [{ required: true, message: "请选择调整类型.", trigger: "blur" }],
  stockValue: [
    { required: true, message: "请输入调整值.", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!value || Number(value) < 1) {
          callback(new Error("不能为空，且不能小于0！"));
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
    state.dialog.title = "调整库存";
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
    .updateStock(state.ruleForm)
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

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>

<style lang="scss" scoped>
.dialog-footer {
  .sel-data {
    float: left;
    height: 32px;
    line-height: 32px;
  }
}
</style>
