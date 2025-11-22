using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems.Mappers;

public class TaskItemUiProfile:Profile
{
    public TaskItemUiProfile()
    {
        CreateMap<TaskItemDto, TaskItemInputModel>().ReverseMap();
        CreateMap<TaskItemDto, TaskItemViewModel>().ReverseMap();

        CreateMap<TaskItemUpdateDto, TaskItemInputModel>().ReverseMap();
    }

}
