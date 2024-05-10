<template>
	<div class="route-container">
		<el-dialog v-model="isShowDialog" :width="800" draggable="">
			<template #header>
				<div style="color: #fff">
					<!--<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>-->
					<span>{{ props.title }}</span>
				</div>
			</template>
			<el-form :model="ruleForm" ref="ruleFormRef" label-width="auto" :rules="rules">
				<el-row :gutter="35">
					<el-form-item v-show="false">
						<el-input v-model="ruleForm.id" />
					</el-form-item>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="编号" prop="code">
							<el-input v-model="ruleForm.code" placeholder="请输入编号" maxlength="32" show-word-limit
								clearable />

						</el-form-item>

					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="名称" prop="name">
							<el-input v-model="ruleForm.name" placeholder="请输入名称" maxlength="80" show-word-limit
								clearable />

						</el-form-item>

					</el-col>
				</el-row>
				<el-divider border-style="dashed" content-position="center">
					<div style="color: #b1b3b8">工序</div>
				</el-divider>
				<el-row>
					<el-popover placement="right" :width="600" trigger="click">
						<template #reference>
							<el-button icon="ele-Plus" type="primary" plain> 增加
							</el-button>
						</template>
						<selectTable :type="'process'"></selectTable>
					</el-popover>

				</el-row>
				<VueDraggable target="tbody" v-model="ruleForm.processes" :animation="150" handle=".sort-handle"
					style="margin-top: 10px;">
					<el-table :data="ruleForm.processes" style="width: 100%">
						<el-table-column label="排序" width="80" align="center">
							<template #default="scope">
								<div style="display: flex; justify-content: center;align-items: center"
									class="sort-handle">
									<svg t="1715178347829" class="icon" viewBox="0 0 1024 1024" version="1.1"
										xmlns="http://www.w3.org/2000/svg" p-id="4274" width="23" height="23">
										<path
											d="M904 160H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8zM904 784H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8zM904 472H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8z"
											p-id="4275" fill="#1296db"></path>
									</svg>
								</div>
							</template>
						</el-table-column>
						<el-table-column prop="code" label="工序编号" />
						<el-table-column prop="name" label="工序名称" />
						<el-table-column label="操作" width="140" align="center" fixed="right" show-overflow-tooltip="">
							<template #default="scope">
								<el-button icon="ele-Delete" size="small" text="" type="primary"
									@click="deleteProcess(scope.row)"> 删除 </el-button>
							</template>
						</el-table-column>
					</el-table>
				</VueDraggable>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="cancel">取 消</el-button>
					<el-button type="primary" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>
<style scoped>
:deep(.el-select),
:deep(.el-input-number) {
	width: 100%;
}
</style>
<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { getDictDataItem as di, getDictDataList as dl } from '/@/utils/dict-utils';
import { ElMessageBox, ElMessage } from "element-plus";
import type { FormRules } from "element-plus";
import { addRoute, updateRoute, detailRoute } from "/@/api/main/route";
import { VueDraggable } from 'vue-draggable-plus'
import selectTable from "/@/views/main/component/selectTable.vue";
//父级传递来的参数
var props = defineProps({
	title: {
		type: String,
		default: "",
	},
});
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
	code: [{ required: true, message: '请输入编号！', trigger: 'blur', },],
	name: [{ required: true, message: '请输入名称！', trigger: 'blur', },],
});

// 打开弹窗
const openDialog = async (row: any) => {
	// ruleForm.value = JSON.parse(JSON.stringify(row));
	// 改用detail获取最新数据来编辑
	let rowData = JSON.parse(JSON.stringify(row));
	if (rowData.id)
		ruleForm.value = (await detailRoute(rowData.id)).data.result;
	else
		ruleForm.value = rowData;
	if (!ruleForm.value.processes) ruleForm.value.processes = [];
	isShowDialog.value = true;
};


//删除工序
const deleteProcess = (row: any) => {
	ElMessageBox.confirm(`确定要删除吗?`, "提示", {
		confirmButtonText: "确定",
		cancelButtonText: "取消",
		type: "warning",
	})
		.then(() => {
			let processes = ruleForm.value.processes;
			let idx = processes.indexOf(row);
			if (idx !== -1) { // 如果元素存在于数组中  
				processes.splice(idx, 1); // 删除元素  
			}
		});
};


// 关闭弹窗
const closeDialog = () => {
	emit("reloadTable");
	isShowDialog.value = false;
};

// 取消
const cancel = () => {
	isShowDialog.value = false;
};

// 提交
const submit = async () => {
	ruleFormRef.value.validate(async (isValid: boolean, fields?: any) => {
		if (isValid) {
			let values = ruleForm.value;
			if (ruleForm.value.id == undefined || ruleForm.value.id == null || ruleForm.value.id == "" || ruleForm.value.id == 0) {
				await addRoute(values);
			} else {
				await updateRoute(values);
			}
			closeDialog();
		} else {
			ElMessage({
				message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
				type: "error",
			});
		}
	});
};







// 页面加载时
onMounted(async () => {
});

//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
