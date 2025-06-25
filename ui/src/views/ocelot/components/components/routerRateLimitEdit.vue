<template>
  <lay-form :size="'md'" :labelWidth="125" isLabelTooltip>
    <lay-row>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="是否启用流量限制">
          <lay-radio-button
            v-model="ratelimitEnableratelimiting"
            name="action"
            value="0"
            label="无"
          ></lay-radio-button>
          <lay-radio-button
            v-model="ratelimitEnableratelimiting"
            name="action"
            value="1"
            label="启用"
          ></lay-radio-button>
          <lay-radio-button
            v-model="ratelimitEnableratelimiting"
            name="action"
            value="2"
            label="禁用"
          ></lay-radio-button>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="限流周期">
          <lay-input
            v-model="routeStore.currentRouter.ratelimitPeriod"
            type="text"
            placeholder="限制期，如1s、1m、1h"
          />
        </lay-form-item>
      </lay-col>

      <lay-col :md="12" :sm="24">
        <lay-form-item label="限制期时间戳">
          <lay-input-number
            v-model="routeStore.currentRouter.ratelimitPeriodtimespan"
            type="text"
            placeholder=""
          >
          </lay-input-number>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="最大请求数" :allow-clear="true">
          <lay-input-number
            v-model="routeStore.currentRouter.ratelimitLimit"
            type="text"
            placeholder="客户端在定义的时间段内可以发出的最大请求数"
          >
          </lay-input-number>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="限流白名单">
          <lay-tag-input
            v-model="ratelimitWhitelist"
            v-model:inputValue="ratelimitWhitelistInput"
            allowClear
            placeholder="输入后按回车确认"
          ></lay-tag-input>
        </lay-form-item>
      </lay-col>
    </lay-row>
  </lay-form>
</template>
<script lang="ts" setup>
import { ref, computed } from "vue";
import {
  arrayToString,
  stringToArray,
  convertSwitchValue,
  reConvertSwitchValue,
} from "../../../../api/util";
import { useRouterStore } from "../../../../store/ocelotRouter";
const routeStore = useRouterStore();
const ratelimitEnableratelimiting = computed({
  get() {
    return reConvertSwitchValue(
      routeStore.currentRouter.ratelimitEnableratelimiting
    );
  },
  set(value: any) {
    routeStore.currentRouter.ratelimitEnableratelimiting =
      convertSwitchValue(value);
  },
});
const ratelimitWhitelist = computed({
  get() {
    return stringToArray(routeStore.currentRouter.ratelimitWhitelist);
  },
  set(value: any) {
    routeStore.currentRouter.ratelimitWhitelist = arrayToString(value);
  },
});
const ratelimitWhitelistInput = ref("");
</script>
<style scoped>
.lay-table-demo {
  width: 800px;
  height: 100px;
  overflow: auto;
  margin: 0;
}
</style>
