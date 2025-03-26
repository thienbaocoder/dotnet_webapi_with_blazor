using Microsoft.AspNetCore.Mvc.Filters;

public class DemoFilter : ActionFilterAttribute
{
    public DemoFilter()
    {
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string? username = context.HttpContext.Request.Form["UserName"];
        // throw new NotImplementedException();
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // throw new NotImplementedException();
    }


}