using System;
using System.Collections.Generic;
using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Tasks.Response
{
    public class TaskResponse: IMapFrom<Domain.Entities.Task>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Domain.Entities.TaskStatus Status { get; set; }
        public Guid? CurrentUserAssignId { get; set; }
        public string? CurrentUserAssignName { get; set; }
        public DateTime DeadlineDate { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public List<TaskFile> Files { get; set; } = new List<TaskFile>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsOverdue { get; set; } = false;
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Task, TaskResponse>()
                .ForMember(dest => dest.CurrentUserAssignName, opt => opt
                    .MapFrom(src => src.CurrentUserAssign != null ? src.CurrentUserAssign.FullName : null))
                .ForMember(dest => dest.ProjectName, opt => opt
                    .MapFrom(src => src.Project != null ? src.Project.ProjectName : null))
                .ForMember(dest => dest.Files, opt => opt
                    .MapFrom(src => src.TaskFiles != null ? src.TaskFiles : new List<TaskFile>()));
        }
    }
} 