using Microsoft.AspNetCore.Identity;

namespace InvoiceBuilder.Api.Modules.Users.Authentication;
public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();
    public string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
        return _hasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));
        
        var result = _hasher.VerifyHashedPassword(null!, passwordHash, password);
        return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}