<template>
  <div class="delivery-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      width="1000px"
    >
      <div class="delivery-detail-box">
        <el-tabs v-loading="state.loading">
          <div class="delivery-detail-tabs">
            <el-tab-pane label="发货信息">
              <div class="delivery-detail-item">
                <table cellpadding="0" cellspacing="0" class="delivery-detail-table">
                  <tr>
                    <th>发货单号</th>
                    <td>{{ state.data?.no }}</td>
                    <th>订单号</th>
                    <td>{{ state.data?.order?.no }}</td>
                  </tr>
                  <tr>
                    <th>快递公司</th>
                    <td>{{ state.data?.logiName }}</td>
                    <th>快递单号</th>
                    <td>{{ state.data?.logiNo }}</td>
                  </tr>
                  <tr>
                    <th>收货人</th>
                    <td>{{ state.data?.shipName }}</td>
                    <th>收货电话</th>
                    <td>{{ state.data?.shipMobile }}</td>
                  </tr>
                  <tr>
                    <th>收货详细地址</th>
                    <td colspan="3">
                      {{ state.data?.displayArea + " " + state.data?.shipAddress }}
                    </td>
                  </tr>
                  <tr>
                    <th>发货备注</th>
                    <td colspan="3">{{ state.data?.memo }}</td>
                  </tr>
                </table>
              </div>
            </el-tab-pane>
            <el-tab-pane label="发货明细">
              <el-table
                ref="billDeliveryItemTableRef"
                :data="state.data?.billDeliveryItems"
                v-loading="state.loading"
                bbillDelivery
                width="100%"
              >
                <el-table-column fixed type="index" width="65" label="序号" />
                <el-table-column
                  prop="name"
                  label="商品名称"
                  show-overflow-tooltip
                  min-width="400"
                />
                <el-table-column prop="sn" label="货品编码" width="170"></el-table-column>
                <el-table-column prop="bn" label="商品编码" width="170"></el-table-column>
                <el-table-column
                  prop="nums"
                  label="发货数量"
                  width="90"
                ></el-table-column>
              </el-table>
            </el-tab-pane>
            <el-tab-pane label="物流明细"> </el-tab-pane>
          </div>
        </el-tabs>
      </div>
      <!--
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
      -->
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="billDeliveryDetail">
import { nextTick, reactive, ref, defineExpose } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useBillDeliveryApi } from "/@/api/orderManage/billDelivery/index";

// 引入 Api 请求接口
const billDeliveryApi = useBillDeliveryApi();

// 定义变量
const state = reactive({
  loading: true,
  data: {} as RowBillDeliveryType,
  dialog: {
    isShowDialog: false,
    title: "发货单详情",
    submitTxt: "取消",
  },
});

// 打开弹窗
const openDialog = (row: RowBillDeliveryType) => {
  state.loading = true;
  getBillDeliveryDetail(row.id);
  nextTick(() => {
    // 查询发货单信息
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

// 查询发货单明细
const getBillDeliveryDetail = (billDeliveryId: string) => {
  billDeliveryApi
    .getBillDeliveryDetail(billDeliveryId, {
      includeDetails: true,
    })
    .then((result) => {
      state.data = result;
      state.loading = false;
    })
    .catch((err: any) => {
      console.error("查询发货单明细出错：", err);
      state.loading = false;
    });
};

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.delivery-detail-container {
  .delivery-detail-tabs .el-card__header {
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
.delivery-detail-container {
  .delivery-detail-box {
    position: relative;
    top: -10px;
    .delivery-detail-tabs {
      height: 500px;
      overflow-y: auto;
    }
    .delivery-detail-item {
      .delivery-detail-table {
        border-collapse: collapse;
        width: 100%;
        tr {
          th {
            width: 110px;
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

    .delivery-detail-mark {
      text-align: center;
      margin-top: 15px;
    }

    .good-price {
      color: #f56c6c;
    }
  }
}
</style>
