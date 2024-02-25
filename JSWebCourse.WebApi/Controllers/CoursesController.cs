using JSWebCourse.Models;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class CourseController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        private readonly IUnitService _unitService;
        public CourseController(IChapterService chapterService, IUnitService unitService)
        {
            _chapterService = chapterService;
            _unitService = unitService;
        }
        [HttpGet]
        [Route("get/titles")]
        public async Task<IActionResult> GetAllTitles()
        {
            var result = await _chapterService.GetAllNames();
            if (result.IsNullOrEmpty())
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("get/details")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _chapterService.GetAllChapters();
            if (result.IsNullOrEmpty())
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
