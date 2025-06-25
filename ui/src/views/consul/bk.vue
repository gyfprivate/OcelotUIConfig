<template>
  <lay-container fluid="true" style="padding: 10px">
    <lay-row :space="10">
      <lay-col :lg="24">
        <lay-card>
          <lay-form style="margin-top: 20px">
            <lay-row>
              <lay-col :lg="4">
                <lay-form-item label="项目：" label-width="50">
                  <lay-select
                    v-model="searchArg.projId"
                    placeholder="请选择"
                    label-width="50"
                    style="width: 90%"
                  >
                    <lay-select-option
                      v-for="item in projStore.projList"
                      :key="item"
                      :value="item.id"
                      :label="item.projectName"
                    ></lay-select-option>
                  </lay-select>
                  <!-- <lay-input
                    v-model="searchArg.proj"
                    style="width: 90%"
                  ></lay-input> -->
                </lay-form-item>
              </lay-col>
              <lay-col :lg="4">
                <lay-form-item label="Consul：" label-width="50">
                  <lay-select
                    v-model="searchArg.consulId"
                    placeholder="请选择"
                    label-width="50"
                    style="width: 90%"
                  >
                    <lay-select-option
                      v-for="item in projStore.consulList"
                      :key="item"
                      :value="item.id"
                      :label="item.name"
                    ></lay-select-option>
                  </lay-select>
                </lay-form-item>
              </lay-col>
              <lay-col :lg="12">
                <lay-form-item label="日期范围">
                  <lay-date-picker
                    v-model="searchArg.rangeTime"
                    range
                    :placeholder="['开始日期', '结束日期']"
                    :allow-clear="true"
                  ></lay-date-picker>
                </lay-form-item>
              </lay-col>
              <lay-col :lg="4">
                <lay-form-item label-width="0">
                  <lay-button type="primary" @click="onSearch">查询</lay-button>
                  <lay-button @click="onReset">重置</lay-button>
                </lay-form-item>
              </lay-col>
            </lay-row>
          </lay-form>
        </lay-card>
      </lay-col>
      <lay-col :md="24">
        <lay-card>
          <lay-table
            :page="page"
            :columns="columns"
            :dataSource="dataSource"
            @row="rowClick"
            @change="change"
          >
            <template v-slot:operator="{ row }">
              <lay-popconfirm
                content="确认回滚到此配置！"
                @confirm="onRollback(row.id)"
              >
                <lay-button type="primary">回滚</lay-button>
              </lay-popconfirm>
            </template>
            <template #content="{ row }">
              <lay-button @click="onShowContent(row)">查看Json</lay-button>
            </template>
            <template #consul="{ row }">
              <consul :data="row.consul" :border="true"></consul>
            </template>
          </lay-table>
        </lay-card>
      </lay-col>
    </lay-row>
  </lay-container>
  <lay-layer
    v-model="jsonContent.vis"
    :title="jsonContent.title"
    :shade="true"
    :area="['90%', '90%']"
  >
    <VueJsonPretty
      :data="jsonContent.content"
      :collapsedOnClickBrackets="true"
      :showSelectController="true"
    ></VueJsonPretty>
  </lay-layer>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted, reactive } from "vue";
import { layer } from "@layui/layer-vue";
import { useProjStore } from "../../store/proj";
import { get_setting_back } from "../../api/module/settingBack";
import { rollback_config } from "../../api/module/routeJson";
import { formatTimeNative } from "../../api/timeutil";
import consul from "../component/consulDescription.vue";
import VueJsonPretty from "vue-json-pretty";
import "vue-json-pretty/lib/styles.css";
const projStore = useProjStore();

const jsonContent = reactive({ vis: false, content: "", title: "" });
const searchArg: any = ref({});

const page = reactive({ total: 100, limit: 10, current: 1 });

onMounted(() => {
  projStore.refreshProjList();
  onSearch();
});

const onRollback = (id: string) => {
  console.log(id);
  rollback_config(id)
    .then((res) => {
      layer.msg("回滚完毕");
    })
    .catch((err) => {});
};

const onShowContent = (data: any) => {
  console.log(data);
  jsonContent.title = `项目【${data.projectName}】${formatTimeNative(
    data.backupTime
  )} 备份数据`;
  jsonContent.content = JSON.parse(data.backupContent);
  jsonContent.vis = true;
};

const columns = [
  {
    title: "备份时间",
    width: "150px",
    customSlot: ({ row }) => formatTimeNative(row.backupTime),
  },
  {
    title: "项目",
    width: "200px",
    key: "projectName",
  },
  {
    title: "Consul",
    customSlot: "consul",
  },
  {
    title: "备份内容",
    width: "180px",
    customSlot: "content",
  },
  {
    title: "备注",
    width: "180px",
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

const dataSource = ref([]);

const rowClick = function (data: any) {};

const rowDoubleClick = function (data: any) {};

const change = function ({ current, limit }: any) {
  layer.msg("current:" + current + " limit:" + limit);
};
function onSearch() {
  const arg = {
    ...searchArg.value,
    ...page,
  };
  // console.log(arg);
  get_setting_back(arg).then(({ data }) => {
    page.total = data.totalCount;
    dataSource.value = data.items;
  });

  // console.log();
  // layer.load(2, { time: 3000 });
}
function onReset() {
  searchArg.value = {};
}
</script>
