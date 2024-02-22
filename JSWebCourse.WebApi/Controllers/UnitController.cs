using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        [Route("get/byChapterId/{id}")]
        public async Task<IActionResult> GetUnitsByChapterId([FromRoute]int id)
        {
            var result = await _unitService.GetUnitsByChapter(id);
            if(result.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetUnit([FromRoute] int id)
        {
            var response = await _unitService.GetUnitById(id);
            switch (response.ServiceResult)
            {
                case ServiceResult.ServerError:
                    return StatusCode(500);
                case ServiceResult.BadRequest:
                    return BadRequest();
                case ServiceResult.Success:
                {
                    var result = response.Unit;

                    return Ok(result);
                }
                default:
                    return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("add/")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddUnit([FromBody]AddUnitDto unit)
        {
            var result = await _unitService.AddUnitToChapter(unit);
            switch(result.Result)
            {
                case AddUnitServiceResult.ServerError:
                    return StatusCode(500);
                case AddUnitServiceResult.HtmlNotValid:
                    return BadRequest(result.Errors);
                case AddUnitServiceResult.BadRequest:
                    return BadRequest("Title or description or html string is empty");
                case AddUnitServiceResult.Success:
                    return Ok();
                default:
                    return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("update/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnit([FromBody] UpdateUnitDto unit)
        {
            var result = await _unitService.UpdateUnit(unit);

            switch (result)
            {
                case ServiceResult.ServerError:
                    return StatusCode(500);
                case ServiceResult.BadRequest:
                    return BadRequest("Unit title of description is empty");
                case ServiceResult.Success:
                    return Ok();
                default:
                    return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUnit([FromRoute]int id)
        {
            var result = await _unitService.RemoveUnitById(id);
            switch (result)
            {
                case ServiceResult.ServerError:
                    return StatusCode(500);
                case ServiceResult.BadRequest:
                    return BadRequest("Cannot find unit by id");
                case ServiceResult.Success:
                    return Ok();
                default:
                    return StatusCode(500);
            }
        }
    }
}
