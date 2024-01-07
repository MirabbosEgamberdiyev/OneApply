

using DataAcceseLayer.Entities.Resumes;
using DTOLayer.Dtos.CertificateDtos;
using AutoMapper;
using DTOLayer.Dtos.SkillDtos;
using DTOLayer.Dtos.ProjectDtos;
using DTOLayer.Dtos.EducationDtos;
using DTOAccessLayer.Dtos.LanguageDtos;
using DTOLayer.Dtos.LinkDtos;
using DTOLayer.Dtos.WorkExperienceDtos;
using DataAcceseLayer.Entities.Vacancies;
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;
using DTOLayer.Dtos.VacanceDtos.JobDtos;
using DataAcceseLayer.Entities;
using DTOLayer.Dtos.ApplicationUserDtos;

namespace DTOLayer.Mapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
             .ForMember(dest => dest.WorkExperienceDtos, opt => opt.MapFrom(src => src.WorkExperiences))
             .ForMember(dest => dest.CertificateDtos, opt => opt.MapFrom(src => src.Certificates))
             .ForMember(dest => dest.EducationDtos, opt => opt.MapFrom(src => src.Educations))
             .ForMember(dest => dest.LanguageDtos, opt => opt.MapFrom(src => src.Languages))
             .ForMember(dest => dest.LinkDtos, opt => opt.MapFrom(src => src.Links))
             .ForMember(dest => dest.ProjectDtos, opt => opt.MapFrom(src => src.Projects))
             .ForMember(dest => dest.SkillDtos, opt => opt.MapFrom(src => src.Skills))
             .ForMember(dest => dest.ApplyDtos, opt => opt.MapFrom(src => src.Applies))
             .ForMember(dest => dest.JobDtos, opt => opt.MapFrom(src => src.Jobs))
             .ReverseMap();

        CreateMap<AddUserDto, User>().ReverseMap();



        // Add Certificate 1
        CreateMap<Certificate, CertificateDto>().ReverseMap();
        CreateMap<AddCertificateDto, Certificate>();
        CreateMap<UpdateCertificateDto, Certificate>();

        // Add Skills 2
        CreateMap<Skill, SkillDto>().ReverseMap();
        CreateMap<AddSkillDto, Skill>();
        CreateMap<UpdateSkillDto, Skill>();

        //Add Projects 3
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<AddProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();

        //Add Education 4
        CreateMap<Education, EducationDto>().ReverseMap();  
        CreateMap<AddEducationDto, Education>();
        CreateMap<UpdateEducationDto, Education>();

        //Add Language 5
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<AddLanguageDto, Language>();
        CreateMap<UpdateLanguageDto, Language>();

        //Add Link 6
        CreateMap<Link, LinkDto>().ReverseMap();
        CreateMap<AddLinkDto, Link>();
        CreateMap<UpdateLinkDto, Link>();

        //Add WorkExperience 7
        CreateMap<WorkExperience, WorkExperienceDto>().ReverseMap();
        CreateMap<AddWorkExperienceDto, WorkExperience>();
        CreateMap<UpdateWorkExperienceDto, WorkExperience>();

        //Add apply 8
        CreateMap<ApplyDto, Apply>().ReverseMap();
        CreateMap<AddApplyDto, Apply>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        CreateMap<UpdateApplyDto, Apply>();

        //Add apply  9
        CreateMap<Job, JobDto>().ReverseMap()
       .ForMember(dest => dest.Applies, opt => opt.MapFrom(src => src.ApplyDtos));
        CreateMap<AddJobDto, Job>();
        CreateMap<UpdateJobDto, Job>();


    }
}
