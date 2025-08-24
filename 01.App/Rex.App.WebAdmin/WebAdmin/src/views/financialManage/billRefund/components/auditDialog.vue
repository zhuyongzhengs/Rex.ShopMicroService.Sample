<template>
  <div class="refund-audit-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      center
      width="1000px"
    >
      <div class="refund-audit-box">
        <div class="refund-audit-item">
          <table cellpadding="0" cellspacing="0" class="refund-audit-table">
            <tr>
              <th>用户名称</th>
              <td>{{ state.data?.userName }}</td>
              <th>退款单号</th>
              <td>{{ state.data?.no }}</td>
              <th>售后单号</th>
              <td>{{ state.data?.billAftersalesNo }}</td>
            </tr>
            <tr>
              <th>单据类型</th>
              <td>{{ state.data?.typeDisplay }}</td>
              <th>关联单号</th>
              <td>{{ state.data?.sourceNo }}</td>
              <th>申请时间</th>
              <td>{{ state.data?.creationTime }}</td>
            </tr>
            <tr>
              <th>支付方式</th>
              <td>
                {{ state.data.paymentCode }}
              </td>
              <th>支付状态</th>
              <td>
                <el-text size="default" :type="colorStatusType(state.data?.status)">{{
                  state.data?.statusDisplay
                }}</el-text>
              </td>
              <th>退款金额</th>
              <td>
                <el-text size="default" type="error">￥{{ state.data?.money }}</el-text>
              </td>
            </tr>
            <tr>
              <th>第三方平台流水号</th>
              <td colspan="5">{{ state.data?.tradeNo }}</td>
            </tr>
            <tr>
              <th>回执说明</th>
              <td colspan="5">{{ state.data?.memo }}</td>
            </tr>
          </table>
        </div>
        <el-card shadow="never" class="mt15">
          <template #header>
            <div class="reship-item-title">退款审核</div>
          </template>
          <el-form
            ref="auditDialogFormRef"
            :model="auditData"
            :rules="formRules"
            size="default"
            label-width="100px"
          >
            <el-row>
              <el-col :span="12">
                <el-form-item label="退款方式" prop="paymentCode">
                  <template v-if="paymentWays">
                    <el-select
                      v-model="auditData.paymentCode"
                      size="default"
                      placeholder="选择退款方式"
                    >
                      <el-option
                        v-for="(pType, index) in paymentWays"
                        :key="index"
                        :label="pType.name"
                        :value="pType.code"
                      />
                    </el-select>
                  </template>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="审核结果" prop="status">
                  <el-radio-group v-model="auditData.status">
                    <el-radio :value="2" label="通过" />
                    <el-radio :value="4" label="拒绝" />
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-card>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button
            type="primary"
            @click="submitValidate(auditDialogFormRef)"
            size="default"
            >确 认</el-button
          >
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="billRefundDetailDialog">
import { ref, nextTick, reactive, onBeforeMount, defineExpose } from "vue";
import { ElMessage, type FormInstance, type FormRules } from "element-plus";
import _ from "lodash";
import { usePaymentApi } from "/@/api/financialManage/payment/index";
import { useBillRefundApi } from "/@/api/financialManage/billRefund/index";

// 引入 Api 请求接口
const paymentApi = usePaymentApi();
const billRefundApi = useBillRefundApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["success"]);

// 定义变量
const auditDialogFormRef = ref<any>();
const paymentWays = ref<RowPaymentType[]>([]);
const auditData = reactive({
  status: 0,
  paymentCode: "",
});
const state = reactive({
  loading: true,
  data: {} as RowBillRefundManyType,
  dialog: {
    isShowDialog: false,
    title: "审核退款单",
    submitTxt: "取消",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  paymentCode: [{ required: true, message: "请选择退款方式！", trigger: "change" }],
  status: [
    { required: true, message: "请选择审核结果！", trigger: "change" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (value !== 2 && value !== 4) {
          callback(new Error("请选择审核结果！"));
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
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

// 提交
const onSubmit = () => {
  billRefundApi
    .updateAuditRefund(state.data.id, auditData)
    .then((result) => {
      if (!result) return;
      emit("success");
      ElMessage.success("操作成功！");
      onCancel();
    })
    .catch((err: any) => {
      console.error("操作出错：", err);
    });
};

// 状态颜色
const colorStatusType = (status: number) => {
  switch (status) {
    case 2:
      return "success";
    case 3:
      return "warning";
    case 4:
      return "error";
    default:
      return "info";
  }
};

// 查询支付方式
const getPaymentTypes = () => {
  paymentApi
    .getPaymentList({
      code: null,
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 100,
    })
    .then((result) => {
      paymentWays.value = result.items;
    })
    .catch((err: any) => {
      console.error("查询支付方式列表出错：", err);
    });
};

// 打开弹窗
const openDialog = (row: RowBillRefundManyType) => {
  state.loading = true;
  Object.assign(state.data, row)
  auditData.paymentCode = row.paymentCode;
  nextTick(() => {
    state.loading = false;
    // 查询退款单信息
    state.dialog.isShowDialog = true;
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

// 页面加载前
onBeforeMount(() => {
  getPaymentTypes();
});

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.refund-audit-container {
  .refund-audit-box .el-card__header {
    background-color: #f5f7fa !important;
    padding: 10px 15px !important;
  }
  .el-dialog__header {
    border-bottom: 1px #e4e7ed dashed !important;
    margin-right: 0px !important;
    padding-bottom: 14px !important;
  }
}
</style>

<style scoped lang="scss">
.refund-audit-container {
  .refund-audit-box {
    position: relative;
    top: -10px;
    .refund-audit-item {
      .refund-audit-table {
        border-collapse: collapse;
        width: 100%;
        tr {
          th {
            width: 130px;
            background-color: #fbfbfb;
            text-align: right;
            padding: 10px 5px;
            font-weight: 500;
            border: 1px solid #eeeeee;
          }
          td {
            min-width: 130px;
            text-align: left;
            padding: 10px 5px;
            color: #999999;
            border: 1px solid #eeeeee;
          }
        }
      }
    }
  }
}
</style>
