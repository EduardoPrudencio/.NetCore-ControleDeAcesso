using System.ComponentModel.DataAnnotations;

namespace Security.Api.ViewModel
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Um login deve ser informado"), MinLength(5), MaxLength(15)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Uma senha deve ser informado"), MinLength(5), MaxLength(15)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Um email deve ser informado"), MinLength(5), MaxLength(15)]
        public string Email { get; set; }
    }

}
