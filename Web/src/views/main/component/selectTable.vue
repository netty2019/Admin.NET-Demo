<template>
    <div class="selectTable-container">
        <el-dialog v-model="isShowDialog" :width="800"   draggable="">
            <template #header>
                <div style="color: #fff">
                    <!--<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>-->
                    <span>{{ title }}</span>
                </div>
            </template>
            <el-form :model="queryParams" ref="queryForm"  labelWidth="60">
                <el-row>
                    <el-col :span="14">
                        <el-form-item label="关键字">
                            <el-input v-model="queryParams.searchKey" clearable="" placeholder="请输入模糊查询关键字">
                                <template #append>
                                    <el-button :icon="Search" @click="onSearch" />
                                </template>
                            </el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <Table style="margin-top: 10px" height="300" ref="tableRef"  v-bind="tableData" :getData="getData"
                @selectionChange="tableSelection" border>
                <template #priceMethod="scope">
                    <el-tag v-if="scope.row.priceMethod == 111" type="primary">计件</el-tag>
                    <el-tag v-else type="danger">计时</el-tag>
                </template>
            </Table>
            <template #footer>
                <el-row>
                    <el-col :span="16"></el-col>
                    <el-col :span="8" style="display: flex;justify-content: flex-end;">
                        <el-button type="default" @click="handleCancel">取消</el-button>
                        <el-button type="primary" @click="handleConfirm">确定</el-button>
                    </el-col>
                </el-row>
            </template>
        </el-dialog>
    </div>
</template>

<script lang="ts" setup>
import { ref, defineAsyncComponent, nextTick, watch, onMounted } from "vue";
import { pageProcess } from '/@/api/main/process';
import { Search } from '@element-plus/icons-vue'
import { handleEmpty } from "/@/utils/other";
import { ElMessage } from "element-plus";
import { useTagsViewRoutes } from "/@/stores/tagsViewRoutes";
// 引入组件
const Table = defineAsyncComponent(() => import('/@/components/table/index.vue'));
const TableSearch = defineAsyncComponent(() => import('/@/components/table/search.vue'));
const tableRef = ref<any>();
const queryParams = ref<any>({});
const tableData = ref<any>({
    // 表头内容（必传，注意格式）
    columns: [

    ],
    // 配置项（必传）
    config: {
        isStripe: true, // 是否显示表格斑马纹
        isBorder: false, // 是否显示表格边框
        isSerialNo: false, // 是否显示表格序号
        isSelection: true, // 是否勾选表格多选
        showSelection: true, //是否显示表格多选
        pageSize: 10, // 每页条数
        hideExport: true, //是否隐藏导出按钮
    },
    param: {},
    defaultSort: {
        prop: 'id',
        order: 'ascending',
    },
});
var props = defineProps({
    title: {
        type: String,
        default: "",
    },
    type: {
        type: String,
        default: '',
    }
});

// 搜索点击时表单回调
const onSearch = () => {
    tableData.value.param = Object.assign({}, tableData.value.param, { ...queryParams.value });
    nextTick(() => {
        tableRef.value.pageReset();
    });
};

watch(
    () => props.type,
    (newValue, oldValue) => {
        updateColumns();
    },
);


const updateColumns = () => {
    if (props.type == 'process') {
        //工序
        tableData.value.columns = [
            { prop: 'code', minWidth: 150, label: '工序编号', sortable: 'code', isCheck: true },
            { prop: 'name', minWidth: 150, label: '工序名称', sortable: 'name', isCheck: true },
            { prop: 'priceMethod', minWidth: 150, label: '计价方式', sortable: 'priceMethod', isCheck: true },
            { prop: 'unitPrice', minWidth: 150, label: '工资单价', isCheck: true }
        ];
    }
};


const getData = (param: any) => {
    if (props.type == 'process') {
        //工序
        return pageProcess(param).then((res) => {
            return res.data;
        });
    }
};

const selectData = ref<any>();
const tableSelection = (data: EmptyObjectType[]) => {
    selectData.value = data;
};

//定义确认、取消事件
const emit = defineEmits(['confirm']);

//确认事件
const handleConfirm = () => {
    isShowDialog.value = false;
    emit('confirm', selectData);
};

//取消事件
const handleCancel = () => {
    isShowDialog.value = false;
};


onMounted(() => {
    updateColumns();
});

const isShowDialog = ref(false);
const show = async () => {
    isShowDialog.value = true;
};

defineExpose({ show });

</script>

<style scoped>
:deep(.table-header) {
    display: none !important;
}
</style>