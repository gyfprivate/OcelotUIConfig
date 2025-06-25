<template>
  <lay-container fluid="true" style="padding: 10px">
    <lay-row :space="10">
      <lay-col :md="24">
        <lay-card>
          <!-- :default-toolbar="false" -->
          <lay-table
            :page="page"
            :columns="columns"
            :dataSource="dataSource"
            v-model:selectedKeys="selectedKeys"
            @row="rowClick"
            @change="change"
          >
            <template v-slot:toolbar>
              <lay-button size="sm" type="primary" @click="addnew"
                >新增</lay-button
              >
            </template>
            <template v-slot:state="{ data }">
              <lay-switch v-model="data.enabled"></lay-switch>
            </template>
            <template v-slot:type="{ data }">
              <lay-tag :type="'normal'" v-if="data.type === 1">Consul</lay-tag>
            </template>
            <template v-slot:operator="{ data }">
              <lay-button size="xs" type="primary" @click="edit_consul(data)"
                >修改</lay-button
              >
            </template>
            <!-- <template v-slot:footer>
              {{ selectedKeys }}
            </template> -->
          </lay-table>
        </lay-card>
      </lay-col>
    </lay-row>
    <consuledit ref="editRef" @refresh="refresh" />
  </lay-container>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted } from "vue";
import { layer } from "@layui/layer-vue";
import { get_consul_list } from "../../api/module/consul";
import consuledit from "./components/edit.vue";

const editRef = ref(null);
const selectedKeys = ref(["1"]);
const checkbox = ref(true);
const defaultToolbars = [
  //   "filter",
  {
    icon: "layui-icon-refresh",
    title: "刷新",
    onClick: () => {
      console.log("导出");
    },
  },
  //   "export",
  //   "print"
];
const page = ref({ total: 100, limit: 10, current: 2 });
const dataSource = ref([]);
const refresh = () => {
  get_consul_list().then((data) => {
    // console.log(data);
    dataSource.value = data.data;
  });
};
onMounted(() => {
  refresh();
});

const edit_consul = (d: any) => {
  if (editRef.value) {
    editRef.value.show({
      title: "修改Consul配置",
      data: d,
      type: 1,
    });
  }
};

const addnew = () => {
  if (editRef.value) {
    editRef.value.show({
      title: "新增Consul配置",
      type: 0,
      data: {
        id: "0",
        enabled: true,
        key: "dc1",
        path: "",
        type: 1,
        name: "",
        remark: "",
        port: 8500,
        host: "",
      },
    });
  }
};

const columns = [
  {
    title: "ID",
    width: "100px",
    key: "id",
    fixed: "left",
    hide: true,
  },
  {
    title: "名称",
    width: "150px",
    key: "name",
  },
  {
    title: "IP地址",
    width: "150px",
    key: "host",
  },
  {
    title: "端口",
    width: "100px",
    key: "port",
  },
  {
    title: "路径",
    width: "200px",
    key: "path",
    ellipsisTooltip: true,
  },
  {
    title: "所属位置",
    width: "150px",
    key: "key",
  },
  {
    title: "类型",
    width: "120px",
    key: "type",
    customSlot: "type",
  },
  {
    title: "状态",
    width: "100px",
    key: "enabled",
    customSlot: "state",
  },
  {
    title: "备注",
    key: "remark",
    ellipsisTooltip: true,
  },
  {
    title: "操作",
    width: "180px",
    customSlot: "operator",
    key: "operator",
    fixed: "right",
  },
];

const rowClick = function (data: any) {};

const rowDoubleClick = function (data: any) {};

const change = function ({ current, limit }: any) {
  layer.msg("current:" + current + " limit:" + limit);
};
function toSearch() {
  layer.load(2, { time: 3000 });
}
const searchAccount = ref("");
const searchEmail = ref("");
function toReset() {
  searchAccount.value = "";
  searchEmail.value = "";
}
</script>
