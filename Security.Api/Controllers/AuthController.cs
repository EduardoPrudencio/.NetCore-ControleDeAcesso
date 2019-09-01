using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.Api.ViewModel;
using Security.Business.Interfaces;
using System.Threading.Tasks;

namespace Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _singnManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotificador notificador, SignInManager<IdentityUser> singnManager, UserManager<IdentityUser> userManager) : base(notificador)
        {
            _singnManager = singnManager;
            _userManager = userManager;
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

            return CustomResponse(registerUser);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _singnManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(loginUser);

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente por tentativas inválidas.");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(loginUser);

        }
    }
}