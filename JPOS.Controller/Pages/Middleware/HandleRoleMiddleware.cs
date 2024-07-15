using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class HandleRoleMiddleware
{
    private readonly RequestDelegate _next;

    public HandleRoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        if (path.StartsWith("/loginpage") || path.StartsWith("/registerpage") || path.StartsWith("/forgetpasswordpage"))
        {
            await _next(context);
            return;
        }

        var userId = context.Session.GetString("UserId");
        var userName = context.Session.GetString("UserName");
        var email = context.Session.GetString("Email");
        var role = context.Session.GetString("Role");

        if (string.IsNullOrEmpty(role))
        {
            context.Response.Redirect("/LoginPage");
            return;
        }

        if (role == "1")
        {
            if (path.StartsWith("/dashboard") || path.StartsWith("/"))
            {
                await _next(context);
                return;
            }
        }

        else if (role == "6")
        {
            if (path.StartsWith("/"))
            {
                await _next(context);
                return;
            }
        }
        context.Response.Redirect("AccessDeniedPage");
        return;
    }
}
