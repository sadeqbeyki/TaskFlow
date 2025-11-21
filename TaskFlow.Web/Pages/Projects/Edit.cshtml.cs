using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Web.Pages.Projects;

public class EditModel : PageModel
{
    private readonly IProjectService _projectService;

    private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

    [BindProperty]
    public ProjectUpdateDto ProjectModel { get; set; } = new();

    public Guid Id { get; set; }

    public EditModel(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Id = id;

        var project = await _projectService.GetByIdAsync(id, _fakeOwnerId);
        if (project == null)
            return NotFound();

        ProjectModel = new ProjectUpdateDto
        {
            Title = project.Title,
            Description = project.Description
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return Page();

        var updated = await _projectService.UpdateAsync(id, ProjectModel, _fakeOwnerId);

        if (!updated)
            return NotFound();

        return RedirectToPage("Index");
    }
}
