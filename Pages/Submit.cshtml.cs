using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SafeVaultApp.Services;

public class SubmitModel : PageModel
{
    private readonly DatabaseService _databaseService;

    public SubmitModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [BindProperty, Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [BindProperty, Required(ErrorMessage = "Email is required."), EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    public bool IsSubmitted { get; set; } = false;

    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            _databaseService.SaveUserData(Username, Email);
            IsSubmitted = true;
        }
    }
}
