<template>
  <div class="good-typeSpec-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="good-typeSpec-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入参数名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchGoodTypeSpec"
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
          <el-col :span="9"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.GoodTypeSpecs.Create'"
          type="success"
          @click="onOpenAddGoodTypeSpec('add')"
        >
          <el-icon>
            <ele-Plus />
          </el-icon>
          新增
        </el-button>
      </template>
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column
          prop="name"
          label="SKU模型名称"
          width="200"
          show-overflow-tooltip
        />
        <el-table-column prop="value" label="SKU模型名称值" show-overflow-tooltip>
          <template #default="scope">
            <el-tag
              class="mr10"
              size="default"
              v-for="(specVal, index) in scope.row.specValues"
              :key="index"
              >{{ specVal.value }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column prop="sort" label="排序" width="100" show-overflow-tooltip />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.GoodTypeSpecs.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditGoodTypeSpec('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.GoodTypeSpecs.Delete'"
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
    <good-type-spec-dialog ref="goodTypeSpecDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="goodTypeSpec">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodTypeSpecApi } from "/@/api/good/typeSpec/index";

// 引入组件
const GoodTypeSpecDialog = defineAsyncComponent(
  () => import("/@/views/good/typeSpec/components/typeSpecDialog.vue")
);

// 引入商品类型规格 Api 请求接口
const goodTypeSpecApi = useGoodTypeSpecApi();

// 定义变量内容
const goodTypeSpecDialogRef = ref();
const state = reactive<GoodTypeSpecState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      type: null,
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
  goodTypeSpecApi
    .getGoodTypeSpecList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商品类型规格列表出错：", err);
    });
};

// 搜索
const searchGoodTypeSpec = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.type = null;
};

// 打开新增商品类型规格弹窗
const onOpenAddGoodTypeSpec = (type: string) => {
  goodTypeSpecDialogRef.value.openDialog(type, null);
};

// 打开修改商品类型规格弹窗
const onOpenEditGoodTypeSpec = (type: string, row: RowGoodTypeSpecType) => {
  goodTypeSpecDialogRef.value.openDialog(type, row);
};

// 删除商品类型规格
const onRowDel = (row: RowGoodTypeSpecType) => {
  ElMessageBox.confirm(`此操作将永久删除参数名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    goodTypeSpecApi
      .deleteGoodTypeSpec(row.id)
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
.good-typeSpec-container {
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
  width: 50px;
  height: 50px;
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
