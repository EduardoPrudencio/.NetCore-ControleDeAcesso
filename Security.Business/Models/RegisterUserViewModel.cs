using System.ComponentModel.DataAnnotations;

namespace Security.Business.Models
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "O campo {0} está em um fprmato inválido .")]

        public string Email { get; set; }

        public string SecurityId { get; set; }
    }
}
