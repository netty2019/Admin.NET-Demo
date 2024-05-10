<template>
    <div class="selectTable-container">
        <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
            <TableSearch :search="tableData.search" @search="onSearch" />
        </el-card>
        <el-card class="full-table" shadow="hover" style="margin-top: 5px">
            <Table ref="tableRef" v-bind="tableData" :getData="getData" border> </Table>
        </el-card>
    </div>
</template>

<script lang="ts" setup>
import { ref, defineAsyncComponent, nextTick, watch } from "vue";
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
        isSerialNo: true, // 是否显示表格序号
        isSelection: true, // 是否勾选表格多选
        showSelection: true, //是否显示表格多选
        pageSize: 100, // 每页条数
        hideExport: true, //是否隐藏导出按钮
    },
    // 搜索表单，动态生成（传空数组时，将不显示搜索，type有3种类型：input,date,select）
    search: [
        { label: '关键字', prop: 'searchKey', placeholder: '请输入模糊查询关键字', required: false, type: 'input' },
    ],
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
const onSearch = (data: any) => {
    tableData.value.param = Object.assign({}, tableData.value.param, { ...data });
    nextTick(() => {
        tableRef.value.pageReset();
    });
};

watch(
    () => props.type,
    (newValue, oldValue) => {
        if (newValue == 'process') {
            //工序
            tableData.value.columns = [
                { prop: 'code', minWidth: 150, label: '工序编号', headerAlign: 'center', sortable: 'code' },
                { prop: 'name', minWidth: 150, label: '工序名称', headerAlign: 'center', sortable: 'name' },
            ];
        }
    },
);


const getData = (param: any) => {
    if (props.type == 'process') {
        //工序
        return [];
    }
    return [];
};


</script>

<style scoped>
:deep(.el-ipnut),
:deep(.el-select),
:deep(.el-input-number) {
    width: 100%;
}
</style>