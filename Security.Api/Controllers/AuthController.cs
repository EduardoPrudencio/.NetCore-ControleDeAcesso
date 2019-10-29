using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Api.Extensions;
using Security.Api.ViewModel;
using Security.Business.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _singnManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSeettings;

        public AuthController(INotificador notificador, SignInManager<IdentityUser> singnManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSeettings) : base(notificador)
        {
            _singnManager = singnManager;
            _userManager = userManager;
            _appSeettings = appSeettings.Value;
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Register(UserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Name,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _singnManager.SignInAsync(user, false); //false para guardar dados do usuário
                return CustomResponse(registerUser);
            }

            foreach (var error in result.Errors)
                NotificarErro(error.Description);

            registerUser.Password = string.Empty;

            return CustomResponse(registerUser);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _singnManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(GetJasonWebToken());

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente por tentativas inválidas.");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(loginUser);

        }

        private string GetJasonWebToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSeettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSeettings.Emissor,
                Audience = _appSeettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSeettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }
}