using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.Services;
using TaskFlow.Core.Repositories;
using TaskFlow.Infrastructure;
using TaskFlow.Infrastructure.Caching;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Web.Pages.TaskItems.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//caching
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IProjectTitleCache, ProjectTitleCache>();
//end caching
builder.Services.AddScoped<IProjectSummaryService, ProjectSummaryService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(TaskItemProfile), typeof(TaskItemUiProfile), typeof(ProjectProfile) /*, ...*/);
builder.Services.AddScoped<TaskItemMapper>();

// Register DbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
options.UseSqlServer(connectionString));


builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

// Repository & generic repo
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
