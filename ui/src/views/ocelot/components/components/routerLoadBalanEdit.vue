<template>
  <lay-form :size="'md'" :labelWidth="125" isLabelTooltip>
    <lay-row>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="负载均衡类型">
          <lay-select
            v-model="routeStore.currentRouter.loadbalancerType"
            :allow-clear="true"
          >
            <lay-select-option
              :value="'LeastConnection'"
              label="最少连接数"
            ></lay-select-option>
            <lay-select-option
              :value="'RoundRobin'"
              label="轮询"
            ></lay-select-option>
            <lay-select-option
              :value="'NoLoadBalance'"
              label="不适用负载均衡"
            ></lay-select-option>
          </lay-select>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="负载均衡Key">
          <lay-input
            v-model="routeStore.currentRouter.loadbalancerKey"
            type="text"
            placeholder="请输入负载均衡Key"
          />
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="负载均衡Expiry">
          <lay-input
            v-model="routeStore.currentRouter.loadbalancerExpiry"
            type="text"
            placeholder="请输入负载均衡Expiry"
          />
        </lay-form-item>
      </lay-col>
    </lay-row>
  </lay-form>
</template>
<script lang="ts" setup>
import { ref, reactive } from "vue";
import { useRouterStore } from "../../../../store/ocelotRouter";
const routeStore = useRouterStore();
</script>
<style scoped>
</style>
