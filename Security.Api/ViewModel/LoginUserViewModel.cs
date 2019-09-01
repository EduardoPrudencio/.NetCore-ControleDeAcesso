using System.ComponentModel.DataAnnotations;

namespace Security.Api.ViewModel
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O password deve ser informado"), MinLength(5), MaxLength(15)]
        public string Password { get; set; }


        [Required(ErrorMessage = "O nome do usuário deve ser informado"), MinLength(5), MaxLength(40)]
        public string UserName { get; set; }
    }

}
