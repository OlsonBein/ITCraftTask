using ITCraftTask.BusinessLogicLayer.Constants;
using ITCraftTask.BusinessLogicLayer.Interfaces;
using ITCraftTask.BusinessLogicLayer.Mappers;
using ITCraftTask.BusinessLogicLayer.Models;
using ITCraftTask.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITCraftTask.BusinessLogicLayer.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetByIdAsync(long id)
        {
            var response = new UserModel();
            var applicationUser = await _userRepository.GetByIdAsync(id);
            if (applicationUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            return UserMapper.MapEntityToModel(applicationUser);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var response = new UserModel();
            var applicationUsers = await _userRepository.GetAllAsync();
            var users = new List<UserModel>();
            foreach (var user in applicationUsers)
            {
                var userModel = UserMapper.MapEntityToModel(user);
                users.Add(userModel);
            }
            return users;
        }

        public async Task<UserModel> RegisterAsync(UserModel model)
        {
            var response = new UserModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var existingUser = await _userRepository.GetByNameAsync(model.Login);
            if (existingUser != null)
            {
                response.Errors.Add(ErrorConstants.UserWithTheSameNameExists);
                return response;
            }
            var applicationUser = UserMapper.MapModelToEntity(model);
            var result = await _userRepository.CreateAsync(applicationUser, model.Password);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateUser);
                return response;
            }
            return model;
        }

        public async Task<UserModel> LogInAsync(UserModel model, string password)
        {
            var response = new UserModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var existingUser = await _userRepository.GetByNameAsync(model.Login);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.IncorrectName);
                return response;
            }
            response = UserMapper.MapEntityToModel(existingUser);
            var result = await _userRepository.LogInAsync(existingUser, password);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.IncorrectPassword);
            }
            return response;
        }

        public async Task LogOutAsync()
        {
            await _userRepository.LogOutAsync();
        }

        public async Task<UserModel> GetUserByNameAsync(string login)
        {
            var response = new UserModel();
            var existingUser = await _userRepository.GetByNameAsync(login);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var userModel = UserMapper.MapEntityToModel(existingUser);
            return userModel;
        }

        public async Task<UserModel> GetUserByIdAsync(long id)
        {
            var response = new UserModel();
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var userModel = UserMapper.MapEntityToModel(existingUser);
            return userModel;
        }
    }
}
