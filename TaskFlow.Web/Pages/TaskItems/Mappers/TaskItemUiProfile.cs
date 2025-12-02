using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems.Mappers;

public class TaskItemUiProfile : Profile
{
    public TaskItemUiProfile()
    {
        CreateMap<TaskItemDto, TaskItemInputModel>();
        CreateMap<TaskItemInputModel, TaskItemUpdateDto>();

        CreateMap<TaskItemDto, TaskItemViewModel>();

        CreateMap<TaskItemInputModel, TaskItemCreateDto>();
    }

}
