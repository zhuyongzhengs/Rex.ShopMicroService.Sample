<template>
  <div class="order-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="order-search">
        <el-row :gutter="15">
          <el-col :span="14">
            <el-radio-group
              v-model="globalOrderStatus.currentValue"
              size="default"
              @change="globalOrderStatusChange"
            >
              <el-radio-button
                v-for="(orderStatus, index) in globalOrderStatus.options"
                :key="index"
                :label="orderStatus.label"
              >
                {{ orderStatus.label }}
                <el-tag :type="orderStatus.tagType" size="small">{{
                  orderStatus.qty
                }}</el-tag>
              </el-radio-button>
            </el-radio-group>
          </el-col>
        </el-row>
        <el-row :gutter="15" class="mt10">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入订单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.orderType"
              size="default"
              placeholder="选择订单类型"
            >
              <el-option
                v-for="(oType, index) in orderTypes"
                :key="index"
                :label="oType.description"
                :value="oType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.status"
              size="default"
              placeholder="选择订单状态"
            >
              <el-option
                v-for="(sType, index) in statusTypes"
                :key="index"
                :label="sType.description"
                :value="sType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.paymentCode"
              size="default"
              placeholder="选择支付方式"
            >
              <el-option
                v-for="(pType, index) in paymentWays"
                :key="index"
                :label="pType.name"
                :value="pType.code"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.payStatus"
              size="default"
              placeholder="选择付款状态"
            >
              <el-option
                v-for="(sType, index) in payStatusTypes"
                :key="index"
                :label="sType.description"
                :value="sType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.shipStatus"
              size="default"
              placeholder="选择发货状态"
            >
              <el-option
                v-for="(sType, index) in shipStatus"
                :key="index"
                :label="sType.description"
                :value="sType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.receiptType"
              size="default"
              placeholder="选择收货方式"
            >
              <el-option
                v-for="(sType, index) in receiptTypes"
                :key="index"
                :label="sType.description"
                :value="sType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.source"
              size="default"
              placeholder="选择订单来源"
            >
              <el-option
                v-for="(sType, index) in sourceTypes"
                :key="index"
                :label="sType.description"
                :value="sType.value"
              />
            </el-select>
          </el-col>
        </el-row>
        <el-row :gutter="15" class="mt10">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.shipName"
              size="default"
              placeholder="请输入收货人姓名"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.shipMobile"
              size="default"
              placeholder="请输入收货人电话"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="7">
            <el-date-picker
              v-model="state.tableData.param.creationTime"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" @click="searchOrder">
              <el-icon>
                <ele-Search />
              </el-icon>
              查询
            </el-button>
            <el-button size="default" class="ml10" @click="onReset">
              <el-icon>
                <ele-RefreshLeft />
              </el-icon>
              重置
            </el-button>
          </el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button-group>
          <el-button
            v-auth="'OrderService.Orders.Update'"
            text
            type="primary"
            size="default"
            class="group-btn"
            @click="onBatchPay"
          >
            批量支付
          </el-button>
          <!--
          <el-button
            v-auth="'OrderService.Orders.Update'"
            text
            type="warning"
            size="default"
            class="group-btn"
          >
            合并发货
          </el-button>
          -->
          <el-button
            v-auth="'OrderService.Orders.Cancel'"
            text
            type="warning"
            size="default"
            class="group-btn"
            @click="onBatchCancelOrder"
          >
            取消订单
          </el-button>
          <el-button
            v-auth="'OrderService.Orders.Delete'"
            text
            type="danger"
            size="default"
            class="group-btn"
            @click="onBatchDeleteOrder"
          >
            批量删除
          </el-button>
          <el-button text type="info" size="default" class="group-btn">
            导出订单
          </el-button>
        </el-button-group>
      </template>
      <el-table
        :data="state.tableData.data"
        ref="orderTableRef"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="selection" width="50" />
        <el-table-column
          fixed
          prop="no"
          label="订单号"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="orderAmount"
          label="订单总额"
          show-overflow-tooltip
          width="100"
        >
          <template #default="scope">
            <el-text class="mx-1" type="danger">￥{{ scope.row.orderAmount }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="orderType" label="订单类型" width="100">
          <template #default="scope">
            {{ formatOrderType(scope.row.orderType) }}
          </template>
        </el-table-column>
        <el-table-column prop="status" label="订单状态" width="100">
          <template #default="scope">
            {{ formatOrderStatus(scope.row.status) }}
          </template>
        </el-table-column>
        <el-table-column prop="paymentCode" label="支付方式" width="120">
          <template #default="scope">
            {{ formatPaymentType(scope.row.paymentCode) }}
          </template>
        </el-table-column>
        <el-table-column prop="payStatus" label="付款状态" width="100">
          <template #default="scope">
            {{ formatOrderPayStatus(scope.row.payStatus) }}
          </template>
        </el-table-column>
        <el-table-column prop="shipStatus" label="发货状态" width="100">
          <template #default="scope">
            {{ formatShipStatus(scope.row.shipStatus) }}
          </template>
        </el-table-column>
        <el-table-column prop="confirmStatus" label="收货状态" width="120">
          <template #default="scope">
            {{ formatConfirmStatus(scope.row.confirmStatus) }}
          </template>
        </el-table-column>
        <el-table-column prop="receiptType" label="收货方式" width="100">
          <template #default="scope">
            {{ formatOrderReceiptType(scope.row.receiptType) }}
          </template>
        </el-table-column>
        <el-table-column prop="source" label="订单来源" width="130">
          <template #default="scope">
            {{ formatOrderSource(scope.row.source) }}
          </template>
        </el-table-column>
        <el-table-column
          prop="creationTime"
          label="下单时间"
          width="170"
        ></el-table-column>
        <el-table-column
          prop="paymentTime"
          label="支付时间"
          width="170"
        ></el-table-column>
        <el-table-column prop="shipAddress" label="地址" min-width="300">
          <template #default="scope">
            {{ scope.row.displayArea + " " + scope.row.shipAddress }}
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="210">
          <template #default="scope">
            <el-button size="small" text type="info" @click="onOrderDetail(scope.row)"
              >查看</el-button
            >
            <el-button
              v-auth="'OrderService.Orders.Pay'"
              v-if="showPayBtn(scope.row)"
              @click="onOrderPay([scope.row])"
              size="small"
              text
              type="primary"
              >支付</el-button
            >

            <el-button
              v-auth="'OrderService.Orders.Delivery'"
              v-if="showOrderShipBtn(scope.row)"
              @click="onOrderShip(scope.row)"
              size="small"
              text
              type="primary"
              >发货</el-button
            >

            <el-button
              v-auth="'OrderService.Orders.Complete'"
              v-if="showOrderComplete(scope.row)"
              @click="onOrderComplete(scope.row)"
              size="small"
              text
              type="success"
              >完成</el-button
            >

            <el-button
              v-auth="'OrderService.Orders.Update'"
              v-if="showShipAddressBtn(scope.row)"
              @click="onEditShip(scope.row)"
              size="small"
              text
              type="warning"
              >编辑</el-button
            >
            <el-button
              v-auth="'OrderService.Orders.Cancel'"
              v-if="showCancelOrderBtn(scope.row)"
              @click="onCancelOrder([scope.row])"
              size="small"
              text
              type="warning"
              >取消</el-button
            >
            <el-dropdown v-auth="'OrderService.Orders.Print'">
              <span class="el-dropdown-print">打印</span>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item>购物单</el-dropdown-item>
                  <el-dropdown-item>配送单</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
            <el-button
              v-auth="'OrderService.Orders.Delete'"
              v-if="showDeleteBtn(scope.row)"
              size="small"
              text
              type="danger"
              @click="onOrderDelete([scope.row])"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        @size-change="onHandleSizeChange"
        @current-change="onHandleCurrentChange"
        class="mt15"
        v-model:current-page="state.tableData.currentPage"
        background
        v-model:page-size="state.tableData.param.maxResultCount"
        layout="total, sizes, prev, pager, next, jumper"
        :total="state.tableData.total"
      >
      </el-pagination>
    </el-card>
    <order-detail-dialog ref="orderDetailDialogRef" />
    <order-pay-dialog ref="orderPayDialogRef" @success="getTableData" />
    <order-ship-dialog ref="ordeShipDialogRef" @success="getTableData" />
    <ship-delivery-dialog ref="shipDeliveryDialogRef" @success="getTableData" />
  </div>
</template>

<script setup lang="ts" name="order">
import { reactive, onBeforeMount, onMounted, ref, defineAsyncComponent } from "vue";
import { useRoute } from "vue-router";
import _ from "lodash";
import { useOrderApi } from "/@/api/orderManage/order/index";
import { usePaymentApi } from "/@/api/financialManage/payment/index";
import { ElMessage, ElMessageBox } from "element-plus";

// 引入组件
const OrderDetailDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/order/components/detailDialog.vue")
);
const OrderPayDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/order/components/payDialog.vue")
);
const OrderShipDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/order/components/shipDialog.vue")
);
const ShipDeliveryDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/order/components/deliveryDialog.vue")
);

// 引入 Api 请求接口
const orderApi = useOrderApi();
const paymentApi = usePaymentApi();

// 定义变量内容
const route = useRoute();
const orderDetailDialogRef = ref();
const orderPayDialogRef = ref();
const ordeShipDialogRef = ref();
const orderTableRef = ref();
const shipDeliveryDialogRef = ref();
const orderTypes = ref<EnumValueType[]>([]);
const payStatusTypes = ref<EnumValueType[]>([]);
const shipStatus = ref<EnumValueType[]>([]);
const receiptTypes = ref<EnumValueType[]>([]);
const confirmStatus = ref<EnumValueType[]>([]);
const statusTypes = ref<EnumValueType[]>([]);
const sourceTypes = ref<EnumValueType[]>([]);
const paymentWays = ref<RowPaymentType[]>([]);
const globalOrderStatus = reactive({
  currentValue: "全部订单",
  options: [
    {
      label: "全部订单",
      value: null,
      tagType: "info",
      qty: 0,
    },
    {
      label: "待支付",
      value: 1,
      tagType: "danger",
      qty: 0,
    },
    {
      label: "待发货",
      value: 2,
      tagType: "danger",
      qty: 0,
    },
    {
      label: "待收货",
      value: 3,
      tagType: "primary",
      qty: 0,
    },
    {
      label: "待评价",
      value: 4,
      tagType: "warning",
      qty: 0,
    },
    {
      label: "已取消",
      value: 7,
      tagType: "warning",
      qty: 0,
    },
    {
      label: "已完成",
      value: 6,
      tagType: "success",
      qty: 0,
    },
  ],
});
const state = reactive<OrderState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      shipName: null,
      shipMobile: null,
      paymentCode: null,
      payStatus: null,
      shipStatus: null,
      status: null,
      orderType: null,
      receiptType: null,
      confirmStatus: null,
      source: null,
      isComment: null,
      creationTime: [],
      globalOrderStatus: null,
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 显示订单[完成]
const showOrderComplete = (row: RowOrderType) => {
  return row.status == 1 && row.payStatus == 2 && row.shipStatus == 3;
};

// 显示[发货按钮]
const showOrderShipBtn = (row: RowOrderType) => {
  return row.status == 1 && row.payStatus == 2 && row.shipStatus == 1;
};

// 显示[支付按钮]
const showPayBtn = (row: RowOrderType) => {
  return row.status == 1 && row.payStatus == 1;
};

// 显示[编辑收货地址按钮]
const showShipAddressBtn = (row: RowOrderType) => {
  return (row.status == 1 && row.payStatus == 1) || showOrderShipBtn(row);
};

// 显示[取消订单按钮]
const showCancelOrderBtn = (row: RowOrderType) => {
  return row.status == 1 && row.payStatus == 1;
};

// 显示[删除按钮]
const showDeleteBtn = (row: RowOrderType) => {
  return row.status == 3;
};

// 订单完成
const onOrderComplete = (row: RowOrderType) => {
  validateAfterSale(row, () => {
    ElMessageBox.confirm(
      `确定要设置该订单[${row.no}]已完成吗？<br /><span style="color:red;">完成订单后将不能再对该订单进行任何操作。</span>`,
      "提示",
      {
        type: "warning",
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        dangerouslyUseHTMLString: true,
      }
    ).then(() => {
      orderApi
        .completeOrder(row.id)
        .then((result) => {
          if (!result) return;
          ElMessage.success("订单已完成！");
          getTableData();
        })
        .catch((err: any) => {
          console.error("订单完成失败：", err);
          ElMessage.error("完成订单操作失败：" + err.message);
        });
    });
  });
};

// 验证是否存在未处理的售后单
const validateAfterSale = (row: RowOrderType, callBack: Function) => {
  orderApi
    .getBillAftersalesByStatusAsync(row.id, 1)
    .then((result) => {
      if (result && result.orderId) {
        ElMessage.warning(`该订单[${row.no}]存在未处理的售后，请先处理！`);
        return;
      }
      callBack();
    })
    .catch((err: any) => {
      console.error("验证失败：", err);
      ElMessage.error("验证是否存在未处理的售后单出错：" + err.message);
    });
};

// 发货
const onOrderShip = (rows: RowOrderType) => {
  shipDeliveryDialogRef.value.openDialog(rows);
};

// 查看订单详情
const onOrderDetail = (row: RowOrderType) => {
  const paymentName = formatPaymentType(row.paymentCode);
  orderDetailDialogRef.value.openDialog(row, paymentName);
};

// 编辑收货地址
const onEditShip = (row: RowOrderType) => {
  ordeShipDialogRef.value.openDialog(row);
};

// 删除订单
const onOrderDelete = (rows: RowOrderType[]) => {
  if (!rows || rows.length < 1) return;
  let cancelMsg =
    rows.length == 1
      ? `您确认要删除该订单[${rows[0].no}]吗？`
      : `您选择了${rows.length}条订单，确认要删除吗？`;
  const orderIds = rows.map((x) => x.id);
  ElMessageBox.confirm(cancelMsg, "提示", {
    type: "warning",
    confirmButtonText: "确定",
    cancelButtonText: "取消",
  }).then(() => {
    orderApi
      .deleteOrder(orderIds)
      .then((result) => {
        if (!result) return;
        ElMessage.success("订单删除成功！");
        getTableData();
      })
      .catch((err: any) => {
        console.error("订单删除失败：", err);
        ElMessage.error("订单删除失败：" + err.message);
      });
  });
};

// 取消订单
const onCancelOrder = (rows: RowOrderType[]) => {
  if (!rows || rows.length < 1) return;
  let cancelMsg =
    rows.length == 1
      ? `您确认要取消该订单[${rows[0].no}]吗？`
      : `您选择了${rows.length}条订单，确认要取消吗？`;
  const orderIds = rows.map((x) => x.id);
  ElMessageBox.confirm(cancelMsg, "提示", {
    type: "warning",
    confirmButtonText: "确定",
    cancelButtonText: "取消",
  }).then(() => {
    orderApi
      .cancelOrder(orderIds)
      .then((result) => {
        if (!result) return;
        ElMessage.success("订单取消成功！");
        getTableData();
      })
      .catch((err: any) => {
        console.error("订单取消失败：", err);
        ElMessage.error("订单取消失败：" + err.message);
      });
  });
};

// 支付订单
const onOrderPay = (rows: RowOrderType[]) => {
  orderPayDialogRef.value.openDialog(rows);
};

// 全局订单状态切换
const globalOrderStatusChange = (value: string) => {
  let oStatus = globalOrderStatus.options.find((x) => x.label == value);
  if (oStatus != null) {
    state.tableData.param.globalOrderStatus = oStatus.value;
  }
  searchOrder();
};

// 批量支付
const onBatchPay = () => {
  const selRows = getSelectionOrders();
  if (selRows.length < 1) return;
  for (let i = 0; i < selRows.length; i++) {
    const order = selRows[i];
    if (!showPayBtn(order)) {
      ElMessage.warning(`选择的订单[${order.no}]不属于待支付状态，无需支付！`);
      return false;
    }
  }
  onOrderPay(selRows);
  return true;
};

// 批量取消订单
const onBatchCancelOrder = () => {
  const selRows = getSelectionOrders();
  if (selRows.length < 1) return;
  for (let i = 0; i < selRows.length; i++) {
    const order = selRows[i];
    if (!showCancelOrderBtn(order)) {
      ElMessage.warning(`选择的订单[${order.no}]不属于[取消订单]状态！`);
      return false;
    }
  }
  onCancelOrder(selRows);
  return true;
};

// 批量删除订单
const onBatchDeleteOrder = () => {
  const selRows = getSelectionOrders();
  if (selRows.length < 1) return;
  for (let i = 0; i < selRows.length; i++) {
    const order = selRows[i];
    if (!showDeleteBtn(order)) {
      ElMessage.warning(`选择的订单[${order.no}]未取消，不允许删除！`);
      return false;
    }
  }
  onOrderDelete(selRows);
  return true;
};

// 获取选择的订单
const getSelectionOrders = () => {
  const selRows = orderTableRef.value.getSelectionRows() as RowOrderType[];
  if (!selRows || selRows.length < 1) {
    return [];
  }
  return selRows;
};

// 查询订单数量
const getStatusQuantity = () => {
  orderApi
    .getStatusQuantity()
    .then((result) => {
      if (!result) return;
      for (let i = 0; i < globalOrderStatus.options.length; i++) {
        const oStatus = globalOrderStatus.options[i];
        if (oStatus.value == null) {
          oStatus.qty = result.all;
        } else if (oStatus.value == 1) {
          oStatus.qty = result.pendingPayment;
        } else if (oStatus.value == 2) {
          oStatus.qty = result.pendingShipment;
        } else if (oStatus.value == 3) {
          oStatus.qty = result.pendingDelivery;
        } else if (oStatus.value == 4) {
          oStatus.qty = result.pendingEvaluate;
        } else if (oStatus.value == 7) {
          oStatus.qty = result.cancelled;
        } else if (oStatus.value == 6) {
          oStatus.qty = result.completed;
        }
      }
    })
    .catch((err: any) => {
      console.error("查询订单数量出错：", err);
    });
};

// 查询订单收货方式
const getOrderReceiptType = () => {
  orderApi
    .getOrderReceiptType()
    .then((result) => {
      receiptTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单收货方式出错：", err);
    });
};

// 订单收货方式
const formatOrderReceiptType = (type: number) => {
  for (let i = 0; i < receiptTypes.value.length; i++) {
    const oType = receiptTypes.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 查询订单支付状态
const getOrderPayStatus = () => {
  orderApi
    .getOrderPayStatus()
    .then((result) => {
      payStatusTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单支付状态出错：", err);
    });
};

// 订单支付状态
const formatOrderPayStatus = (type: number) => {
  for (let i = 0; i < payStatusTypes.value.length; i++) {
    const oType = payStatusTypes.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 查询订单来源
const getOrderSource = () => {
  orderApi
    .getOrderSource()
    .then((result) => {
      sourceTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单来源出错：", err);
    });
};

// 订单来源
const formatOrderSource = (type: number) => {
  for (let i = 0; i < sourceTypes.value.length; i++) {
    const oType = sourceTypes.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 查询订单状态
const getOrderStatus = () => {
  orderApi
    .getOrderStatus()
    .then((result) => {
      statusTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单状态出错：", err);
    });
};

// 订单状态
const formatOrderStatus = (type: number) => {
  for (let i = 0; i < statusTypes.value.length; i++) {
    const oType = statusTypes.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 查询售后状态
const getConfirmStatus = () => {
  orderApi
    .getOrderConfirmStatus()
    .then((result) => {
      confirmStatus.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询售后状态出错：", err);
    });
};

// 售后状态
const formatConfirmStatus = (type: number) => {
  for (let i = 0; i < confirmStatus.value.length; i++) {
    const sType = confirmStatus.value[i];
    if (sType.value == String(type)) {
      return sType.description;
    }
  }
  return "";
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
      state.tableData.loading = false;
      console.error("查询支付方式列表出错：", err);
    });
};

// 支付方式
const formatPaymentType = (code: string) => {
  for (let i = 0; i < paymentWays.value.length; i++) {
    const pType = paymentWays.value[i];
    if (pType.code == code) {
      return pType.name;
    }
  }
  return "";
};

// 查询发货状态
const getShipStatus = () => {
  orderApi
    .getOrderShipStatus()
    .then((result) => {
      shipStatus.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询发货状态出错：", err);
    });
};

// 发货状态
const formatShipStatus = (type: number) => {
  for (let i = 0; i < shipStatus.value.length; i++) {
    const oType = shipStatus.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 查询订单类型
const getOrderTypes = () => {
  orderApi
    .getOrderType()
    .then((result) => {
      orderTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单类型出错：", err);
    });
};

// 订单类型
const formatOrderType = (type: number) => {
  for (let i = 0; i < orderTypes.value.length; i++) {
    const oType = orderTypes.value[i];
    if (oType.value == String(type)) {
      return oType.description;
    }
  }
  return "";
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;

  let searchParam = _.cloneDeep(state.tableData.param);
  delete searchParam.creationTime;
  if (
    state.tableData.param.creationTime &&
    state.tableData.param.creationTime.length == 2
  ) {
    searchParam.beginTime = state.tableData.param.creationTime[0];
    searchParam.endTime = state.tableData.param.creationTime[1];
  }

  orderApi
    .getOrderList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
      getStatusQuantity();
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单方式列表出错：", err);
    });
};

// 搜索
const searchOrder = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.shipName = null;
  state.tableData.param.shipMobile = null;
  state.tableData.param.paymentCode = null;
  state.tableData.param.payStatus = null;
  state.tableData.param.shipStatus = null;
  state.tableData.param.status = null;
  state.tableData.param.orderType = null;
  state.tableData.param.receiptType = null;
  state.tableData.param.confirmStatus = null;
  state.tableData.param.source = null;
  state.tableData.param.isComment = null;
  state.tableData.param.creationTime = [];
};

// 分页改变
const onHandleSizeChange = (val: number) => {
  state.tableData.param.maxResultCount = val;
  getTableData();
};

// 分页改变
const onHandleCurrentChange = (val: number) => {
  state.tableData.currentPage = val;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  getTableData();
};

// 页面加载前
onBeforeMount(() => {
  getStatusQuantity();
  getPaymentTypes();
  getOrderTypes();
  getShipStatus();
  getConfirmStatus();
  getOrderStatus();
  getOrderSource();
  getOrderPayStatus();
  getOrderReceiptType();
});

// 页面加载完时
onMounted(() => {
  if (route.query.status) {
    let status = route.query.status as string;
    globalOrderStatus.currentValue = status;
    setTimeout(() => {
      globalOrderStatusChange(status);
    }, 500);
  } else {
    getTableData();
  }
});
</script>

<style scoped lang="scss">
.order-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
  .group-btn {
    border: 1px #ebeef5 solid;
    border-right: 1px #dcdfe6 solid !important;
  }
  .el-dropdown-print {
    height: 24px;
    line-height: 24px;
    margin: 0px 8px;
    font-size: 12px;
    color: #e6a23c;
    cursor: default;
  }
}
</style>
