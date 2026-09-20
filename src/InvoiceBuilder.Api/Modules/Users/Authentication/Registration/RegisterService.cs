using InvoiceBuilder.Api.Shared.Database;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;

public sealed class RegisterService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterService(AppDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
       var email = request.Email.Trim().ToLowerInvariant();

        // Check if the email is already registered
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == email && !user.IsDeleted, cancellationToken);

        if (existingUser is not null)
        {
            return new RegisterConflict("Email is already registered.");
        }

        // Hash the password
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Create a new user
        var newUser = new User(request.FirstName.Trim(), request.LastName.Trim(), email, passwordHash);

        // Add the user to the database
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Return the response
        return new RegisterSuccess(new RegisterResponse(newUser.Id, newUser.FirstName, newUser.LastName, newUser.Email));
    }
    
}