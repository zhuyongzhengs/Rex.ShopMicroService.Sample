<template>
  <div class="member-user-we-chat-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="member-user-we-chat-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.nickName"
              size="default"
              placeholder="请输入昵称"
              maxlength="20"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.mobile"
              @input="onVerifiyNumberInteger($event)"
              size="default"
              class="search-input mr15"
              placeholder="请输入手机号码"
              maxlength="20"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.openId"
              size="default"
              class="search-input"
              placeholder="请输入OpenId"
              maxlength="50"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="12">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchUserWeChat"
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
        <el-table-column prop="avatar" label="头像" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-avatar :size="50" :src="scope.row.avatar" />
          </template>
        </el-table-column>
        <el-table-column prop="nickName" label="昵称" show-overflow-tooltip width="240" />
        <el-table-column
          prop="userName"
          label="关联用户"
          show-overflow-tooltip
          width="240"
        />
        <el-table-column
          prop="mobile"
          label="手机号码"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="type"
          label="第三方登录类型"
          show-overflow-tooltip
          width="130"
        >
          <template #default="scope">
            <span>{{ showUserAccountType(scope.row.type) }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="openId" label="OpenId" show-overflow-tooltip width="275" />
        <el-table-column
          prop="unionid"
          label="Unionid"
          show-overflow-tooltip
          width="275"
        />
        <el-table-column prop="gender" label="性别" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-tag v-if="scope.row.gender == 1" class="ml-2" type="info">男</el-tag>
            <el-tag v-else-if="scope.row.gender == 2" class="ml-2" type="info">女</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="city" label="城市" show-overflow-tooltip width="100" />
        <el-table-column prop="province" label="省" show-overflow-tooltip width="100" />
        <el-table-column prop="country" label="国家" show-overflow-tooltip width="100" />
        <el-table-column
          prop="creationTime"
          label="注册日期"
          width="170"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.creationTime }}</span>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="100">
          <template #default="scope">
            <el-button size="small" text type="primary" @click="onLookDetail(scope.row)"
              >查看详情</el-button
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

    <el-drawer
      v-model="userWeChatDetailDrawer.showDetail"
      title="查看详情"
      :with-header="true"
    >
      <div class="detail-container">
        <el-card shadow="never" class="layout-padding-auto mb15">
          <div class="user-info-content" v-if="userWeChatDetailDrawer.userWeChatData">
            <el-form size="default" label-width="80px">
              <el-row :gutter="35">
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <div class="u-avatar mb15">
                    <el-avatar
                      :size="100"
                      :src="userWeChatDetailDrawer.userWeChatData.avatar"
                    />
                  </div>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="昵称:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.nickName
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="关联用户:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.userName
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="OpenId:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.openId
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="SessionKey:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.sessionKey
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="UnionId:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.unionId
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24">
                  <el-form-item label="性别:">
                    <span v-if="userWeChatDetailDrawer.userWeChatData.gender == 1"
                      >男</span
                    >
                    <span v-else-if="userWeChatDetailDrawer.userWeChatData.gender == 2"
                      >女</span
                    >
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="14" :xl="24">
                  <el-form-item label="手机号码:">
                    <span
                      class="detail-item"
                      v-if="
                        userWeChatDetailDrawer.userWeChatData.countryCode &&
                        userWeChatDetailDrawer.userWeChatData.mobile
                      "
                      >({{
                        "+" + userWeChatDetailDrawer.userWeChatData.countryCode
                      }})</span
                    >
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.mobile
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24">
                  <el-form-item label="国家:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.country
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24">
                  <el-form-item label="省份:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.province
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24">
                  <el-form-item label="城市:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.city
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="注册日期:">
                    <span class="detail-item">{{
                      userWeChatDetailDrawer.userWeChatData.creationTime
                    }}</span>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </div>
        </el-card>
      </div>
    </el-drawer>
  </div>
</template>

<script setup lang="ts" name="memberUserWeChat">
import { ref, reactive, onMounted } from "vue";
import _ from "lodash";
import { useUserWeChatApi } from "/@/api/system/userWeChat/index";
import { verifiyNumberInteger } from "/@/utils/toolsValidate";

// 引入微信用户 Api 请求接口
const userWeChatApi = useUserWeChatApi();

// 定义变量内容
const userAccountTypes = ref([] as EnumValueType[]);
const state = reactive<MemberUserWeChatState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      nickName: "",
      mobile: "",
      openId: "",
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 定义查看详情
const userWeChatDetailDrawer = reactive({
  showDetail: false,
  userWeChatData: {} as any,
});

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  userWeChatApi
    .getUserWeChatList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询微信用户列表出错：", err);
    });
};

// 搜索
const searchUserWeChat = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 查看详情
const onLookDetail = (row: RowUserWeChatType) => {
  userWeChatDetailDrawer.showDetail = true;
  userWeChatDetailDrawer.userWeChatData = row;
};

// 重置
const onReset = () => {
  state.tableData.param.nickName = "";
  state.tableData.param.mobile = "";
  state.tableData.param.openId = "";
};

// 正整数
const onVerifiyNumberInteger = (val: string) => {
  state.tableData.param.mobile = verifiyNumberInteger(val);
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

// 获取第三方用户类型
const getUserAccountTypes = () => {
  userWeChatApi
    .getUserAccountTypes()
    .then((result) => {
      if (result) {
        userAccountTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取第三方登录类型出错：", err);
    });
};

// 显示用户类型
const showUserAccountType = (type: number) => {
  let uType = userAccountTypes.value.find((p) => p.value == String(type));
  if (uType) return uType.description;
  return type;
};

// 页面加载完时
onMounted(() => {
  getUserAccountTypes();
  getTableData();
});
</script>

<style scoped lang="scss">
.member-user-we-chat-container {
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

.detail-item {
  word-wrap: break-word;
  word-break: break-all;
}

.u-avatar {
  text-align: center;
}
</style>
