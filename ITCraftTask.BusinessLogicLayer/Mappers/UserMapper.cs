using ITCraftTask.BusinessLogicLayer.Models;
using ITCraftTask.DataAccessLayer.Entities;

namespace ITCraftTask.BusinessLogicLayer.Mappers
{
    public static class UserMapper
    {
        public static User MapModelToEntity(UserModel model)
        {
            var entity = new User()
            {
                Id = model.Id,
                Name = model.Name,
                UserName = model.Login
            };
            return entity;
        }

        public static UserModel MapEntityToModel(User user)
        {
            var model = new UserModel()
            {
                Id = user.Id,
                Login = user.UserName,
                Name = user.Name
            };
            return model;
        }
    }
}
