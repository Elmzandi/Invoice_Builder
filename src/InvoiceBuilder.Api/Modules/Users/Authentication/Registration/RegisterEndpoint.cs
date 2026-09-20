using Microsoft.AspNetCore.Http.HttpResults;

namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/register",
            async Task<Results<
                Created<RegisterResponse>,
                Conflict<string>>> (
                RegisterRequest request,
                RegisterHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(
                    request,
                    cancellationToken);

                return result switch
                {
                    RegisterSuccess success =>
                        TypedResults.Created(
                            $"/users/{success.Response.Id}",
                            success.Response),

                    RegisterConflict conflict =>
                        TypedResults.Conflict(conflict.Message),

                    _ => throw new InvalidOperationException(
                        "Unknown registration result.")
                };
            });
    }

}
