using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using System.Security.Claims;
using TaskFlow.Application.UseCases.Projects.Create;

namespace TaskFlow.Web.Pages.Projects;

public class CreateModel : PageModel
{
    private readonly ICreateProjectUseCase _createProject;

    public CreateModel(ICreateProjectUseCase createProject)
    {
        _createProject = createProject;
    }

    [BindProperty]
    public ProjectCreateDto Input { get; set; } = new();
    private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var ownerId = _fakeOwnerId;

        var newId = await _createProject.HandleAsync(new CreateProjectCommand("", "", ownerId));
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
