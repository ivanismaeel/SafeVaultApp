using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Policy = "RequireAdminRole")]
public class DashboardModel : PageModel
{
    public void OnGet()
    {
    }
}
