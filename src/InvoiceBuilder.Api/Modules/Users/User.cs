namespace InvoiceBuilder.Api.Modules.Users;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string  PasswordHash { get; private set; } = null!;
    public DateTime CreatedAt { get;  private set; } 
    public DateTime UpdatedAt { get;  private set; } 
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; } = false;

    private User()
    {
        // Parameterless constructor for EF Core
    }
    public User(string firstName, string lastName, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Cannot deactivate a deleted user.");
        }
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Activate()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Cannot activate a deleted user.");
        }
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    public void SoftDelete()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("User is already deleted.");
        }
        IsDeleted = true;
        IsActive = false; // deactivate the user when deleted
        UpdatedAt = DateTime.UtcNow;
    }
    public void Restore()
    {
        if (!IsDeleted)
        {
            throw new InvalidOperationException("User is not deleted.");
        }
        IsDeleted = false;
        IsActive = false; // restoring only undoes the delete; activation is a separate, explicit step
        UpdatedAt = DateTime.UtcNow;
    }
}