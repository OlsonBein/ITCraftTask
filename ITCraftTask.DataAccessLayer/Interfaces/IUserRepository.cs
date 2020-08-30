using ITCraftTask.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITCraftTask.DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User applicationUser, string password);
        Task<User> GetByIdAsync(long id);
        Task<User> GetByNameAsync(string name);
        Task<List<User>> GetAllAsync();
        Task<bool> LogInAsync(User applicationUser, string password);
        Task LogOutAsync();
        Task<bool> CheckPasswordAsync(User applicationUser, string password);
    }
}
