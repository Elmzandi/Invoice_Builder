namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;
public abstract record RegisterResult;

public sealed record RegisterSuccess(
    RegisterResponse Response) : RegisterResult;

public sealed record RegisterConflict(
    string Message) : RegisterResult;
