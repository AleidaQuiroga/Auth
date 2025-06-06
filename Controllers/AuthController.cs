using Auth.Models.InModels;
using Auth.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Auth.Controllers
{
    public class AuthController : Controller
    {        
        private readonly IAuth_Service _authService;
        public AuthController(IAuth_Service authService)
        {
            _authService = authService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserIM addUserIM)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.AddUser(addUserIM);

                    if (result.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = result.IsSuccess,
                            Message = result.Message,
                            Data = result.Data,
                        });
                    }

                    return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = false,
                            Message = result.Message,
                            Data = result.Data,
                        });
                }

                return BadRequest("Algunas propiedades no son válidas.");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { IsSuccess = false, Message = "Error interno del servidor.", Data = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginIM loginIM)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.Login(loginIM);

                    if (result.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = result.IsSuccess,
                            Message = result.Message,
                            Data = result.Data,
                        });
                    }

                    return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false, Message = result.Message, Data = result.Data, });
                }

                return BadRequest("Algunas propiedades no son válidas.");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { IsSuccess = false, Message = "Error interno del servidor.", Data = ex.Message });
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()

        {
            try

            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.GetUsers();

                    if (result.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = result.IsSuccess,
                            Message = result.Message,
                            Data = result.Data,

                        });
                    }

                    return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false, Message = result.Message, Data = result.Data, });
                }

                return BadRequest("Algunas propiedades no son válidas.");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new { IsSuccess = false, Message = "Error interno del servidor.", Data = ex.Message });
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordIM changePasswordIM)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.ChangePassword(changePasswordIM);

                    if (result.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = result.IsSuccess,
                            Message = result.Message,
                            Data = result.Data,
                        });
                    }

                    return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false, Message = result.Message, Data = result.Data, });
                }

                return BadRequest("Algunas propiedades no son válidas.");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { IsSuccess = false, Message = "Error interno del servidor.", Data = ex.Message });
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserIM deleteUserIM)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.DeleteUser(deleteUserIM);

                    if (result.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            IsSuccess = result.IsSuccess,
                            Message = result.Message,
                            Data = result.Data,
                        });
                    }

                    return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false, Message = result.Message, Data = result.Data, });
                }

                return BadRequest("Algunas propiedades no son válidas.");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { IsSuccess = false, Message = "Error interno del servidor.", Data = ex.Message });
            }
        }
    }
}
