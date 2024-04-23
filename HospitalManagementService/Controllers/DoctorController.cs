using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Response;
using Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _service;

        public DoctorController(IDoctor service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateDoctor(DoctorRequest request)
        {
            try
            {
                var details = _service.CreateDoctor(request);
                if (details == null)
                {
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                var response = new ResponseModel<int>
                {
                    Message = "Doctor Created Successfully",
                    Data = (int)details
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet("{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            try
            {
                var details = _service.GetDoctorById(doctorId);
                if (details == null)
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No Doctor found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<Doctor>
                {
                    Message = "Doctor retrieved successfully",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            try
            {
                var details = _service.GetAllDoctors();
                if (details == null || !details.Any())
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No Doctors found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<IEnumerable<Doctor>>
                {
                    Message = "Doctors retrieved successfully",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{doctorId}")]
        public IActionResult UpdateDoctor(int doctorId, DoctorRequest request)
        {
            try
            {
                var details = _service.UpdateDoctor(doctorId, request);
                if (details == null)
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Doctor not found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<int>
                {
                    Message = "Doctor updated successfully",
                    Data = (int)details
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{doctorId}")]
        public IActionResult DeleteDoctor(int doctorId)
        {
            try
            {
                var details = _service.DeleteDoctor(doctorId);
                if (details > 0)
                {
                    return Ok(new ResponseModel<string>
                    {
                        Message = "Doctor deleted successfully",
                        Data = null
                    });
                }

                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Doctor not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("specialization/{specialization}")]
        public IActionResult GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                var details = _service.GetDoctorsBySpecialization(specialization);
                if (details == null || !details.Any())
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No Doctors found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<IEnumerable<Doctor>>
                {
                    Message = "Doctors retrieved successfully",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}
