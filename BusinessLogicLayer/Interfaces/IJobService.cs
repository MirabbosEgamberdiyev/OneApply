﻿
using BusinessLogicLayer.Extended;
using DTOLayer.Dtos.VacanceDtos.JobDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IJobService
{
    Task<PagedList<JobDto>> Filter(FilterParametrs parametrs);
    Task<PagedList<JobDto>> GetAllPaged(int pageSize, int pageNumber);


    Task<List<JobDto>> GetAllAsync();
    Task<JobDto> GetByIdAsync(int id);
    Task AddAsync(AddJobDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateJobDto dto);
}
