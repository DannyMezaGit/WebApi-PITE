using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
//using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnController : ControllerBase
    {
        private readonly IAlumnService _alumnService;

        public AlumnController(IAlumnService alumnService)
        {
            _alumnService = alumnService;

        }

        [HttpGet("")]
        public async Task<ActionResult<ICollection<ApiResponse>>> GetAllAlumnsAsync()
        {
            ApiResponse res = new ApiResponse();
            res.Success = 0;

            try
            {
                var alumns = await _alumnService.GetAllAlumnsAsync();

                if (alumns.Count() > 0)
                {
                    res.Success = 1;
                    res.Message = "Consulta realizada con éxito";
                    res.Data = alumns;

                    return Ok(res);
                }

                res.Message = "No se encontró ningún registro";
                return NotFound(res);
            }
            catch (Exception ex)
            {
                res.Message = $"{ex.Message}. {ex.InnerException.Message}";
                res.Data = ex.InnerException.Message.ToString();
                return BadRequest(res);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetAlumnByIdAsync(long id)
        {
            ApiResponse res = new ApiResponse();
            res.Success = 0;

            try
            {
                var alumn = await _alumnService.GetAlumnByIdAsync(id);

                if (alumn != null)
                {
                    res.Success = 1;
                    res.Message = "Consulta realizada con éxito";
                    res.Data = alumn;

                    return Ok(res);
                }

                res.Message = "No se encontró ningún registro";
                return NotFound(res);
            }
            catch (Exception ex)
            {
                res.Message = $"{ex.Message}. {ex.InnerException.Message}";
                res.Data = ex.InnerException.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<ApiResponse>> AddAlumnAsync(AlumnDTO model)
        {
            ApiResponse res = new ApiResponse();
            res.Success = 0;

            try
            {
                var alumn = await _alumnService.AddAlumnAsync(model);

                if (alumn != null)
                {
                    res.Success = 1;
                    res.Message = "El alumno se agregó con éxito";
                    res.Data = alumn;

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Message = $"{ex.Message}. {ex.InnerException.Message}";
                res.Data = ex.InnerException.Message.ToString();
            }
            
            return BadRequest(res);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateAlumnAsync(AlumnDTO model)
        {
            ApiResponse res = new ApiResponse();
            res.Success = 0;

            try
            {
                var alumn = await _alumnService.UpdateAlumnAsync(model);

                if (alumn != null)
                {
                    res.Success = 1;
                    res.Message = "Actualización realizada con éxito";
                    res.Data = alumn;

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Message = $"{ex.Message}. {ex.InnerException.Message}";
                res.Data = ex.InnerException.Message.ToString();
            }
            
            return BadRequest(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAlumnAsync(long id)
        {
            ApiResponse res = new ApiResponse();
            res.Success = 0;

            try
            {
                bool isDeleted = await _alumnService.DeleteAlumnAsync(id);

                if (isDeleted)
                {
                    res.Success = 1;
                    res.Message = "Alumno eliminado";
                    // res.Data = alumn;

                    return Ok(res);
                }

                res.Message = "No se encontró ningún registro";
                return NotFound(res);
            }
            catch (Exception ex)
            {
                res.Message = $"{ex.Message}. {ex.InnerException.Message}";
                res.Data = ex.InnerException.Message.ToString();
                return BadRequest(res);
            }
        }
    }
}