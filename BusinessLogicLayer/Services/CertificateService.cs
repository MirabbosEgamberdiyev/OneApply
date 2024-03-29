﻿

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.CertificateDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class CertificateService(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                UserManager<User> userManager) : ICertificateService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;

    #region Certificate qo'shish 
    public async Task AddAsync(AddCertificateDto entity)
    {
       
        if (entity is null)
            throw new ArgumentNullException("CertificateDto is null");

        var certificate = _mapper.Map<Certificate>(entity);
        if(certificate is null)
        {
            throw new CustomException("Certificate is null");
        }
        var existingUser = await _userManager.FindByIdAsync(entity.UserId);
        if (existingUser != null)
        {

            certificate.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var certificates = await _unitOfWork.CertificateInterface.GetAllAsync();

        if (certificate.IsExistCertificate(certificates))
            throw new CustomException($"{certificate.Name} is already exist");
        if (!certificate.IsValidCertificate()){
            throw new CustomException("Invalid Certificate");
        }
        certificate.User = null;
        await _unitOfWork.CertificateInterface.AddAsync(certificate);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Certificateni o'chirish
    public async Task DeleteAsync(int id)
    {
        var certificate = await _unitOfWork.CertificateInterface.GetByIdAsync(id);
        if (certificate is null)
        {
            throw new ArgumentNullException($"{certificate.Name} is null");
        }
        await _unitOfWork.CertificateInterface.DeleteAsync(certificate);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Barcha  Certificatelarni olish 
    public async Task<List<CertificateDto>> GetAllAsync()
    {
        var certificates = await _unitOfWork.CertificateInterface.GetAllAsync();
        return _mapper.Map<List<CertificateDto>>(certificates);
    }
    #endregion

    #region Certifcateni  Idsi bilan olish
    public async Task<CertificateDto> GetByIdAsync(int id)
    {
        var certificate = await _unitOfWork.CertificateInterface.GetByIdAsync(id);
        return _mapper.Map<CertificateDto>(certificate);
    }
    #endregion

    #region Certificateni Update qilish
    public async Task UpdateAsync(UpdateCertificateDto entity)
    {

        if (entity is null)
            throw new ArgumentNullException("CertificateDto is null");

        var certificate = _mapper.Map<Certificate>(entity);
        if (certificate is null)
        {
            throw new CustomException("Certificate is null");
        }
        var existingUser = await _userManager.FindByIdAsync(entity.UserId);
        if (existingUser != null)
        {

            certificate.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var certificates = await _unitOfWork.CertificateInterface.GetAllAsync();

        if (certificate.IsExistCertificate(certificates))
            throw new CustomException($"{certificate.Name} is already exist");
        if (!certificate.IsValidCertificate())
        {
            throw new CustomException("Invalid Certificate");
        }
        certificate.User = null;
        await _unitOfWork.CertificateInterface.UpdateAsync(certificate);
        await _unitOfWork.SaveAsync();
    }
    #endregion

}
