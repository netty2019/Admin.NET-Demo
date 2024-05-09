<template>
	<div class="process-container">
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
						<el-form-item label="工序编号" prop="code">
							<el-input v-model="ruleForm.code" placeholder="请输入编号" maxlength="32" show-word-limit
								clearable />

						</el-form-item>

					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="工序名称" prop="name">
							<el-input v-model="ruleForm.name" placeholder="请输入名称" maxlength="80" show-word-limit
								clearable />

						</el-form-item>

					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="计价方式" prop="priceMethod">
							<el-select v-model="ruleForm.priceMethod" placeholder="请输入计价方式">
								<el-option label="计件" :value="111" />
								<el-option label="计时" :value="222" />
							</el-select>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="工资单价" prop="unitPrice">
							<el-input-number v-model="ruleForm.unitPrice" placeholder="请输入单价" min="0" :precision="2" clearable />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="作业者" prop="WorkerIds">
							<el-select v-model="ruleForm.workerIds" multiple filterable placeholder="请选择作业者">
								<el-option v-for="item in users" :key="item.id"
									:label="item.account + '|' + item.realName" :value="item.id" />
							</el-select>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="不良项目" prop="ngItemIds">
							<el-select v-model="ruleForm.ngItemIds" multiple filterable placeholder="请选择不良项目">
								<el-option v-for="item in ngItems" :key="item.id" :label="item.code + '|' + item.name"
									:value="item.id" />
							</el-select>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="报工比例" prop="ratio">
							<el-input-number v-model="ruleForm.ratio" placeholder="请输入报工比例" min="0" :precision="2" clearable />
						</el-form-item>
					</el-col>
				</el-row>
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
import { ElMessage } from "element-plus";
import type { FormRules } from "element-plus";
import { addProcess, updateProcess, detailProcess } from "/@/api/main/process";

import { getAPI } from '/@/utils/axios-utils';
import { SysUserApi } from '/@/api-services/api';

import { pageNgItem } from '/@/api/main/ngItem';

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

const users = ref<any>([]);
const getUsers = async () => {
	users.value = (await getAPI(SysUserApi).apiSysUserPagePost({ pageSize: 1000 })).data.result?.items;
};

const ngItems = ref<any>([]);
const getNgItems = async () => {
	ngItems.value = (await pageNgItem({ pageSize: 1000 })).data.result?.items;
};

//自行添加其他规则
const rules = ref<FormRules>({
	code: [{ required: true, message: '请输入编号！', trigger: 'blur', },],
	name: [{ required: true, message: '请输入名称！', trigger: 'blur', },],
	priceMethod: [{ required: true, message: '请输入计价方式！', trigger: 'blur', },],
	ratio: [{ required: true, message: '请输入报工比例！', trigger: 'blur', },],
});

// 打开弹窗
const openDialog = async (row: any) => {
	// ruleForm.value = JSON.parse(JSON.stringify(row));
	// 改用detail获取最新数据来编辑
	let rowData = JSON.parse(JSON.stringify(row));
	if (rowData.id)
		ruleForm.value = (await detailProcess(rowData.id)).data.result;
	else
		ruleForm.value = rowData;
	if (!ruleForm.value.id) {
		ruleForm.value.ratio = 1;
	}
	isShowDialog.value = true;
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
				await addProcess(values);
			} else {
				await updateProcess(values);
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
	getUsers();
	getNgItems();
});

//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
