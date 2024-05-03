using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Response;
using Response;
using UserManagement.Controllers;

namespace HospitalManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class DeptController(IDepartment dept) : ControllerBase
    {
        //[Authorize(Roles = "Admin")]
        [HttpPost]

        public IActionResult CreateDepartment(DeptRequest deptRequest)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Create department
                var details = dept.CreateDept(deptRequest);

                // Check if department creation was successful
                if (details == null)
                {
                    return BadRequest();
                }

                var response = new ResponseModel<int>
                {
                    Message = "Department Created Successfully",
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
                return BadRequest();
            }
        }


        [HttpGet("int")]

        public IActionResult getByDeptId(int id)
        {
            try
            {
                var details = dept.getByDeptId(id);
                if (details == null)
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No Depsrtment found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<Department>
                {
                    Message = "Department retrieved successfully",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                return BadRequest( new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });

            }
        }

        [HttpGet("ByName")]
        public IActionResult getByDeptName(String name)
        {
            try
            {
                var details = dept.getByDeptName(name);

                if (details == null)
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No Depsrtment found",
                        Data = null
                    });
                }

                return Ok(new ResponseModel<Department>
                {
                    Message = "Department retrieved successfully",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentById(int Depid)
        {
            try
            {

                // Proceed with deleting the user account
                var details = await dept.DeleteDepartmentById(Depid);
                if (details > 0)
                {
                    return Ok(new ResponseModel<string>
                    {

                        Message = "Department deleted  successfully",
                        Data = null,

                    });
                }
                else
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Department not found",
                        Data = null
                    });
                }
            }
            catch (DatabaseException ex)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                });
            }

            }

        [HttpPut]
        public async Task<IActionResult> updateDepartment(int Depid, DeptRequest Department)
        {
            try
            {
        
                // Call the business logic layer to update the user's information
                var details = await dept.updateDepartment(Depid, Department);

                var response = new ResponseModel<int>
                {

                    Message = "Department updated successfully",
                    Data = details

                };

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return NotFound(response);
            }
            catch (DatabaseException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return BadRequest();
            }
            catch (RepositoryException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return BadRequest();
            }
            catch (Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred: " + ex.Message
                };
                return BadRequest();
            }
        }
    }
    }
