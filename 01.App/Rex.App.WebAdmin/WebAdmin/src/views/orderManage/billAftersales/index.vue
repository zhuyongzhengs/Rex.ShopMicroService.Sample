<template>
  <div class="billAftersales-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="billAftersales-search">
        <el-row :gutter="15" class="mt10">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入售后单号"
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
            <el-select
              v-model="state.tableData.param.type"
              size="default"
              placeholder="选择收货类型"
            >
              <el-option
                v-for="(rType, index) in receiveTypes"
                :key="index"
                :label="rType.description"
                :value="rType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.status"
              size="default"
              placeholder="选择审核状态"
            >
              <el-option
                v-for="(bStatus, index) in billAftersalesStatus"
                :key="index"
                :label="bStatus.description"
                :value="bStatus.value"
              />
            </el-select>
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
            <el-button size="default" type="primary" @click="searchBillAftersales">
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
        ref="billAftersalesTableRef"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="index" width="65" label="序号" />
        <el-table-column
          fixed
          prop="no"
          label="售后单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column label="订单号" show-overflow-tooltip width="160">
          <template #default="scope">
            {{ scope.row.orderNo }}
          </template>
        </el-table-column>
        <el-table-column prop="typeDisplay" label="收货类型" width="100" />
        <el-table-column prop="statusDisplay" label="审核状态" width="100">
          <template #default="scope">
            <el-text size="default" :type="colorStatusType(scope.row.status)">{{
              scope.row.statusDisplay
            }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="userName" label="用户" width="180" show-overflow-tooltip />
        <el-table-column prop="refundAmount" label="退款金额" width="120">
          <template #default="scope">
            <el-text type="danger">￥{{ scope.row.refundAmount }}</el-text>
          </template>
        </el-table-column>
        <el-table-column
          prop="reason"
          label="退款原因"
          show-overflow-tooltip
          min-width="165"
        ></el-table-column>
        <el-table-column
          prop="creationTime"
          label="申请时间"
          width="165"
        ></el-table-column>
        <el-table-column fixed="right" label="操作" width="125">
          <template #default="scope">
            <el-button size="small" text type="info" @click="onDetails(scope.row)"
              >查看</el-button
            >
            <el-button
              v-if="scope.row.status === 1"
              v-auth="'OrderService.BillAftersales.Operate'"
              size="small"
              text
              type="warning"
              @click="onAuditBillAftersales(scope.row)"
              >编辑&审核</el-button
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
    <bill-aftersales-audit-dialog
      ref="billAftersalesAuditDialogRef"
      @success="getTableData"
    />
    <bill-aftersales-detail-dialog ref="billAftersalesDetailDialogRef" />
  </div>
</template>

<script setup lang="ts" name="billAftersales">
import { reactive, onBeforeMount, onMounted, ref, defineAsyncComponent } from "vue";
import _ from "lodash";
import { useBillAftersalesApi } from "/@/api/orderManage/billAftersales/index";

// 引入组件
const BillAftersalesAuditDialog = defineAsyncComponent(
  () =>
    import("/@/views/orderManage/billAftersales/components/billAftersalesAuditDialog.vue")
);
const BillAftersalesDetailDialog = defineAsyncComponent(
  () =>
    import(
      "/@/views/orderManage/billAftersales/components/billAftersalesDetailDialog.vue"
    )
);

// 引入 Api 请求接口
const billAftersalesApi = useBillAftersalesApi();

// 定义变量内容
const billAftersalesAuditDialogRef = ref();
const billAftersalesDetailDialogRef = ref();
const receiveTypes = ref<EnumValueType[]>([]);
const billAftersalesStatus = ref<EnumValueType[]>([]);
const state = reactive<BillAftersalesState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      orderNo: null,
      type: null,
      status: null,
      creationTime: [],
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 编辑
const onAuditBillAftersales = (row: RowBillAftersalesType) => {
  billAftersalesAuditDialogRef.value.openDialog(row);
};

// 查看
const onDetails = (row: RowBillAftersalesType) => {
  billAftersalesDetailDialogRef.value.openDialog(row);
};

// 状态颜色
const colorStatusType = (status: number) => {
  switch (status) {
    case 1:
      return "warning";
    case 2:
      return "success";
    default:
      return "info";
  }
};

// 查询售后单收货类型
const getReceiveTypes = () => {
  billAftersalesApi
    .getReceiveTypes()
    .then((result) => {
      receiveTypes.value = result;
    })
    .catch((err: any) => {
      console.error("查询售后单收货类型出错：", err);
    });
};

// 查询售后单状态
const getBillAftersalesStatus = () => {
  billAftersalesApi
    .getBillAftersalesStatus()
    .then((result) => {
      billAftersalesStatus.value = result;
    })
    .catch((err: any) => {
      console.error("查询售后单状态出错：", err);
    });
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

  billAftersalesApi
    .getBillAftersalesList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询售后单方式列表出错：", err);
    });
};

// 搜索
const searchBillAftersales = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.orderNo = null;
  state.tableData.param.type = null;
  state.tableData.param.status = null;
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
  getReceiveTypes();
  getBillAftersalesStatus();
  getTableData();
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>

<style scoped lang="scss">
.billAftersales-container {
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
