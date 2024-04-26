<template>
	<div class="so-container">
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
							<el-input v-model="ruleForm.code" placeholder="请输入编号" maxlength="32" show-word-limit clearable />
							
						</el-form-item>
						
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="客户" prop="customer">
							<el-input v-model="ruleForm.customer" placeholder="请输入客户" maxlength="32" show-word-limit clearable />
							
						</el-form-item>
						
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="创建者部门名称" prop="createOrgName">
							<el-input v-model="ruleForm.createOrgName" placeholder="请输入创建者部门名称" maxlength="64" show-word-limit clearable />
							
						</el-form-item>
						
					</el-col>
					<el-divider border-style="dashed" content-position="center">
								<div style="color: #b1b3b8">销售订单行</div>
					</el-divider>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-button icon="ele-Plus" type="primary" plain @click="addSoItem"> 增加销售订单行 </el-button>
					</el-col>

					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<template v-if="ruleForm.items != undefined && ruleForm.items.length > 0">
							<el-row :gutter="35" v-for="(v, k) in ruleForm.items" :key="k">
								<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="行号" :prop="`items[${k}].lineNum`" :rules="[{ required: true, message: `行号不能为空`, trigger: 'blur' }]">
										<template #label>
											<el-button icon="ele-Delete" type="danger" circle plain size="small" @click="deleteSoItem(k)" />
											<span class="ml5">行号</span>
										</template>
										<el-input v-model="v.lineNum" placeholder="请输入行号" maxlength="32" show-word-limit clearable />
									</el-form-item>
								</el-col>
								<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="物料" :prop="`items[${k}].name`" :rules="[{ required: true, message: `物料号不能为空`, trigger: 'blur' }]">
										<el-input v-model="v.name" placeholder="请输入物料号" maxlength="32" show-word-limit clearable />
									</el-form-item>
								</el-col>
							</el-row>
						</template>
						<el-empty :image-size="50" description="空数据" v-else></el-empty>
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
	import { ref,onMounted } from "vue";
	import { getDictDataItem as di, getDictDataList as dl } from '/@/utils/dict-utils';
	import { ElMessage } from "element-plus";
	import type { FormRules } from "element-plus";
	import { addSo, updateSo, detailSo } from "/@/api/main/so";

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
	});

	// 打开弹窗
	const openDialog = async (row: any) => {
		// ruleForm.value = JSON.parse(JSON.stringify(row));
		// 改用detail获取最新数据来编辑
		let rowData = JSON.parse(JSON.stringify(row));
		if (rowData.id)
			ruleForm.value = (await detailSo(rowData.id)).data.result;
		else
			ruleForm.value = rowData;
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
					await addSo(values);
				} else {
					await updateSo(values);
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


    const deleteSoItem=(k:number)=>{
		 //@ts-ignore
        ruleForm.value.items?.splice(k,1);
	};

    const addSoItem=()=>{
		//@ts-ignore
        if(ruleForm.value.items==undefined)
		{
		//@ts-ignore
		   ruleForm.value.items=[];
		}
	    //@ts-ignore
		console.log(ruleForm.value.items);
		//@ts-ignore
		ruleForm.value.items?.push({});
	};


	// 页面加载时
	onMounted(async () => {
	});

	//将属性或者函数暴露给父组件
	defineExpose({ openDialog });
</script>




