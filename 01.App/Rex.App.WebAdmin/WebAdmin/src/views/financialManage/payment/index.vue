<template>
  <div class="payment-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="payment-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.code"
              size="default"
              placeholder="请输入支付编码"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入支付名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" class="ml10" @click="searchPayment">
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
          <el-col :span="14"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column prop="sort" label="排序" width="65" />
        <el-table-column prop="code" label="支付编码" show-overflow-tooltip width="150" />
        <el-table-column prop="name" label="支付名称" show-overflow-tooltip width="180" />
        <el-table-column prop="isOnline" label="是否线上支付" width="150">
          <template #default="scope">
            <el-tag type="success" v-if="scope.row.isOnline">是</el-tag>
            <el-tag type="info" v-else>否</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="memo" label="描述" show-overflow-tooltip />
        <el-table-column prop="isEnable" label="是否启用" width="100">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isEnable"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsEnable($event, scope.row)"
            />
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
  </div>
</template>

<script setup lang="ts" name="payment">
import { reactive, onMounted, ref } from "vue";
import _ from "lodash";
import { usePaymentApi } from "/@/api/financialManage/payment/index";

// 引入支付方式 Api 请求接口
const paymentApi = usePaymentApi();

// 定义变量内容
const state = reactive<PaymentState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      code: null,
      name: null,
      isEnable: null,
      sorting: "Sort",
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
  paymentApi
    .getPaymentList(state.tableData.param)
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
const searchPayment = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 是否启用
const onIsEnable = (val: boolean, payment: RowPaymentType) => {
  paymentApi
    .updatePaymentIsEnable(payment.id, val)
    .then((result) => {
      console.log("修改[" + payment.name + "]是否启用，结果：", result);
    })
    .catch((err: any) => {
      payment.isEnable = !val;
      console.error("修改出错：", err);
    });
};

// 重置
const onReset = () => {
  state.tableData.param.code = null;
  state.tableData.param.name = null;
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

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.payment-container {
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
