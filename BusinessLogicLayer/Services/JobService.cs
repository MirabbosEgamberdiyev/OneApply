using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Enums;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.VacanceDtos.JobDtos;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Reflection.Metadata;

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
        var jobs = await _unitOfWork.JobInterface.GetAllWithApplyAsync();
        var job = jobs.FirstOrDefault(j => j.Id==id);

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
        if (parametrs.Title is not "")
        {
            list = list.Where(book => book.Title.ToLower()
                .Contains(parametrs.Title.ToLower()));
        }

        list = list.Where(job => job.SalaryMax >= parametrs.MinPrice &&
                                          job.SalaryMin <= parametrs.MaxPrice);

        var dtos = list.Select(job => _mapper.Map<JobDto>(job)).ToList();
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

    public async Task<PagedList<JobDto>> GetAllPaged(int pageSize, int pageNumber)
    {
        var jobs = await GetAllAsync();
        PagedList<JobDto> pagedList = new(jobs, jobs.Count, pageNumber, pageSize);

        return pagedList.ToPagedList(jobs, pageSize, pageNumber);
    }

    public async Task<List<JobDto>> GetAllWithApplyAsync()
    {
        var jobs = await _unitOfWork.JobInterface.GetAllWithApplyAsync();

        if (jobs == null || !jobs.Any())
            throw new CustomException("No jobs found with applies");

        return jobs.Select(c => _mapper.Map<JobDto>(c)).ToList();
    }

    public async Task<List<JobDto>> GetByUserId(string UserId)
    {
        if (UserId == null) throw new ArgumentNullException("UserId is null");

        var jobs = await _unitOfWork.JobInterface.GetAllWithApplyAsync();

        var filteredJobs = jobs.Where(c => c.UserId == UserId).ToList();

        return filteredJobs.Select(c => _mapper.Map<JobDto>(c)).ToList();
    }


    public async Task<List<JobDto>> Filter(FilterJob filterJob)
    {
        var jobs = await _unitOfWork.JobInterface.GetAllAsync();

        if (!string.IsNullOrEmpty(filterJob.Title))
        {
            jobs = jobs.Where(job => job.Title.ToLower().Contains(filterJob.Title.ToLower()));
        }

        if (filterJob.EmploymentType > 0)
        {
            jobs = jobs.Where(job => job.EmploymentType == (EmploymentType)filterJob.EmploymentType);
        }

        if (!string.IsNullOrEmpty(filterJob.Location))
        {
            jobs = jobs.Where(job => job.Location.ToLower().Contains(filterJob.Location.ToLower()));
        }

        var dtos = jobs.Select(job => _mapper.Map<JobDto>(job)).ToList();
        return dtos;
    }
}
