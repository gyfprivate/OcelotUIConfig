import { defineStore } from 'pinia'
import { layer } from "@layui/layui-vue";

import {
    get_list_asyn
} from "../api/module/proj";
import { get_info, save_config, save_routes } from "../api/module/projInfo"
import { get_consul_list } from "../api/module/consul"
export const useProjStore = defineStore({
    id: 'proj',
    state: () => {
        return {
            currentProj: {} as any,
            projList: [] as any,
            consulList: [] as any,
        }
    },
    actions: {
        selectProj(item: any) {
            // console.log(item)
            if (this.currentProj.id && this.currentProj.id == item.id) return;

            get_info(item.id)
                .then(({ data }) => {
                    // if (data.projectId != this.currentRouterProj.id) {
                    //     layer.msg("路由不属于当前项目，请重新选择项目！", { icon: 5 });
                    //     return;
                    // }
                    this.currentProj = data
                })
                .catch((err) => {
                    // this.currentRouter = { projectId: this.currentRouterProj.id }
                })
        }
        ,
        // 保存当前项目的配置中心
        saveProjConfig(configId: string) {
            save_config(this.currentProj.id, configId).then((res) => {
                console.log(res)
                layer.msg(res.msg)
            }).catch((e) => {
                console.log(e)
            })

        },
        //保存当前项目的路由项目
        saveProjRoutes(routeKeys: string[]) {
            save_routes(this.currentProj.id, routeKeys).then((res) => {
                console.log(res)
                layer.msg(res.msg)
            }).catch((e) => {
                console.log(e)
            })
        },
        // addRouter() {
        //     this.currentRouter = { projectId: this.currentRouterProj.id }
        // },
        // 刷新路由列表
        refreshProjList() {
            this.projList = []
            get_list_asyn(1, 9999)
                .then(({ data }) => {
                    //   console.log(data.items);
                    this.projList = data.items;
                })
                .catch((err) => { });
        }
        ,

        refreshConsulList(action?: (data: any) => void) {
            //开始时拉取下拉数据
            get_consul_list()
                .then(({ data }) => {
                    this.consulList = data
                    action?.(data)
                })
                .catch((err) => { });
        }
        // onSaveRouter() {
        //     if (this.currentRouter.id) {
        //         console.log("编辑路由信息", this.currentRouter);
        //         // 编辑
        //         update_router(this.currentRouter)
        //             .then((res) => {
        //                 layer.msg("更新成功");
        //                 this.refreshRouterList(); // 刷新路由列表
        //             })
        //             .catch((err) => {
        //                 layer.msg("网络异常，请稍后再试");
        //             });
        //     } else {
        //         console.log("新增路由信息", this.currentRouter);
        //         // 新增
        //         add_router(this.currentRouter)
        //             .then((res) => {
        //                 console.log(res);
        //                 layer.msg("新增成功");
        //                 this.refreshRouterList(); // 刷新路由列表
        //             })
        //             .catch((err) => {
        //                 layer.msg("网络异常，请稍后再试");
        //             });
        //     }
        // }
    }

})