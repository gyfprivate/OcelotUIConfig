using OcelotManagement.Contracts.Interfaces;
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
    internal class UserRepository : IUserRepository
    {
        private readonly Repository<tbUser> rep;
        private readonly IPasswordHasher passwordHasher;
        private readonly ISnowflake snow;

        public UserRepository(Repository<tbUser> rep, IPasswordHasher _passwordHasher, ISnowflake snow)
        {
            this.rep = rep;
            passwordHasher = _passwordHasher;
            this.snow = snow;
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            var data = await rep.AsQueryable().FirstAsync(u => u.Username == username);
            if (data == null)
                return null;
            return MapperConfig.Map<User>(data);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var data = await rep.AsQueryable()
                .FirstAsync(u => u.Id == id);
            return MapperConfig.Map<User>(data);

        }

        public async Task<bool> AddAsync(User user)
        {
            var data = MapperConfig.Map<tbUser>(user);
            data.Id = snow.GetSnowID();
            data.CreatedAt = DateTime.Now;
            // 生成密码哈希
            passwordHasher.CreatePasswordHash(user.PassWord, out byte[] passwordHash, out byte[] passwordSalt);
            data.PasswordHash = passwordHash;
            data.PasswordSalt = passwordSalt;

            return await rep.AsInsertable(data).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var data = MapperConfig.Map<tbUser>(user);
            return await rep.AsUpdateable(data)
                .IgnoreColumns(u => new { u.PasswordHash, u.PasswordSalt }) // 可选：忽略某些字段
                .ExecuteCommandAsync() > 0;
        }

        public async Task<User> VerifyPassword(string username, string password)
        {
            var data = await rep.AsQueryable().FirstAsync(u => u.Username == username);
            if (data == null) return null;

            var result = MapperConfig.Map<User>(data);
            var b = passwordHasher.VerifyPasswordHash(password, data.PasswordHash, data.PasswordSalt);
            if (b) result.PassWord = password;
            return result;

        }
    }
}
