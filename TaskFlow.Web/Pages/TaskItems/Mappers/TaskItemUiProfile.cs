using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems.Mappers;

public class TaskItemUiProfile : Profile
{
    public TaskItemUiProfile()
    {
        CreateMap<TaskItemViewDto, TaskItemInputModel>();
        CreateMap<TaskItemInputModel, TaskItemUpdateDto>();

        CreateMap<TaskItemInputModel, TaskItemCreateDto>();

        CreateMap<TaskItemViewDto, TaskItemViewModel>();
    }

}
