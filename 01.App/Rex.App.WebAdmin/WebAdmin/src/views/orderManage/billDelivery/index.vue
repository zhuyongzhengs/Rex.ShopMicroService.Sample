<template>
  <div class="billDelivery-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="billDelivery-search">
        <el-row :gutter="15" class="mt10">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入发货单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.orderNo"
              size="default"
              placeholder="请输入订单号"
              clearable
            >
            </el-input>
          </el-col>
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
          <el-col :span="5">
            <el-button size="default" type="primary" @click="searchBillDelivery">
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
      <el-table
        :data="state.tableData.data"
        ref="billDeliveryTableRef"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="index" width="65" label="序号" />
        <el-table-column
          fixed
          prop="no"
          label="发货单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column label="发货单号" show-overflow-tooltip width="160">
          <template #default="scope">
            {{ scope.row.order.no }}
          </template>
        </el-table-column>
        <el-table-column
          prop="logiName"
          label="物流公司"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="logiNo"
          label="物流单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column
          prop="shipAddress"
          label="收货详细地址"
          width="300"
          show-overflow-tooltip
        >
          <template #default="scope">
            {{ scope.row.displayArea + " " + scope.row.shipAddress }}
          </template>
        </el-table-column>
        <el-table-column
          prop="shipName"
          label="收货人姓名"
          show-overflow-tooltip
          width="110"
        />
        <el-table-column
          prop="shipMobile"
          label="收货人电话"
          show-overflow-tooltip
          width="120"
        />
        <el-table-column
          prop="creationTime"
          label="发货时间"
          width="170"
        ></el-table-column>
        <el-table-column prop="memo" label="备注" width="180"></el-table-column>
        <el-table-column fixed="right" label="操作" width="100">
          <template #default="scope">
            <el-button
              size="small"
              text
              type="info"
              @click="onBillDeliveryDetail(scope.row)"
              >查看</el-button
            >
            <el-button
              v-auth="'OrderService.BillDeliverys.Update'"
              size="small"
              text
              type="warning"
              @click="onEditLogistics(scope.row)"
              >编辑</el-button
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
    <bill-delivery-detail-dialog ref="billDeliveryDetailDialogRef" />
    <bill-delivery-logistics-dialog
      ref="billDeliveryLogisticsDialogRef"
      @success="getTableData"
    />
  </div>
</template>

<script setup lang="ts" name="billDelivery">
import { reactive, onBeforeMount, onMounted, ref, defineAsyncComponent } from "vue";
import _ from "lodash";
import { useBillDeliveryApi } from "/@/api/orderManage/billDelivery/index";

// 引入组件
const BillDeliveryDetailDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/billDelivery/components/detailDialog.vue")
);
const BillDeliveryLogisticsDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/billDelivery/components/logisticsDialog.vue")
);

// 引入 Api 请求接口
const billDeliveryApi = useBillDeliveryApi();

// 定义变量内容
const billDeliveryDetailDialogRef = ref();
const billDeliveryLogisticsDialogRef = ref();
const state = reactive<BillDeliveryState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      orderNo: null,
      logiNo: null,
      shipName: null,
      creationTime: [],
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 查看发货单详情
const onBillDeliveryDetail = (row: RowBillDeliveryType) => {
  billDeliveryDetailDialogRef.value.openDialog(row);
};

// 编辑物料信息
const onEditLogistics = (row: RowBillDeliveryType) => {
  billDeliveryLogisticsDialogRef.value.openDialog(row);
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

  billDeliveryApi
    .getBillDeliveryList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询发货单方式列表出错：", err);
    });
};

// 搜索
const searchBillDelivery = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.orderNo = null;
  state.tableData.param.logiNo = null;
  state.tableData.param.shipName = null;
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
  getTableData();
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>

<style scoped lang="scss">
.billDelivery-container {
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
