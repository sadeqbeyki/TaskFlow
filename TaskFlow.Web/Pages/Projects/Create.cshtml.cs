using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using System.Security.Claims;

namespace TaskFlow.Web.Pages.Projects;

public class CreateModel : PageModel
{
    private readonly IProjectService _projectService;

    public CreateModel(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [BindProperty]
    public ProjectCreateDto Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var ownerId = GetCurrentUserId();

        var newId = await _projectService.CreateAsync(Input, ownerId);
        TempData["Message"] = "Project created successfully.";
        return RedirectToPage("./Index");
    }

    private Guid GetCurrentUserId()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(id, out var guid))
            throw new InvalidOperationException("User not authenticated.");
        return guid;
    }
}
