<template>
  <div class="ship-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="ship-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入配送方式名称"
              maxlength="100"
              clearable
            >
            </el-input>
          </el-col>
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
            <el-button size="default" type="primary" class="ml10" @click="searchShip">
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
          v-auth="'OrderService.Ships.Create'"
          type="success"
          @click="onOpenAddShip('add')"
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
        <el-table-column prop="sort" label="排序" show-overflow-tooltip width="65" />
        <el-table-column prop="name" label="配送方式名称" show-overflow-tooltip />
        <el-table-column
          prop="logiCode"
          label="物流编码"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="logiName"
          label="物流名称"
          show-overflow-tooltip
          width="180"
        />
        <el-table-column prop="status" label="状态" width="90">
          <template #default="scope">
            <el-tag type="success" size="default" v-if="scope.row.status === 1"
              >启用</el-tag
            >
            <el-tag type="info" size="default" v-else>禁用</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="isDefault"
          label="是否默认"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isDefault"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsDefault($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="isfreePostage"
          label="是否包邮"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isfreePostage"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsFreePostage($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'OrderService.Ships.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditShip('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'OrderService.Ships.Delete'"
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
    <ship-dialog ref="shipDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="ship">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useShipApi } from "/@/api/shopSetting/ship/index";

// 引入组件
const ShipDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/ships/components/shipDialog.vue")
);

// 引入物流 Api 请求接口
const shipApi = useShipApi();

// 定义变量内容
const shipDialogRef = ref();
const state = reactive<ShipState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      logiCode: null,
      logiName: null,
      name: null,
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
  shipApi
    .getShipList(state.tableData.param)
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
const searchShip = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.logiCode = null;
  state.tableData.param.logiName = null;
  state.tableData.param.name = null;
};

// 打开新增物流弹窗
const onOpenAddShip = (type: string) => {
  shipDialogRef.value.openDialog(type);
};

// 打开修改物流弹窗
const onOpenEditShip = (type: string, row: RowShipType) => {
  shipDialogRef.value.openDialog(type, row);
};

// 删除物流
const onRowDel = (row: RowShipType) => {
  ElMessageBox.confirm(`此操作将永久删除配送方式名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    shipApi
      .deleteShip(row.id)
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

// 修改是否默认
const onIsDefault = (val: boolean, ship: RowShipType) => {
  shipApi
    .updateShipIsDefault(ship.id, val)
    .then((result) => {
      if (result) {
        getTableData();
      }
      console.log("修改[" + ship.name + "]是否默认显示，结果：", result);
    })
    .catch((err: any) => {
      ship.isDefault = !val;
      console.error("修改是否默认出错：", err);
    });
};

// 修改是否包邮
const onIsFreePostage = (val: boolean, ship: RowShipType) => {
  shipApi
    .updateShipIsfreePostage(ship.id, val)
    .then((result) => {
      console.log("修改[" + ship.name + "]是否包邮显示，结果：", result);
    })
    .catch((err: any) => {
      ship.isDefault = !val;
      console.error("修改是否包邮出错：", err);
    });
};

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.ship-container {
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
