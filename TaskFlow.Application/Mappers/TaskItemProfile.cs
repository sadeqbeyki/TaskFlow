using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Filters;

namespace TaskFlow.Application.Mappers;

public class TaskItemProfile : Profile
{
    public TaskItemProfile()
    {
        CreateMap<TaskItem, TaskItemDto>().ReverseMap();

        CreateMap<TaskItem, TaskItemViewDto>()
            .ForMember(dest => dest.ProjectTitle,
               opt => opt.MapFrom(src => src.Project != null
                                          ? src.Project.Title
                                          : null));

        CreateMap<TaskItemDto, TaskItemFilter>().ReverseMap();

    }

}
