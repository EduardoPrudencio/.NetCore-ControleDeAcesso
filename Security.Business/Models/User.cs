using System;

namespace Security.Business.Models
{
    public class User : Entity
    {
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime DatadeNascimento { get; set; }

        public string Name { get; set; }
    }
}
