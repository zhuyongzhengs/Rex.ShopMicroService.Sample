<template>
  <div class="refund-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      center
      width="1000px"
    >
      <div class="refund-detail-box">
        <div class="refund-detail-item">
          <table cellpadding="0" cellspacing="0" class="refund-detail-table">
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
              <td>{{ state.data?.paymentCode }}</td>
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
              <th style="vertical-align: top">回执说明</th>
              <td colspan="5">
                <pre> {{ state.data?.memo }} </pre>
              </td>
            </tr>
          </table>
        </div>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="billRefundDetailDialog">
import { nextTick, reactive, defineExpose } from "vue";
import _ from "lodash";

// 定义变量
const state = reactive({
  loading: true,
  data: {} as RowBillRefundManyType,
  dialog: {
    isShowDialog: false,
    title: "退款单详情",
    submitTxt: "取消",
  },
});

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

// 打开弹窗
const openDialog = (row: RowBillRefundManyType) => {
  state.loading = true;
  state.data = row;
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

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.refund-detail-container {
  .refund-detail-box .el-card__header {
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
.refund-detail-container {
  .refund-detail-box {
    position: relative;
    top: -10px;
    .refund-detail-item {
      .refund-detail-table {
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
