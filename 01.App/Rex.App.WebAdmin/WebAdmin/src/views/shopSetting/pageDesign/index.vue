<template>
  <div class="page-design-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="page-design-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.code"
              size="default"
              placeholder="请输入设计编码"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入设计名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="16">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchPageDesign"
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
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.PageDesigns.Create'"
          type="success"
          @click="onOpenAddPageDesign('add')"
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
        <el-table-column prop="code" label="设计编码" show-overflow-tooltip width="150" />
        <el-table-column prop="name" label="设计名称" show-overflow-tooltip />
        <el-table-column prop="description" label="描述" show-overflow-tooltip />
        <el-table-column prop="layout" label="布局样式" show-overflow-tooltip width="110">
          <template #default="scope">
            <el-tag v-if="scope.row.layout == 1">移动端</el-tag>
            <el-tag type="info" v-else-if="scope.row.layout == 2">PC端</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="type" label="是否默认" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-switch
              v-model="scope.row.type"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              :activeValue="1"
              :inactiveValue="2"
              @change="onTypeChange($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column label="操作" width="160">
          <template #default="scope">
            <!--
            <el-button
              v-auth="'GoodService.PageDesigns.Preview'"
              size="small"
              text
              type="info"
              >预览</el-button
            >
            <el-button
              v-auth="'GoodService.PageDesigns.Copy'"
              size="small"
              text
              type="success"
              >复制新增</el-button
            >
            -->
            <el-button
              v-auth="'GoodService.PageDesigns.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditPageDesign('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.PageDesigns.Layout'"
              size="small"
              text
              type="warning"
              @click="onLayout(scope.row)"
              >版面设计</el-button
            >
            <el-button
              v-auth="'GoodService.PageDesigns.Delete'"
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
    <page-design-dialog ref="pageDesignDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="pageDesign">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useRouter } from "vue-router";
import { usePageDesignApi } from "/@/api/shopSetting/pageDesign/index";

// 引入组件
const PageDesignDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/pageDesign/components/pageDesignDialog.vue")
);

// 引入页面设计 Api 请求接口
const pageDesignApi = usePageDesignApi();

// 定义变量内容
const pageDesignDialogRef = ref();
const router = useRouter();
const state = reactive<PageDesignState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      code: null,
      name: null,
      sorting: "creationTime desc",
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
  pageDesignApi
    .getPageDesignList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询页面设计列表出错：", err);
    });
};

// 搜索
const searchPageDesign = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.code = null;
  state.tableData.param.name = null;
};

// 是否默认
const onTypeChange = (val: number, pageDesign: RowPageDesignType) => {
  pageDesignApi
    .updatePageDesignIsType(pageDesign.id, val)
    .then((result) => {
      console.log("修改[" + pageDesign.name + "]是否默认显示，结果：", result);
      searchPageDesign();
    })
    .catch((err: any) => {
      pageDesign.type = val == 1 ? 2 : 1;
      console.error("修改是否默认出错：", err);
    });
};

// 打开新增页面设计弹窗
const onOpenAddPageDesign = (type: string) => {
  pageDesignDialogRef.value.openDialog(type);
};

// 打开修改页面设计弹窗
const onOpenEditPageDesign = (type: string, row: RowPageDesignType) => {
  pageDesignDialogRef.value.openDialog(type, row);
};

// 打开版面设计
const onLayout = (row: RowPageDesignType) => {
  if (row.layout != 1) {
    ElMessage.warning("目前仅支持[移动端]版面设计！");
    return;
  }
  const params: EmptyObjectType = { id: row.id };
  router.push({
    path: "/shopSetting/pageDesign/layout",
    query: params,
  });
};

// 删除页面设计
const onRowDel = (row: RowPageDesignType) => {
  ElMessageBox.confirm(`此操作将永久删除设计名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    pageDesignApi
      .deletePageDesign(row.id)
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
.page-design-container {
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
