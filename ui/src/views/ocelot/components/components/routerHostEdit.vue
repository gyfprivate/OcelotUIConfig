<template>
  <div>
    <lay-table
      class="lay-table-demo"
      :resize="true"
      :data-source="routeStore.currentRouter.hosts"
      :columns="columns"
      :height="'250px'"
    >
      <template v-slot:toolbar>
        <lay-button size="xs" type="primary" @click="onAdd">新增</lay-button>
      </template>
      <template v-slot:operator="{ row }">
        <lay-button size="xs" type="primary" @click="onEdit(row)"
          >编辑</lay-button
        >
        <lay-button size="xs" type="danger" @click="onDelete(row)"
          >删除</lay-button
        >
      </template>
    </lay-table>
    <!-- 编辑窗口 -->
    <lay-layer
      v-model="visible"
      :shade="false"
      :area="['400px', '300px']"
      :btn="actions"
      :title="title"
    >
      <div class="edit-layer">
        <lay-json-schema-form
          :model="form"
          :schema="schema"
          :space="space"
        ></lay-json-schema-form>
      </div>
    </lay-layer>
  </div>
</template>
<script lang="ts" setup>
import { ref, reactive } from "vue";
import { useRouterStore } from "../../../../store/ocelotRouter";
const routeStore = useRouterStore();
let formType = 0;
const space = ref(0);
const title = ref("");
const columns = ref([
  { title: "Host", key: "host" },
  { title: "端口", key: "port" },

  {
    title: "操作",
    width: "150px",
    customSlot: "operator",
    key: "operator",
    fixed: "right",
    ignoreExport: true,
  },
]);
const onEdit = (row: any) => {
  formType = 1;
  title.value = "编辑Host";
  Object.assign(form, row);
  visible.value = true;
};
const onDelete = (row: any) => {
  console.log(row);
  console.log( routeStore.currentRouter.hosts)
  // 过滤掉指定 ID 的 host
  routeStore.currentRouter.hosts = routeStore.currentRouter.hosts.filter(
    (item: any) =>
      item.id !== row.id || item.host != row.host || item.port != row.port
  );
};
const onAdd = () => {
  formType = 0;
  title.value = "新增Host";
  (form.id = ""), (form.host = ""), (form.port = 80);
  visible.value = true;
};
// 弹窗显示状态
const visible = ref(false);
// 弹窗按键
const actions = ref([
  {
    text: "确认",
    callback: () => {
      if (formType == 0) {
        if (!routeStore.currentRouter.hosts)
          routeStore.currentRouter.hosts = [];
        routeStore.currentRouter.hosts.push({ ...form });
      }
      if (formType == 1) {
        // 找到对应 ID 的 host 并更新它
        routeStore.currentRouter.hosts = routeStore.currentRouter.hosts.map(
          (item: any) => {
            if (item.id === form.id) {
              Object.assign(item, form);
            }
            return item;
          }
        );
      }
      visible.value = false;
    },
  },
  {
    text: "取消",
    callback: () => {
      visible.value = false;
    },
  },
]);
// 表单数据
const form = reactive({ id: "", host: "", port: 0 });
// 表单验证规则
const schema = reactive({
  host: {
    label: "Host",
    type: "input",
    props: {
      type: "text",
      placeholder: "127.0.0.1",
    },
    rules: [{ required: true, message: "", trigger: "blur" }],
  },
  port: {
    label: "排序",
    type: "input",
    props: {
      placeholder: "",
      min: 0,
      type: "number",
    },
  },
});
</script>
<style scoped>
.lay-table-demo {
  width: 800px;
  height: 100px;
  overflow: auto;
  margin: 0;
}
.edit-layer {
  margin: 20px;
}
</style>
