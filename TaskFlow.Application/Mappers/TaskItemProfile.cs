using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Mappers;

public class TaskItemProfile : Profile
{
    public TaskItemProfile()
    {
        CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        CreateMap<TaskItem, TaskItemUpdateDto>().ReverseMap();

    }

}
