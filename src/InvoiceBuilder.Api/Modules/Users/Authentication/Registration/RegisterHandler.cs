using InvoiceBuilder.Api.Shared.Database;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;

public sealed class RegisterHandler
{
    private readonly RegisterService _registerService;

    public RegisterHandler(RegisterService registerService)
    {
        _registerService = registerService;
    }

    public async Task<RegisterResult> HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        return await _registerService.RegisterAsync(request, cancellationToken);
    }
}