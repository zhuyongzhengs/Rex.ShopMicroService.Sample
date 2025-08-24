<template>
  <div class="good-brand-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="good-brand-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入品牌名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isShow"
              size="default"
              placeholder="是否显示"
            >
              <el-option label="开启" :value="true" />
              <el-option label="关闭" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="6">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchGoodBrand"
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
          v-auth="'GoodService.GoodBrands.Create'"
          type="success"
          @click="onOpenAddGoodBrand('add')"
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
        <el-table-column prop="name" label="品牌名称" show-overflow-tooltip />
        <el-table-column
          prop="logoImageUrl"
          label="Logo"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <el-image
              class="img-logo"
              :src="scope.row.logoImageUrl"
              :zoom-rate="1.2"
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="[scope.row.logoImageUrl]"
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
        <el-table-column prop="isShow" label="是否显示" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isShow"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsShow($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column prop="sort" label="排序" show-overflow-tooltip width="65" />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.GoodBrands.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditGoodBrand('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.GoodBrands.Delete'"
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
    <good-brand-dialog ref="goodBrandDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="goodBrand">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Picture as IconPicture } from "@element-plus/icons-vue";
import _ from "lodash";
import { useGoodBrandApi } from "/@/api/good/brand/index";

// 引入组件
const GoodBrandDialog = defineAsyncComponent(
  () => import("/@/views/good/brand/components/brandDialog.vue")
);

// 引入品牌 Api 请求接口
const brandApi = useGoodBrandApi();

// 定义变量内容
const goodBrandDialogRef = ref();
const state = reactive<GoodBrandState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      isShow: null,
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
  brandApi
    .getGoodBrandList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询品牌列表出错：", err);
    });
};

// 搜索
const searchGoodBrand = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 是否显示
const onIsShow = (val: boolean, brand: RowGoodBrandType) => {
  brandApi
    .updateGoodBrandIsShow(brand.id, val)
    .then((result) => {
      console.log("修改[" + brand.name + "]是否显示，结果：", result);
    })
    .catch((err: any) => {
      brand.isShow = !val;
      console.error("修改出错：", err);
    });
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.isShow = null;
};

// 打开新增品牌弹窗
const onOpenAddGoodBrand = (type: string) => {
  goodBrandDialogRef.value.openDialog(type);
};

// 打开修改品牌弹窗
const onOpenEditGoodBrand = (type: string, row: RowGoodBrandType) => {
  goodBrandDialogRef.value.openDialog(type, row);
};

// 删除品牌
const onRowDel = (row: RowGoodBrandType) => {
  ElMessageBox.confirm(`此操作将永久删除品牌名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    brandApi
      .deleteGoodBrand(row.id)
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
.good-brand-container {
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
