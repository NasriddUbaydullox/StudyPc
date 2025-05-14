﻿//using System.Net;
//using AutoMapper;
//using HospitalManagment_V2.classes;
//using HospitalManagment_V2.DataAccess;
//using HospitalManagment_V2.DataAccess.Entities;
//using HospitalManagment_V2.Dtos;
//using HospitalManagment_V2.Examples;
//using HospitalManagment_V2.Repository;
//using HospitalManagment_V2.Services;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.RateLimiting;
//using Microsoft.Extensions.Logging;
//using Swashbuckle.AspNetCore.Annotations;
//using Swashbuckle.AspNetCore.Filters;

//namespace HospitalManagment_V2.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	//[EnableRateLimiting("fixed")]
//	public class TestController : ControllerBase
//	{
//		private readonly IDoctorRepository _doc;
//		private readonly IMediator _medatr;
//		private readonly ILogger<TestController> _logger;
//		private readonly ICorroletionId _corrId;
//		private readonly Context _context;
//		private readonly IMapper _mapper;
//		private readonly IPatientRepository _repository;
//		private readonly IPatientService _patientService;
//		private readonly IPatientRepository _patientRepos;

//		public TestController(ICorroletionId corroletionId,
//			ILogger<TestController> logger, Context context,
//			IMapper mapper, IPatientRepository patientRepository,
//			IPatientService patientService,
//			IPatientRepository patient,
//			IDoctorRepository doctor,
//			IMediator mediator)
//		{
//			_doc = doctor;
//			_logger = logger;
//			_corrId = corroletionId;
//			_context = context;
//			_mapper = mapper;
//			_repository = patientRepository;
//			_patientService = patientService;
//			_patientRepos = patient;
//			_medatr = mediator;
//		}

//		[HttpGet("test")]
//		public IActionResult TestCorrId()
//		{
//			_logger.LogInformation("CorreletionId {correletionId}", _corrId.Get());
//			var getSomething = _context.Doctors.ToList();
//			return Ok(getSomething);
//		}


//		[HttpGet]
//		[SwaggerOperation("Get all Doctors")]
//		[SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DoctorExample))]
//		[SwaggerResponse((int)HttpStatusCode.OK, "Get all menu", typeof(IList<DoctorDto>))]
//		[SwaggerResponse((int)HttpStatusCode.NotFound, "Menu not found")]
//		public async Task<IActionResult> GetAllDoctors()
//		{
//			return Ok(await _doc.GetAllAsync());
//		}

//		[HttpDelete]
//		[SwaggerOperation(
//	Summary = "Delete Doctor by Id",
//	Description = "Deletes an existing patient blank based on ID.",
//	OperationId = "DeletePatientBlank",
//	Tags = new[] { "Doctor" }
//)]
//		[SwaggerResponse(StatusCodes.Status204NoContent, "Deleted Successfully")]
//		[SwaggerResponse(StatusCodes.Status404NotFound, "PatientBlank not found")]
//		public async Task<IActionResult> DeleteDoctorById([FromRoute] int Id)
//		{
//			await _doc.DeleteAsync(Id);
//			return Ok();
//		}
//	}
//}
