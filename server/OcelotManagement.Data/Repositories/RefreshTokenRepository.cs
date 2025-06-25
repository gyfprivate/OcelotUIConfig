using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly Repository<tbRefreshToken> rep;

        public RefreshTokenRepository(Repository<tbRefreshToken> rep)
        {
            this.rep = rep;
        }
        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            var data = await rep.AsQueryable()
                .FirstAsync(rt => rt.Token == token && !rt.IsRevoked);
            return MapperConfig.Map<RefreshToken>(data);
        }

        public async Task<bool> SaveAsync(RefreshToken refreshToken)
        {
            var data = MapperConfig.Map<tbRefreshToken>(refreshToken);
            return await rep.AsInsertable(data).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RefreshToken refreshToken)
        {
            var data = MapperConfig.Map<tbRefreshToken>(refreshToken);
            return await rep.AsUpdateable(data).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> RevokeByUsernameAsync(string username)
        {
            return await rep.AsUpdateable()
                .SetColumns(rt => new tbRefreshToken { IsRevoked = true })
                .Where(rt => rt.Username == username)
                .ExecuteCommandAsync() > 0;
        }
    }
}
