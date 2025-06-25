<template>
  <lay-card class="container">
    <lay-table
      class="lay-table-demo"
      ref="tableRef"
      :resize="true"
      :data-source="tbdata"
      :columns="columns"
      v-model:selectedKeys="selectedKeys"
    >
      <template v-slot:toolbar>
        <lay-button type="primary" @click="onSave">保存</lay-button>
      </template>
      <template #createTime="{ row }">
        {{ formatTimeNative(row.createTime) }}
      </template>
      <template #updateTime="{ row }">
        {{ formatTimeNative(row.updateTime) }}
      </template>
    </lay-table>
  </lay-card>
</template>
<script lang="ts" setup>
import { reactive, ref, watch, computed, onMounted } from "vue";
import { layer } from "@layui/layui-vue";
import { useProjStore } from "../../../store/proj";
import { get_list_router } from "../../../api/module/proj";
import { formatCurrentTime, formatTimeNative } from "../../../api/timeutil";

const projStore = useProjStore();
const selectedKeys: any = ref([]);
const tableRef = ref();
const columns = ref([
  { title: "选项", width: "55px", type: "checkbox", fixed: "left" },
  { title: "id", key: "id", hide: true },
  { title: "名称", key: "projectName" },
  {
    title: "创建时间",
    customSlot: "createTime",
  },
  { title: "修改时间", customSlot: "updateTime" },
]);
const tbdata: any = ref([]);
onMounted(() => {
  get_list_router(1, 9999)
    .then(({ data }) => {
      tbdata.value = data.items;
      selectedKeys.value = projStore.currentProj.routeProjs?.map(
        (o: any) => o.id
      );
    })
    .catch((err) => {});
});

const onSave = () => {
  // layer.msg(selectedKeys.value);
  projStore.saveProjRoutes(selectedKeys.value);
  // layer.msg(JSON.stringify(tableRef.value.getCheckData()));
};
</script>
<style scoped>
.container {
  margin: 0px;
  width: 100%;
  min-height: 300px;
}

.lay-table-demo {
  overflow: auto;
  margin: 0;
}
</style>
