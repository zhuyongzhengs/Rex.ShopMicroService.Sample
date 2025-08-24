<template>
  <div class="logistics-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="logistics-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.logiCode"
              size="default"
              placeholder="请输入物流编码"
              maxlength="100"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.logiName"
              size="default"
              placeholder="请输入物流名称"
              maxlength="100"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchLogistics"
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
          <el-col :span="12"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'OrderService.Logisticss.Create'"
          type="success"
          @click="onOpenAddLogistics('add')"
        >
          <el-icon>
            <ele-Plus />
          </el-icon>
          新增
        </el-button>
        <el-button
          size="default"
          v-auth="'OrderService.Logisticss.Create'"
          type="default"
          @click="onRefreshLogistics"
        >
          <el-icon>
            <ele-Refresh />
          </el-icon>
          刷新接口
        </el-button>
      </template>
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column prop="sort" label="排序" show-overflow-tooltip width="65" />
        <el-table-column
          prop="logiCode"
          label="物流编码"
          show-overflow-tooltip
          width="180"
        />
        <el-table-column prop="logiName" label="物流名称" show-overflow-tooltip />
        <el-table-column prop="imgUrl" label="Logo" show-overflow-tooltip width="150">
          <template #default="scope">
            <el-image
              class="img-logo"
              fit="scale-down"
              :src="scope.row.imgUrl"
              :zoom-rate="1.2"
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="[scope.row.imgUrl]"
              :preview-teleported="true"
            >
              <template #error>
                <div class="image-slot">
                  <el-icon><icon-picture /></el-icon>
                </div>
              </template>
            </el-image>
          </template>
        </el-table-column>
        <el-table-column
          prop="phone"
          label="物流电话"
          show-overflow-tooltip
          width="200"
        />
        <el-table-column prop="url" label="物流网址" show-overflow-tooltip width="240" />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'OrderService.Logisticss.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditLogistics('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'OrderService.Logisticss.Delete'"
              size="small"
              text
              type="danger"
              @click="onRowDel(scope.row)"
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
    <logistics-dialog ref="logisticsDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="logistics">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Picture as IconPicture } from "@element-plus/icons-vue";
import _ from "lodash";
import { useLogisticsApi } from "/@/api/shopSetting/logistics/index";

// 引入组件
const LogisticsDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/logistics/components/logisticsDialog.vue")
);

// 引入物流 Api 请求接口
const logisticsApi = useLogisticsApi();

// 定义变量内容
const logisticsDialogRef = ref();
const state = reactive<LogisticsState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      logiCode: null,
      logiName: null,
      phone: null,
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
  logisticsApi
    .getLogisticsList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询物流列表出错：", err);
    });
};

// 搜索
const searchLogistics = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.logiCode = null;
  state.tableData.param.logiName = null;
  state.tableData.param.phone = null;
};

// 打开新增物流弹窗
const onOpenAddLogistics = (type: string) => {
  logisticsDialogRef.value.openDialog(type);
};

// 打开修改物流弹窗
const onOpenEditLogistics = (type: string, row: RowLogisticsType) => {
  logisticsDialogRef.value.openDialog(type, row);
};

// 刷新接口
const onRefreshLogistics = () => {
  ElMessage.warning("可通过[易源平台]接入数据！");
};

// 删除物流
const onRowDel = (row: RowLogisticsType) => {
  ElMessageBox.confirm(`此操作将永久删除物流名称：“${row.logiName}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    logisticsApi
      .deleteLogistics(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
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
.logistics-container {
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
</style>
