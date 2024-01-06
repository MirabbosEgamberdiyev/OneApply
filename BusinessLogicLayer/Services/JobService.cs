using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.VacanceDtos.JobDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class JobService(IUnitOfWork unitOfWork,
                        IMapper mapper,
                        UserManager<User> userManager) : IJobService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;


    #region Add Job
    public async Task AddAsync(AddJobDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("JobDto is null");
        }

        var job = _mapper.Map<Job>(dto);

        if (job is null)
        {
            throw new ArgumentNullException("Mapped Job is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            job.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var jobs = await _unitOfWork.JobInterface.GetAllAsync();

        if (!job.IsValid())
        {
            throw new CustomException("Invalid job");
        }

        if (job.IsExist(jobs))
        {
            throw new CustomException($"{job.Title} already exists");
        }

        job.User = null;
        await _unitOfWork.JobInterface.AddAsync(job);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Delete Job
    public async Task DeleteAsync(int id)
    {
        var job = await _unitOfWork.JobInterface.GetByIdAsync(id);

        if (job is null)
            throw new CustomException("Job is null");

        await _unitOfWork.JobInterface.DeleteAsync(job);
        await _unitOfWork.SaveAsync();
    }

    #endregion

    #region Get All Jobs
    public async Task<List<JobDto>> GetAllAsync()
    {
        var jobs = await _unitOfWork.JobInterface.GetAllAsync();

        if (jobs is null)
            throw new CustomException("Jobs are null");

        return jobs.Select(c => _mapper.Map<JobDto>(c)).ToList();
    }
    #endregion

    #region Get Job by Id
    public async Task<JobDto> GetByIdAsync(int id)
    {
        var job = await _unitOfWork.JobInterface.GetByIdAsync(id);

        if (job is null)
            throw new CustomException("Job is null");

        return _mapper.Map<JobDto>(job);
    }
    #endregion

    #region Update Job
    public async Task UpdateAsync(UpdateJobDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto), "JobDto is null");
        }

        var job = _mapper.Map<Job>(dto);

        if (job is null)
        {
            throw new ArgumentNullException("Mapped Job is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            job.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var jobs = await _unitOfWork.JobInterface.GetAllAsync();

        if (!job.IsValid())
        {
            throw new CustomException("Invalid job");
        }

        if (job.IsExist(jobs))
        {
            throw new CustomException($"{job.Title} already exists");
        }

        job.User = null;
        await _unitOfWork.JobInterface.UpdateAsync(job);
        await _unitOfWork.SaveAsync();
    }
    #endregion


    public async Task<PagedList<JobDto>> Filter(FilterParametrs parametrs)
    {
        var list = await _unitOfWork.JobInterface.GetAllAsync();
        // Filter by title
        if (parametrs.Title is not "")
        {
            list = list.Where(book => book.Title.ToLower()
                .Contains(parametrs.Title.ToLower()));
        }

        // Filter by price
        list = list.Where(job => job.SalaryMax >= parametrs.MinPrice &&
                                          job.SalaryMin <= parametrs.MaxPrice);

        var dtos = list.Select(job => _mapper.Map<JobDto>(job)).ToList();
        // Order by title
        if (parametrs.OrderByTitle)
        {
            dtos = dtos.OrderBy(book => book.Title).ToList();
        }
        else
        {
            dtos = dtos.OrderByDescending(job => job.SalaryMin).ToList();
        }

        PagedList<JobDto> pagedList = new(dtos, dtos.Count,
                                          parametrs.PageNumber, parametrs.PageSize);

        return pagedList.ToPagedList(dtos, parametrs.PageSize, parametrs.PageNumber);
    }
}
