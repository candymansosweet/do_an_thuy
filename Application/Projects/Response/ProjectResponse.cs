using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Projects.Response
{
    public class ProjectResponse: IMapFrom<Project>
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Staff> Staffs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectResponse>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.FullName))
                .ForMember(dest => dest.Staffs, opt => opt.MapFrom(src => src.ProjectMems.Select(pm => pm.Staff).ToList()));
        }
    }
} 