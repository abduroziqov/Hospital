using Application.Services;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Models.DoctorDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _doctorService = doctorService;
    }

    [HttpPost]
    public async Task<ResponseModel<DoctorGetDTO>> CreateAsync(DoctorCreateDTO doctorCreateDTO)
    {
        var mappedDoctor = new Doctor
        {
            Specialization = doctorCreateDTO.Specialization,
            YearsOfExperience = doctorCreateDTO.YearsOfExperience,
            MedicalLicenseNumber = doctorCreateDTO.MedicalLicenseNumber,
            Gender = doctorCreateDTO.Gender,
            Age = doctorCreateDTO.Age,
            PhoneNumber = doctorCreateDTO.PhoneNumber,
            Name = doctorCreateDTO.Name,
            Surname = doctorCreateDTO.Surname
        };

        Doctor doctorEntity = await _doctorService.CreateAsync(mappedDoctor);

        var doctorDTO = new DoctorCreateDTO
        {
            Specialization = mappedDoctor.Specialization,
            YearsOfExperience = mappedDoctor.YearsOfExperience,
            MedicalLicenseNumber = mappedDoctor.MedicalLicenseNumber,
            Gender = mappedDoctor.Gender,
            Age = mappedDoctor.Age,
            PhoneNumber = mappedDoctor.PhoneNumber,
            Name = mappedDoctor.Name,
            Surname = mappedDoctor.Surname,
        };

        return new ResponseModel<DoctorGetDTO>(doctorDTO);
    }

    [HttpPost]
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
    //public async Task<IActionResult> Create(DoctorMediatr doctorMediatr)
    //{
    //    var request = new CreateMediatrService { DoctorMediatr = doctorMediatr };
    //    var createdDoctor = await _mediator.Send(request);

    //    return Ok(createdDoctor);
    //}
}