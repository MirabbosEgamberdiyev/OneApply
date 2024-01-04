

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
        CreateMap<User, UserDto>().ReverseMap();


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
        CreateMap<Apply, ApplyDto>().ReverseMap();
        CreateMap<AddApplyDto, Apply>();
        CreateMap<UpdateApplyDto, Apply>();

        //Update apply  9
        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<AddJobDto, Job>();
        CreateMap<UpdateJobDto, Job>();


    }
}
