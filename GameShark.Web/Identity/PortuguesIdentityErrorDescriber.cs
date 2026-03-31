using Microsoft.AspNetCore.Identity;

namespace GameShark.Web.Identity;

public class PortuguesIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordRequiresUpper() =>
        new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z')." };

    public override IdentityError PasswordRequiresLower() =>
        new IdentityError { Code = nameof(PasswordRequiresLower), Description = "A senha deve conter pelo menos uma letra minúscula ('a'-'z')." };

    public override IdentityError PasswordRequiresDigit() =>
        new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "A senha deve conter pelo menos um número ('0'-'9')." };

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve conter pelo menos um caractere especial (ex: @, #, $, etc)." };

    public override IdentityError PasswordTooShort(int length) =>
        new IdentityError { Code = nameof(PasswordTooShort), Description = $"A senha deve ter no mínimo {length} caracteres." };

    public override IdentityError DuplicateEmail(string email) =>
        new IdentityError { Code = nameof(DuplicateEmail), Description = $"O e-mail '{email}' já está sendo usado por outro player." };

    public override IdentityError DuplicateUserName(string userName) =>
        new IdentityError { Code = nameof(DuplicateUserName), Description = $"O login '{userName}' já está em uso." };
}