﻿using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetChapter([FromRoute] int id)
        {
            var response = await _chapterService.GetChapterById(id);
            switch (response.ServiceResult)
            {
                case ServiceResult.ServerError:
                    return StatusCode(500);
                case ServiceResult.BadRequest:
                    return BadRequest();
                case ServiceResult.Success:
                {
                    var result = response.Chapter;

                    return Ok(result);
                }
                default:
                    return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("add/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddChapter([FromBody]AddChapterDto chapter)
        {
            var result = await _chapterService.AddChapter(chapter);
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

        [HttpPost]
        [Route("update/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateChapter([FromBody]UpdateChapterDto chapter)
        {
            var result = await _chapterService.UpdateChapter(chapter);
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
        public async Task<IActionResult> DeleteChapter([FromRoute]int id)
        {
            var result = await _chapterService.RemoveChapterById(id);
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
