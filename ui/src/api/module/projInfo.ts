import Http from '../http';
export const get_info = function (id: string) {
    return Http.get(`/api/SyncProjectInfo/${id}`)
}

export const save_config = function (projId: string, configId: string) {
    return Http.post(`/api/SyncProjectInfo/config`, { projId, configId })
}

export const save_routes = function (projId: string, routeIds: string[]) {
    return Http.post(`/api/SyncProjectInfo/routes`, { projId, routeIds })
}


