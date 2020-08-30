using ITCraftTask.DataAccessLayer.AppContext;
using ITCraftTask.DataAccessLayer.Entities;
using ITCraftTask.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCraftTask.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _dbContext;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(User applicationUser, string password)
        {
            var result = await _userManager.CreateAsync(applicationUser, password);
            return result.Succeeded;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<bool> LogInAsync(User applicationUser, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CheckPasswordAsync(User applicationUser, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
    
}
