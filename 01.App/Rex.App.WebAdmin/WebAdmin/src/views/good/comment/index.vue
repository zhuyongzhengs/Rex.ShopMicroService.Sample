<template>
  <div class="good-comment-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="good-comment-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.goodName"
              size="default"
              placeholder="请选择商品名称"
              clearable
              readonly
            >
              <template #append>
                <el-button size="default" :icon="Search" @click="onOpenSelectGood" />
              </template>
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.orderNo"
              size="default"
              placeholder="请选择订单号"
              clearable
              readonly
            >
              <template #append>
                <el-button
                  size="default"
                  :icon="ShoppingCartFull"
                  @click="onOpenSelectOrder"
                />
              </template>
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.userName"
              size="default"
              placeholder="请选择评价用户"
              clearable
              readonly
            >
              <template #append>
                <el-button size="default" :icon="User" @click="onOpenSelectUser" />
              </template>
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.score"
              placeholder="请选择评星"
              size="default"
            >
              <el-option label="★" :value="1"></el-option>
              <el-option label="★★" :value="2"></el-option>
              <el-option label="★★★" :value="3"></el-option>
              <el-option label="★★★★" :value="4"></el-option>
              <el-option label="★★★★★" :value="5"></el-option>
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isDisplay"
              placeholder="请选择前台显示"
              size="default"
            >
              <el-option label="是" :value="true"></el-option>
              <el-option label="否" :value="false"></el-option>
            </el-select>
          </el-col>
          <el-col :span="4">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchGoodComment"
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
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column type="index" label="序号" width="60" fixed />
        <el-table-column
          prop="isDisplay"
          label="前台显示"
          show-overflow-tooltip
          width="100"
          fixed
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isDisplay"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsDisplay($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="userName"
          label="评价用户"
          show-overflow-tooltip
          width="200"
        />
        <el-table-column prop="score" label="评星" width="160">
          <template #default="scope">
            <el-rate v-model="scope.row.score" disabled />
          </template>
        </el-table-column>
        <el-table-column
          prop="contentBody"
          label="评价内容"
          show-overflow-tooltip
          width="200"
        ></el-table-column>
        <el-table-column
          prop="shipMobile"
          label="联系方式"
          show-overflow-tooltip
          width="120"
        />
        <el-table-column
          prop="orderNo"
          label="订单号"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="goodName"
          label="商品名称"
          show-overflow-tooltip
          min-width="320"
        />
        <el-table-column prop="images" label="图集" width="150">
          <template #default="scope">
            <el-text type="info" v-if="!scope.row.images">无</el-text>
            <el-image
              v-else
              class="img-logo"
              :src="scope.row.images[0]"
              :zoom-rate="1.2"
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="scope.row.images.split(';')"
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
          prop="sellerContent"
          label="商家回复"
          show-overflow-tooltip
          width="200"
        ></el-table-column>
        <el-table-column
          prop="creationTime"
          label="评价日期"
          width="170"
        ></el-table-column>
        <el-table-column label="操作" fixed="right" width="130">
          <template #default="scope">
            <el-button
              size="small"
              text
              type="info"
              @click="openGoodComment('查看详情', scope.row)"
              >查看</el-button
            >
            <el-button
              v-auth="'GoodService.GoodComments.SellerReply'"
              size="small"
              text
              type="warning"
              @click="openGoodComment('商家回复', scope.row)"
              >回复</el-button
            >
            <el-button
              v-auth="'GoodService.GoodComments.Delete'"
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
    <select-goods
      selectionType="single"
      ref="selectGoodsRef"
      @selGoodsResult="onSelectGoodsResult"
    />
    <user-select-dialog
      selectionType="single"
      ref="userSelectOuDialogRef"
      @selUserResult="selUserResultData"
    />
    <select-order
      selectionType="single"
      ref="selectOrderRef"
      @selOrderResult="onSelectOrderResult"
    />
    <good-comment-dialog ref="goodCommentDialogRef" @refresh="getTableData" />
  </div>
</template>

<script setup lang="ts" name="goodComment">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Search, User, ShoppingCartFull } from "@element-plus/icons-vue";
import _ from "lodash";
import { useGoodCommentApi } from "/@/api/good/comment/index";

// 引入组件
const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);
const UserSelectDialog = defineAsyncComponent(
  () => import("/@/views/member/sysUser/components/selectUser.vue")
);
const SelectOrder = defineAsyncComponent(
  () => import("/@/views/orderManage/order/components/selectOrderDialog.vue")
);
const GoodCommentDialog = defineAsyncComponent(
  () => import("/@/views/good/comment/components/commentDialog.vue")
);

// 引入商品评价 Api 请求接口
const commentApi = useGoodCommentApi();

// 定义变量内容
const selectGoodsRef = ref();
const userSelectOuDialogRef = ref();
const selectOrderRef = ref();
const goodCommentDialogRef = ref();
const state = reactive<GoodCommentState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      score: null,
      userId: null,
      userName: null,
      goodId: null,
      goodName: null,
      orderId: null,
      orderNo: null,
      isDisplay: null,
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 打开商品评价明细
const openGoodComment = (title: string, row: RowGoodCommentType) => {
  goodCommentDialogRef.value.openDialog(title, row);
};

// 打开新增用户弹窗
const onOpenSelectUser = () => {
  userSelectOuDialogRef.value.openDialog("选择用户");
};

// 选择用户结果
const selUserResultData = (user: RowUserType) => {
  state.tableData.param.userId = user.id;
  state.tableData.param.userName = user.name;
};

// 打开选择订单
const onOpenSelectOrder = () => {
  selectOrderRef.value.openDialog("选择订单");
};

// 选择的订单返回结果
const onSelectOrderResult = (order: RowOrderType) => {
  state.tableData.param.orderId = order.id;
  state.tableData.param.orderNo = order.no;
};

// 打开选择商品
const onOpenSelectGood = () => {
  selectGoodsRef.value.openDialog("选择商品");
};

// 选择的商品返回结果
const onSelectGoodsResult = (good: RowGoodsType) => {
  state.tableData.param.goodId = good.id;
  state.tableData.param.goodName = good.name;
};

// 修改是否显示
const onIsDisplay = (val: boolean, comment: RowGoodCommentType) => {
  commentApi
    .updateIsDisplay(comment.id, val)
    .then((result) => {
      if (!result) {
        console.error("修改[是否显示]失败！");
      }
    })
    .catch((err: any) => {
      comment.isDisplay = !val;
      console.error("修改[是否显示]失败：", err);
    });
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  commentApi
    .getGoodCommentList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商品评价列表出错：", err);
    });
};

// 搜索
const searchGoodComment = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.goodId = null;
  state.tableData.param.goodName = null;
  state.tableData.param.score = null;
  state.tableData.param.userId = null;
  state.tableData.param.userName = null;
  state.tableData.param.orderId = null;
  state.tableData.param.orderNo = null;
  state.tableData.param.isDisplay = null;
};

// 删除商品评价
const onRowDel = (row: RowGoodCommentType) => {
  ElMessageBox.confirm("您确定要删除该用户的评价吗？", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    commentApi
      .deleteGoodsMany([row.id])
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
.good-comment-container {
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
