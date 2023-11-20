using Application;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Models.DoctorDTO;
using Domain.States;
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

    public DoctorController(IDoctorService doctorService, IMediator mediator)
    {
        _mediator = mediator;
        _doctorService = doctorService;
    }

    [HttpPost]
    public async Task<ReponseModel<DoctorGetDTO>> Create(DoctorCreateDTO doctorCreateDTO)
    {
        var mappedDoctor = new Doctor
        {
            Name = doctorCreateDTO.Name,
            Surname = doctorCreateDTO.Surname
        };

        Doctor doctorEntity = await _doctorService.CreateAsync(mappedDoctor);

        var doctorDTO = new DoctorCreateDTO
        {
            Name = mappedDoctor.Name,
            Surname = mappedDoctor.Surname,
        };

        return new ReponseModel<DoctorGetDTO>(doctorDTO);
    }

}