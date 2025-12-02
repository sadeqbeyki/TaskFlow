using AutoMapper;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Mappers;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
    }

}