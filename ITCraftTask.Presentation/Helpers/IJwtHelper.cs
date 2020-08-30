using ITCraftTask.BusinessLogicLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITCraftTask.Presentation.Helpers
{
    public interface IJwtHelper
    {
        SecurityKey GetKey();
        TokensModel GenerateTokens(UserModel model);
        TokensModel RefreshOldTokens(UserModel model, string refreshToken);
        long GetIdFromToken(string token);
    }
}
