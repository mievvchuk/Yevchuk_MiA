using FluentValidation;
using LW4_Task6_MiA.DTO;

namespace LW4_Task6_MiA.Validators
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
