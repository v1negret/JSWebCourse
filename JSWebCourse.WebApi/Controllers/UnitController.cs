using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUnit([FromRoute] int id)
        {
            var response = await _unitService.GetUnitById(id);
            if (response.ServiceResult == ServiceResult.ServerError)
            {
                return StatusCode(500);
            }
            if (response.ServiceResult == ServiceResult.BadRequest)
            {
                return BadRequest();
            }
            var result = response.Unit;

            return Ok(result);
        }

        [HttpPost]
        [Route("add/")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddUnit([FromBody]AddUnitDto unit)
        {
            var result = await _unitService.AddUnitToChapter(unit);
            if (result == ServiceResult.ServerError)
            {
                return StatusCode(500);
            }
            if (result == ServiceResult.BadRequest)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch]
        [Route("update/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnit([FromBody] UpdateUnitDto unit)
        {
            var result = await _unitService.UpdateUnit(unit);

            if (result == ServiceResult.ServerError)
            {
                return StatusCode(500);
            }
            if (result == ServiceResult.BadRequest)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUnit([FromRoute]int id)
        {
            var result = await _unitService.RemoveUnitById(id);
            if (result == ServiceResult.ServerError)
            {
                return StatusCode(500);
            }
            if (result == ServiceResult.BadRequest)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
