using ITCraftTask.BusinessLogicLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITCraftTask.BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetByIdAsync(long id);
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> RegisterAsync(UserModel model);
        Task<UserModel> LogInAsync(UserModel model, string password);
        Task LogOutAsync();
        Task<UserModel> GetUserByNameAsync(string login);
        Task<UserModel> GetUserByIdAsync(long id);
    }
}
