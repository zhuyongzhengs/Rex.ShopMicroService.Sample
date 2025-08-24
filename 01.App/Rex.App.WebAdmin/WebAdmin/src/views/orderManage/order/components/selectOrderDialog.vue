<template>
  <div class="select-order-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="1000px"
    >
      <el-card shadow="never" class="mb15">
        <div class="select-order-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.no"
                size="default"
                placeholder="请输入订单号"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="5">
              <el-input
                v-model="state.tableData.param.shipName"
                size="default"
                placeholder="请输入收货人姓名"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="5">
              <el-input
                v-model="state.tableData.param.shipMobile"
                size="default"
                placeholder="请输入收货人电话"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="8">
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
      <el-card shadow="never" class="layout-padding-auto">
        <el-table
          ref="multipleOrderTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
          maxHeight="450px"
        >
          <el-table-column
            v-if="props.selectionType == 'selection'"
            fixed
            type="selection"
            width="55"
          />
          <el-table-column prop="sort" label="排序" width="60" />
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
          <el-table-column prop="paymentCode" label="支付方式" width="100">
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
      <template #footer>
        <span class="dialog-footer">
          <el-button size="default" @click="onCancel">取 消</el-button>
          <el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="selectOrder">
import { reactive, onBeforeMount, onMounted, ref, nextTick, defineProps } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useOrderApi } from "/@/api/orderManage/order/index";
import { usePaymentApi } from "/@/api/financialManage/payment/index";

// 引入Api请求接口
const orderApi = useOrderApi();
const paymentApi = usePaymentApi();

const emit = defineEmits(["selOrderResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 定义变量内容
const multipleOrderTableRef = ref();
const orderData = ref<RowOrderType[]>();
const orderTypes = ref<EnumValueType[]>([]);
const payStatusTypes = ref<EnumValueType[]>([]);
const shipStatus = ref<EnumValueType[]>([]);
const receiptTypes = ref<EnumValueType[]>([]);
const confirmStatus = ref<EnumValueType[]>([]);
const statusTypes = ref<EnumValueType[]>([]);
const sourceTypes = ref<EnumValueType[]>([]);
const paymentWays = ref<RowPaymentType[]>([]);
const currentOrder = ref({} as RowOrderType);
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

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  orderApi
    .getOrderList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询订单列表出错：", err);
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

// 选择(单选)当前
const onSelectedCurrChange = (good: RowOrderType) => {
  currentOrder.value = good;
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择普通订单",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询普通订单
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleOrderTableRef.value.getSelectionRows() as RowOrderType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择订单！");
    return;
  }
  if (!currentOrder.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择订单！");
    return;
  }
  emit("selOrderResult", props.selectionType == "single" ? currentOrder.value : selRows);
  closeDialog();
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载前
onBeforeMount(() => {
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
  getTableData();
});
</script>

<style scoped lang="scss">
.select-order-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
}

.img-logo {
  width: 35px;
  height: 35px;
}

.img-logo .image-slot {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background: var(--el-fill-color-light);
  color: var(--el-text-color-secondary);
  font-size: 30px;
}
.img-logo .image-slot .el-icon {
  font-size: 30px;
}

.good-price {
  color: #f56c6c;
}

.mkt-price {
  color: #999999;
  text-decoration: line-through;
}
</style>
