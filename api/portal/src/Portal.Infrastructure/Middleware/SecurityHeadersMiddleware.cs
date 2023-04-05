using Microsoft.AspNetCore.Http;

namespace Portal.Infrastructure.Middleware;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var headers = new Dictionary<string, string>
            {
                { "X-Frame-Options", "DENY" },
                { "X-XSS-Protection", "1; mode=block" },
                { "X-Content-Type-Options", "nosniff" },
                { "Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload" },
                { "X-Download-Options", "noopen" },
                { "Referrer-Policy", "no-referrer" },
                { "X-Permitted-Cross-Domain-Policies", "none" },
            };

        foreach (var header in headers.Where(header => !httpContext.Response.Headers.ContainsKey(header.Key)))
            httpContext.Response.Headers.Add(header.Key, header.Value);

        await _next(httpContext);
    }
}