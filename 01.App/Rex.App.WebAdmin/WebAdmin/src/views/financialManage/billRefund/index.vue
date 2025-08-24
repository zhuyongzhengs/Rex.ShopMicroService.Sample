<template>
  <div class="bill-refund-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="bill-refund-search">
        <el-row :gutter="15">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入退款单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.aftersalesNo"
              size="default"
              placeholder="请输入售后单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.userName"
              size="default"
              placeholder="请输入用户昵称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.type"
              size="default"
              placeholder="选择单据类型"
            >
              <el-option
                v-for="(bpType, index) in billRefundTypes"
                :key="index"
                :label="bpType.description"
                :value="bpType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.status"
              size="default"
              placeholder="选择支付状态"
            >
              <el-option
                v-for="(bpStatus, index) in billRefundStatus"
                :key="index"
                :label="bpStatus.description"
                :value="bpStatus.value"
              />
            </el-select>
          </el-col>
          <el-col :span="9">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchBillRefund"
            >
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
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="index" label="序号" width="60" />
        <el-table-column
          fixed
          prop="no"
          label="退款单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column
          fixed
          prop="billAftersalesNo"
          label="售后单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column prop="money" label="退款金额" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-text type="danger">￥{{ scope.row.money }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="userName" label="用户" show-overflow-tooltip width="210" />
        <el-table-column
          prop="sourceNo"
          label="关联单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column prop="typeDisplay" label="单据类型" width="100" />
        <el-table-column prop="statusDisplay" label="支付状态" width="100">
          <template #default="scope">
            <el-text size="default" :type="colorStatusType(scope.row.status)">{{
              scope.row.statusDisplay
            }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="PaymentCode" label="退款方式" width="120" />
        <el-table-column prop="memo" label="回执说明" show-overflow-tooltip width="180" />
        <el-table-column prop="creationTime" label="申请时间" width="165" />
        <el-table-column fixed="right" label="操作" width="120">
          <template #default="scope">
            <el-button size="small" text type="info" @click="onDetails(scope.row)"
              >查看</el-button
            >
            <el-button
              v-if="scope.row.status === 1"
              v-auth="'PaymentService.BillRefunds.AuditRefund'"
              size="small"
              text
              type="warning"
              @click="onAudit(scope.row)"
              >退款审核</el-button
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

    <bill-refund-detail-dialog ref="billRefundDetailDialogRef" />
    <bill-refund-audit-dialog ref="billRefundAuditDialogRef" @success="getTableData" />
  </div>
</template>

<script setup lang="ts" name="billRefund">
import { ref, reactive, onBeforeMount, onMounted, defineAsyncComponent } from "vue";
import _ from "lodash";
import { useBillRefundApi } from "/@/api/financialManage/billRefund/index";

// 引入组件
const BillRefundDetailDialog = defineAsyncComponent(
  () => import("/@/views/financialManage/billRefund/components/detailDialog.vue")
);
const BillRefundAuditDialog = defineAsyncComponent(
  () => import("/@/views/financialManage/billRefund/components/auditDialog.vue")
);

// 引入支付方式 Api 请求接口
const billRefundApi = useBillRefundApi();

// 定义变量内容
const billRefundDetailDialogRef = ref();
const billRefundAuditDialogRef = ref();
const billRefundStatus = ref<EnumValueType[]>([]);
const billRefundTypes = ref<EnumValueType[]>([]);
const state = reactive<BillRefundState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      aftersalesId: null,
      aftersalesNo: null,
      userId: null,
      userName: null,
      type: null,
      status: null,
      paymentCode: null,
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 查询查询单据类型
const getBillRefundTypes = () => {
  billRefundApi
    .getBillRefundType()
    .then((result) => {
      billRefundTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询单据类型出错：", err);
    });
};

// 查询单据状态
const getBillRefundStatus = () => {
  billRefundApi
    .getBillRefundStatus()
    .then((result) => {
      billRefundStatus.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询单据状态出错：", err);
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

// 审核
const onAudit = (row: RowBillRefundManyType) => {
  billRefundAuditDialogRef.value.openDialog(row);
};

// 查看详情
const onDetails = (row: RowBillRefundManyType) => {
  billRefundDetailDialogRef.value.openDialog(row);
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

  billRefundApi
    .getBillRefundList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询支付方式列表出错：", err);
    });
};

// 搜索
const searchBillRefund = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.aftersalesNo = null;
  state.tableData.param.userName = null;
  state.tableData.param.type = null;
  state.tableData.param.status = null;
  state.tableData.param.paymentCode = null;
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

// 组件挂载到节点上之前
onBeforeMount(() => {
  getBillRefundTypes();
  getBillRefundStatus();
});

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.bill-refund-container {
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
</style>
