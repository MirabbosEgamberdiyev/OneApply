
using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.LinkDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class LinkService(IUnitOfWork unitOfWork,
                         IMapper mapper,
                         UserManager<User> userManager) : ILinkService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;

    #region Add Link
    public async Task AddAsync(AddLinkDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("LinkDto is null");
        }

        var link = _mapper.Map<Link>(dto);

        if (link is null)
        {
            throw new ArgumentNullException("Mapped Link is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            link.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var links = await _unitOfWork.LinkInterface.GetAllAsync();

        if (!link.IsValid())
        {
            throw new CustomException("Invalid link");
        }

        if (link.IsExist(links))
        {
            throw new CustomException($"{link.Url} already exists");
        }

        link.User = null;
        await _unitOfWork.LinkInterface.AddAsync(link);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Delete Link
    public async Task DeleteAsync(int id)
    {
        var link = await _unitOfWork.LinkInterface.GetByIdAsync(id);

        if (link is null)
            throw new CustomException("Link is null");

        await _unitOfWork.LinkInterface.DeleteAsync(link);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Get All Links
    public async Task<List<LinkDto>> GetAllAsync()
    {
        var links = await _unitOfWork.LinkInterface.GetAllAsync();

        if (links is null)
            throw new CustomException("Links are null");

        return links.Select(c => _mapper.Map<LinkDto>(c)).ToList();
    }
    #endregion

    #region Get Link by Id
    public async Task<LinkDto> GetByIdAsync(int id)
    {
        var link = await _unitOfWork.LinkInterface.GetByIdAsync(id);

        if (link is null)
            throw new CustomException("Link is null");

        return _mapper.Map<LinkDto>(link);
    }
    #endregion

    #region Update Link
    public async Task UpdateAsync(UpdateLinkDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("LinkDto is null");
        }

        var link = _mapper.Map<Link>(dto);

        if (link is null)
        {
            throw new ArgumentNullException("Mapped Link is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            link.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var links = await _unitOfWork.LinkInterface.GetAllAsync();

        if (!link.IsValid())
        {
            throw new CustomException("Invalid link");
        }

        if (link.IsExist(links))
        {
            throw new CustomException($"{link.Url} already exists");
        }

        link.User = null;
        await _unitOfWork.LinkInterface.UpdateAsync(link);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}
