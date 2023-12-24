

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.ProjectDtos;

namespace BusinessLogicLayer.Services;

public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : IProjectService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    #region Project qo'shish
    public async Task AddAsync(AddProjectDto dto)
    {
    
        if(dto is null)
        {
            throw new ArgumentNullException("Project is null here");
        }
        var project = _mapper.Map<Project>(dto);
        if (!project.IsValid())
        {
            throw new CustomException("Invalid project");
        }
        if (project is null)
        {
            throw new CustomException("Project is null here");
        }
        var projects = await _unitOfWork.ProjectInterface.GetAllAsync();
        if (projects is null)
        {
            throw new CustomException("Project is null here");
        }
        if (!project.IsExist(projects))
        {
            throw new CustomException($"{project.Name} is already exist");
        }
        await _unitOfWork.ProjectInterface.AddAsync(project);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Project o'chirish tugadi
    public async Task DeleteAsync(int id)
    {
        var project = await _unitOfWork.ProjectInterface.GetByIdAsync(id);
        if(project is null)
        {
            throw new CustomException("Project is not found");
        }
        await _unitOfWork.ProjectInterface.DeleteAsync(project);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Barcha projectlarni olish
    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _unitOfWork.ProjectInterface.GetAllAsync();
        if(projects is null)
        {
            throw new CustomException("Projects are null here");
        }
        return projects.Select(p => _mapper.Map<ProjectDto>(p)).ToList();
    }
    #endregion

    #region Projectni Id si bo'yicha olish
    public async Task<ProjectDto> GetByIdAsync(int id)
    {
        var project = await _unitOfWork.ProjectInterface.GetByIdAsync(id);
        if(project is null)
        {
            throw new CustomException("Project is not found");
        }
        return _mapper.Map<ProjectDto>(project);
    }
    #endregion

    #region Projectni update qilish
    public async Task UpdateAsync(UpdateProjectDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("Project is null here");
        }
        var project = _mapper.Map<Project>(dto);
        if (!project.IsValid())
        {
            throw new CustomException("Invalid project");
        }
        if (project is null)
        {
            throw new CustomException("Project is null here");
        }
        var projects = await _unitOfWork.ProjectInterface.GetAllAsync();
        if (projects is null)
        {
            throw new CustomException("Project is null here");
        }
        if (project.IsExist(projects))
        {
            throw new CustomException($"{project.Name} is already exist");
        }
        await _unitOfWork.ProjectInterface.UpdateAsync(project);
        await _unitOfWork.SaveAsync();


    }
    #endregion

}
