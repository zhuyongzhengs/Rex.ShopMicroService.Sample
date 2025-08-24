<template>
  <div class="shop-setting-area-container layout-padding">
    <el-row :gutter="15">
      <el-col :span="6">
        <div class="card-tree-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <span class="iconfont icon-shuxing">&nbsp;区域信息</span>
                <el-button
                  text
                  :icon="Refresh"
                  title="刷新"
                  @click="refreshTreeData"
                ></el-button>
              </div>
            </template>
            <div class="tree-content" :style="'height: ' + (itemHeight + 30) + 'px'">
              <el-tree
                ref="elTreeRef"
                v-loading="treeState.treeData.loading"
                node-key="id"
                :data="treeState.treeData.data"
                :props="treeState.treeData.props"
                :expand-on-click-node="false"
                :load="loadTreeAreaNode"
                lazy
                @current-change="treeCurrentChange"
              >
                <template #default="{ node }">
                  <span>
                    {{ node.label }}
                  </span>
                </template>
              </el-tree>
            </div>
          </el-card>
        </div>
      </el-col>
      <el-col :span="18">
        <div class="card-area-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <el-button
                  size="default"
                  v-auth="'GoodService.Areas.Create'"
                  type="success"
                  @click="onOpenAddArea('add')"
                >
                  <el-icon>
                    <ele-Plus />
                  </el-icon>
                  添加下级
                </el-button>
              </div>
            </template>
            <div class="area-content" :style="'height: ' + itemHeight + 'px'">
              <el-table
                :data="state.tableData.data"
                v-loading="state.tableData.loading"
                :empty-text="
                  state.tableData.param.parentId
                    ? '暂无数据'
                    : '选择左侧区域信息进行浏览.'
                "
                style="width: 100%"
                height="480"
              >
                <el-table-column fixed prop="sort" label="排序" width="60" />
                <el-table-column
                  prop="depth"
                  label="地区深度"
                  show-overflow-tooltip
                  width="90"
                >
                  <template #default="scope">
                    <div class="column-center">{{ scope.row.depth }}</div>
                  </template>
                </el-table-column>
                <el-table-column prop="parentId" label="上级名称" show-overflow-tooltip>
                  <template #default="scope">
                    <span v-if="scope.row.parentArea">{{
                      scope.row.parentArea.name
                    }}</span>
                    <span v-else>{{ scope.row.parentId }}</span>
                  </template>
                </el-table-column>
                <el-table-column prop="name" label="地区名称" show-overflow-tooltip>
                  <template #default="scope">
                    <span>{{ scope.row.name }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="postalCode"
                  label="邮编"
                  show-overflow-tooltip
                  width="100"
                >
                  <template #default="scope">
                    <div
                      v-show="scope.row.postalCode && Number(scope.row.postalCode) > 0"
                    >
                      {{ scope.row.postalCode }}
                    </div>
                  </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" width="100">
                  <template #default="scope">
                    <el-button
                      v-auth="'GoodService.Areas.Update'"
                      size="small"
                      text
                      type="warning"
                      @click="onOpenEditArea('edit', scope.row)"
                      >修改</el-button
                    >
                    <el-button
                      v-auth="'GoodService.Areas.Delete'"
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
            </div>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <area-dialog ref="areaDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="shop-settingArea">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Refresh } from "@element-plus/icons-vue";
import type Node from "element-plus/es/components/tree/src/model/node";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";

// 引入组件
const AreaDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/area/components/areaDialog.vue")
);

// 引入组织单位 Api 请求接口
const areaApi = useGoodAreaApi();

const itemHeight = ref(0);
const areaDialogRef = ref();
const elTreeRef = ref();
const treeState = reactive({
  treeData: {
    props: {
      children: "children",
      label: "name",
    },
    data: [] as any,
    loading: true,
    selectedTreeItem: {} as RowAreaType,
  },
});
const state = reactive<AreaState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      parentId: null,
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 刷新树数据
const refreshTreeData = () => {
  treeState.treeData.loading = true;
  treeState.treeData.data = [];
  loadGoodAreaTree(0, null, (result: any) => {
    treeState.treeData.data = result;
  });
};

// 加载区域树节点
const loadTreeAreaNode = (node: Node, resolve: (data: any) => void) => {
  let parentId = 0;
  let depth = null;
  if (node.level > 0) {
    parentId = node.data.id;
    depth = node.data.depth + 1;
  }
  // 获取节点数据
  loadGoodAreaTree(parentId, depth, (result: any) => {
    resolve(result);
  });
};

// 加载区域树数据
const loadGoodAreaTree = (
  parentId: number,
  depth: number | null = null,
  callback: Function
) => {
  areaApi
    .loadGoodAreaTree(parentId, depth)
    .then((result) => {
      treeState.treeData.loading = false;
      callback(result);
    })
    .catch((err: any) => {
      console.error("查询区域树出错：", err);
    });
};

// 区域树选中节点变化时触发
const treeCurrentChange = (data: any, node: any) => {
  state.tableData.param.parentId = data.id;
  treeState.treeData.selectedTreeItem = data;
  searchArea();
};

// 打开新增区域弹窗
const onOpenAddArea = (type: string) => {
  if (!state.tableData.param.parentId) {
    ElMessage.warning("请选择左侧区域信息进行添加.");
    return;
  }
  areaDialogRef.value.openDialog(type, null, treeState.treeData.selectedTreeItem);
};

// 打开修改区域弹窗
const onOpenEditArea = (type: string, row: RowAreaType) => {
  areaDialogRef.value.openDialog(type, row, row.parentArea);
};

// 删除区域
const onRowDel = (row: RowAreaType) => {
  ElMessageBox.confirm(`此操作将永久删除区域名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    areaApi
      .deleteGoodArea(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
          elTreeRef.value?.remove(row);
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 搜索
const searchArea = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  areaApi
    .getGoodAreaList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询区域列表出错：", err);
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
  itemHeight.value = window.innerHeight - 215;
});
</script>

<style lang="scss">
.card-tree-container {
  .el-card__body {
    padding: 5px !important;
  }
}
</style>
<style scoped lang="scss">
.shop-setting-area-container {
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

.card-tree-container {
  .tree-content {
    overflow-y: auto;
  }
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 25px;
}

.ur-tabs > .el-tabs__content {
  padding: 32px;
  color: #6b778c;
  font-size: 32px;
  font-weight: 600;
}

.column-center {
  text-align: center;
}
</style>
