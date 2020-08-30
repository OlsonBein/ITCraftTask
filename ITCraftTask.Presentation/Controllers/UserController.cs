using ITCraftTask.BusinessLogicLayer.Interfaces;
using ITCraftTask.BusinessLogicLayer.Models;
using ITCraftTask.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCraftTask.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;

        public UserController(IUserService userService, IJwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == BusinessLogicLayer.Constants.JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        private void InsertTokens(TokensModel tokens)
        {
            Response.Cookies.Append(BusinessLogicLayer.Constants.JwtConstants.AccessToken, tokens.AccessToken);
            Response.Cookies.Append(BusinessLogicLayer.Constants.JwtConstants.RefreshToken, tokens.RefreshToken);
        }

        [HttpPost("registrate")]
        public async Task<IActionResult> RegistrateAsync(UserModel model)
        {
            var result = await _userService.RegisterAsync(model);
            if (model.Errors == null)
            {
                var tokens = _jwtHelper.GenerateTokens(result);
                InsertTokens(tokens);
            }
            
            return Ok(result);
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogInAsync(UserModel model)
        {
            var result = await _userService.LogInAsync(model, model.Password);
            if (result.Errors.Any())
            {
                return Ok(result);
            }
            var tokens = _jwtHelper.GenerateTokens(result);
            InsertTokens(tokens);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("logOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            await _userService.LogOutAsync();
            return Ok();
        }

        [HttpPost("refreshTokens")]
        public async Task<IActionResult> RefreshTokensAsync()
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == BusinessLogicLayer.Constants.JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();

            var userId = _jwtHelper.GetIdFromToken(token);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user.Errors.Any())
            {
                return Ok(user);
            }
            var newTokenPair = _jwtHelper.RefreshOldTokens(user, token);
            InsertTokens(newTokenPair);
            return Ok();
        }
    }
}
