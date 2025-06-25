<template>
  <lay-form :size="'md'" :labelWidth="125" isLabelTooltip>
    <lay-row>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="身份验证关键字">
          <lay-input
            v-model="
              routeStore.currentRouter.authenticationAuthenticationproviderkey
            "
            type="text"
            placeholder="这个key对应的是代码中.AddJWTBreark中的Key"
          />
        </lay-form-item>
      </lay-col>

      <lay-col :md="12" :sm="24">
        <lay-form-item label="允许的作用域">
          <lay-tag-input
            v-model="authenticationAllowedscopes"
            v-model:inputValue="authenticationAllowedscopesInput"
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

const authenticationAllowedscopes = computed({
  get() {
    return stringToArray(routeStore.currentRouter.authenticationAllowedscopes);
  },
  set(value: any) {
    routeStore.currentRouter.authenticationAllowedscopes = arrayToString(value);
  },
});
const authenticationAllowedscopesInput = ref("");
</script>
<style scoped></style>
