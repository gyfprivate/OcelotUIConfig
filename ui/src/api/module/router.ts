import Http from '../http';

export const get_router_list = function (projId: string) {
    return Http.get('/api/Route', { projId })
}

export const get_router_info = function (id: string) {
    return Http.get('/api/Route/' + id)
}

export const add_router = function (data: any) {
    return Http.post('/api/Route', data)
}

export const update_router = function (data: any) {
    return Http.put(`/api/Route/${data.id}`, data)
}

export const delete_router = function (id: string) {
    return Http.delete('/api/Route/' + id)
}
export const update_router_enabled = function (id: string, enabled: boolean) {
    return Http.patch(`/api/Route/${id}/enabled`, { enabled })
}
