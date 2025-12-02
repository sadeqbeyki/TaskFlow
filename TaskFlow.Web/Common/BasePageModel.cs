using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace TaskFlow.Web.Common;

public abstract class BasePageModel : PageModel
{
    //private readonly ICurrentUserService _currentUser;

    //protected BasePageModel(ICurrentUserService currentUser)
    //{
    //    _currentUser = currentUser;
    //}
    //_currentUser.UserId
    //protected Guid OwnerId => User.GetCurrentUserId();

    protected Guid OwnerId => Guid.Parse("11111111-1111-1111-1111-111111111111");


    protected void SetSuccess(string message)
    {
        TempData["Message"] = message;
        TempData["MessageType"] = "success";
    }

    protected void SetError(string message)
    {
        TempData["Message"] = message;
        TempData["MessageType"] = "error";
    }

    private Guid GetCurrentUserId()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(id, out var guid))
            throw new InvalidOperationException("User not authenticated.");
        return guid;
    }
}
