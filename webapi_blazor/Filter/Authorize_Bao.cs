using Microsoft.AspNetCore.Mvc.Filters;

public class Authorize_Bao : ActionFilterAttribute
{
    public Authorize_Bao()
    {
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        //Lấy thông tin token từ header => giải mã token => kiểm tra hợp lệ hay không
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }


}