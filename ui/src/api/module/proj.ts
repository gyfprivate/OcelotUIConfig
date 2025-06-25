import Http from '../http';
export const get_list_asyn = function (current: number, size: number) {
    return Http.get('/api/SynchronizationProject', { currentpage: current, pagesize: size })
}

export const get_info_asyn = function (id: string) {
    return Http.get(`/api/SynchronizationProject/${id}`)
}

export const add_asyn = function (data: any) {
    return Http.post('/api/SynchronizationProject', data)
}


export const edit_asyn = function (id: string, data: any) {
    return Http.put(`/api/SynchronizationProject/${id}`, data)
}



export const get_info_router = function (id: string) {
    return Http.get(`/api/RoutingProject/${id}`)
}

export const get_list_router = function (current: number, size: number) {
    return Http.get('/api/RoutingProject', { currentpage: current, pagesize: size })
}


export const add_router = function (data: any) {
    return Http.post('/api/RoutingProject', data)
}


export const edit_router = function (id: string, data: any) {
    return Http.put(`/api/RoutingProject/${id}`, data)
}
