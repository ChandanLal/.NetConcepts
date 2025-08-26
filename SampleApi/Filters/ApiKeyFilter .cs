using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyFilter : ActionFilterAttribute
{
    private const string Apikey = "my-secret-key";
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var extractedKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "API Key is missing"
            };
            return;
        }

        if (!Apikey.Equals(extractedKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Invalid API Key"
            };
            return;
        }
        base.OnActionExecuting(context);
    }
}