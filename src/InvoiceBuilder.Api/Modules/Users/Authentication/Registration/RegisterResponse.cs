namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;

public sealed record RegisterResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);
