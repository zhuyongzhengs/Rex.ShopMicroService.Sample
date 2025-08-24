<template>
	<div class="system-role-container layout-padding">
		<el-dialog :title="dialog.title" v-model="dialog.isShowDialog" :close-on-click-modal="false" draggable width="750px">
			<el-card shadow="hover" class="layout-padding-auto">
				<div class="system-role-search mb15">
					<el-input v-model="state.tableData.param.filter" size="default" placeholder="请输入角色名称" style="max-width: 180px"> </el-input>
					<el-button size="default" type="primary" class="ml10" @click="searchRole">
						<el-icon>
							<ele-Search />
						</el-icon>
						查询
					</el-button>
				</div>
				<el-table ref="multipleRoleTableRef" :data="state.tableData.data" v-loading="state.tableData.loading" height="280" style="width: 100%">
					<el-table-column type="selection" width="55" />
						<el-table-column prop="name" label="角色名称" show-overflow-tooltip>
						<template #default="scope">
							{{ scope.row.name }}
							<el-tag size="small" v-if="scope.row.name === 'admin'">&nbsp;系统&nbsp;</el-tag>
						</template>
					</el-table-column>
					<el-table-column prop="status" label="是否默认" width="100" show-overflow-tooltip>
						<template #default="scope">
							<el-tag type="success" v-if="scope.row.isDefault"><b>是</b></el-tag>
							<el-tag type="info" v-else>否</el-tag>
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
			<template #footer>
				<span class="dialog-footer">
					<el-button size="default" @click="onCancel">取 消</el-button>
					<el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts" name="systemRole">
import { reactive, ref, nextTick } from 'vue';
import { ElMessage, ElTable } from 'element-plus';
import { useRoleApi } from '/@/api/system/role/index';


// 定义子组件向父组件传值/事件
const emit = defineEmits(['selRoleResult']);

// 引入角色 Api 请求接口
const roleApi = useRoleApi();

// 定义变量内容
const multipleRoleTableRef = ref();
const state = reactive<SysRoleState>({
	tableData: {
		data: [],
		total: 0,
		loading: false,
		currentPage: 1,
		param: {
			filter: "",
			sorting: "name",
			skipCount: 0,
			maxResultCount: 10
		}
	}
});

// 对话框
const dialog = reactive({
	isShowDialog: false,
	title: '选择角色'
});

// 打开弹窗
const openDialog = (title: string) => {
	if(title) dialog.title = title;
	nextTick(() => {
		dialog.isShowDialog = true;
		// 查询角色
		searchRole();
	});
};

// 关闭弹窗
const closeDialog = () => {
	dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
	const selRows = multipleRoleTableRef.value.getSelectionRows() as RowRoleType[];
	if(selRows.length < 1){
		ElMessage.warning('您还未选择角色！');
		return;
	}
	emit('selRoleResult', selRows);
	closeDialog();
};

// 取消
const onCancel = () => {
	closeDialog();
};

// 搜索
const searchRole = () => {
	state.tableData.currentPage = 1;
	getTableData();
}

// 初始化表格数据
const getTableData = () => {
	state.tableData.loading = true;
	state.tableData.param.skipCount = (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
	roleApi.getRoleList(state.tableData.param).then((result) => {
		state.tableData.total = result.totalCount;
		state.tableData.data = result.items;
		state.tableData.loading = false;
	}).catch(err => {
		state.tableData.loading = false;
		console.error("查询角色列表出错：", err);
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
	state.tableData.param.skipCount = (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
	getTableData();
};

// 暴露变量
defineExpose({
	openDialog
});
</script>

<style scoped lang="scss">
.system-role-container {
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
