﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Handlers.Accounts.Register.Member
{
    public class RegisterMemberCommand : IRequest<RegisterResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

    }
}
