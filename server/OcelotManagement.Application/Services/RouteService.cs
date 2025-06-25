using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class RouteService
    {
        // 这里假设我们有一个仓储接口用于获取数据
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<List<RouterListOutputDto>> GetRouteDtoList(string proj_id)
        {
            var routes = await _routeRepository.GetList(proj_id);
            var dtos = routes.Select(r => MapperConfig.Map<RouterListOutputDto>(r)).ToList();
            return dtos;
        }
        public async Task<RouterInfoDto> GetRouteDtoById(string id)
        {
            var route = await _routeRepository.GetRouteById(id);
            return MapperConfig.Map<RouterInfoDto>(route);
        }

        public async Task<string> AddRoute(RouterInfoDto inputDto)
        {
            var route = MapperConfig.Map<Route>(inputDto);
            route.Enabled = true;
            return await _routeRepository.AddRoute(route);
        }

        public async Task UpdateRoute(RouterInfoDto inputDto)
        {
            var route = MapperConfig.Map<Route>(inputDto);
            await _routeRepository.UpdateRoute(route);

        }

        public async Task<bool> DeleteRoute(string id)
        {
            return await _routeRepository.DeleteRoute(id);
        }


        public async Task<bool> UpdateRouteEnabled(string id, bool enabled)
        {
            await _routeRepository.UpdateRouteEnabled(id, enabled);
            return true;

        }

        //public async Task<List<ConfigRouteDto>> GetRoutes(string id)
        //{
        //    var data = await _routeRepository.GetAllRoutes(id);

        //    List<ConfigRouteDto> result = new List<ConfigRouteDto>();
        //    result.AddRange(data.Select(o =>
        //    {
        //        ConfigRouteDto r = null;
        //        r = MapperConfig.Map<ConfigRouteDto>(o);
        //        return r;
        //    }));
        //    return result;
        //}

   
    }

}
