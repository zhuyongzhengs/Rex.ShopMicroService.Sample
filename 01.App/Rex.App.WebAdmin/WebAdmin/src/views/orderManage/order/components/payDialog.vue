<template>
  <div class="payment-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      center
      draggable
      width="450px"
    >
      <div class="payment-detail-box">
        <table
          class="order-item-table"
          cellpadding="0"
          cellspacing="0"
          v-for="order in state.orders"
          :key="order.id"
        >
          <tr>
            <th>订单号</th>
            <td>{{ order.no }}</td>
          </tr>
          <tr>
            <th>订单总额</th>
            <td>
              <el-text type="danger">￥{{ order.orderAmount }}</el-text>
            </td>
          </tr>
        </table>
        <div class="order-payment-box">
          <el-row>
            <el-col :span="6"> <div class="payment-name">支付方式</div> </el-col>
            <el-col :span="18">
              <el-select
                v-model="state.data.paymentCode"
                placeholder="请选择支付方式"
                size="default"
              >
                <el-option
                  v-for="payment in state.paymentCodes"
                  :label="payment.name"
                  :value="payment.code"
                  :key="payment.id"
                  >{{ payment.name }}</el-option
                >
              </el-select>
            </el-col>
          </el-row>
        </div>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button type="primary" @click="onConfirm" size="default">确 认</el-button>
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="paymentDetail">
import { nextTick, reactive, defineExpose } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { usePaymentApi } from "/@/api/financialManage/payment/index";
import { useBillPaymentApi } from "/@/api/financialManage/billPayment/index";

// 引入 Api 请求接口
const paymentApi = usePaymentApi();
const billPaymentApi = useBillPaymentApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["success"]);

// 定义变量
const state = reactive({
  loading: true,
  orders: [] as RowOrderType[],
  paymentCodes: [] as RowPaymentType[],
  data: {
    orderIds: [] as string[],
    paymentCode: "",
    paymentType: 1, // 1：订单、2：充值、3：表单订单、4：表单付款码、5：服务订单
  },
  dialog: {
    isShowDialog: false,
    title: "订单支付",
    submitTxt: "取消",
  },
});

// 打开弹窗
const openDialog = (rows: RowOrderType[]) => {
  state.loading = true;
  state.orders = rows;
  state.data.orderIds = state.orders.map((item) => item.id);
  getPaymentData();
  nextTick(() => {
    // 查询支付信息
    state.dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 确认
const onConfirm = () => {
  billPaymentApi
    .orderPay(state.data)
    .then((result) => {
      if (result) {
        emit("success");
        ElMessage.success("支付成功");
        onCancel();
      }
    })
    .catch((err: any) => {
      console.error("支付出错：", err);
    });
};

// 取消
const onCancel = () => {
  closeDialog();
};

const getPaymentData = () => {
  paymentApi
    .getPaymentList({
      code: null,
      name: null,
      isEnable: true,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 100,
    })
    .then((result) => {
      if (result.items && result.items.length > 0) {
        state.paymentCodes = result.items;
        state.data.paymentCode = result.items[0].code;
      }
    })
    .catch((err: any) => {
      console.error("查询支付方式列表出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.payment-detail-container {
  .payment-detail-tabs .el-card__header {
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
.payment-detail-container {
  .payment-detail-box {
    .order-item-table {
      width: 100%;
      border-collapse: collapse;
      margin-bottom: 10px;
      tr th {
        border: 1px solid #e4e7ed;
        background-color: #f7f7f7;
        padding: 10px;
        width: 90px;
        text-align: right;
      }
      tr td {
        border: 1px solid #e4e7ed;
        padding: 10px;
      }
    }
    .order-payment-box {
      margin-top: 10px;
      .payment-name {
        line-height: 32px;
        height: 32px;
        text-align: right;
        padding-right: 10px;
      }
    }
  }
}
</style>
