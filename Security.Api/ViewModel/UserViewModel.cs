﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Security.Api.ViewModel
{
    public class UserViewModel
    {
        public string Email { get; set; }

        //[Required(ErrorMessage = "Um login deve ser informado"), MinLength(5), MaxLength(15)]
        //public string Login { get; set; }

        [Required(ErrorMessage = "Uma senha deve ser informado"), MinLength(5), MaxLength(15)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Uma data de nascimento deve ser informada.")]
        public DateTime DatadeNascimento { get; set; }

        [Required(ErrorMessage = "O nome do usuário deve ser informado")]
        public string Name { get; set; }

        //usado para base 64
        //public string ImageUpload { get; set; } 

        //public IFormFile ImageUpload { get; set; }

        //public EnderecoViewModel Endereco { get; set; }
    }

    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
