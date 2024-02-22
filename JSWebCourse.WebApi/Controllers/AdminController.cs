using JSWebCourse.Models;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminService _adminService;
        public AdminController(IConfiguration configuration,RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IAdminService adminService)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _adminService = adminService;

        }
        [HttpPost]
        [Route("add/")]
        [Authorize]
        public async Task<IActionResult> AddAdmin([FromBody]Key key)
        {
            if(string.IsNullOrEmpty(key.Text))
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var result = await _adminService.AddAsync(user, key.Text);

            switch (result)
            {
                case(AddAdminResult.Succeeded):
                    return Ok();
                case(AddAdminResult.Failed):
                    return StatusCode(500, "Unknown error");
                case(AddAdminResult.WrongKey):
                    return BadRequest("Wrong key");
                case(AddAdminResult.AlreadyInRole):
                    return BadRequest("User is already in role");
                default:
                    return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("get/isAdmin")]
        [Authorize]
        public async Task<IActionResult> IsUserAdmin()
        {
            var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            if (await _userManager.IsInRoleAsync(user, "Admin")) 
            {
                return Ok(new IsUserAdminResult() { IsAdmin = true });
            }

            return Ok(new IsUserAdminResult() { IsAdmin = false });
        }

        [HttpGet]
        [Route("get/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var result = await _adminService.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Route("remove/{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin([FromRoute]string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var result = await _adminService.RemoveAsync(user);
            switch (result)
            {
                case(RemoveAdminResult.Succeeded):
                    return Ok();
                case(RemoveAdminResult.Failed):
                    return StatusCode(500);
                case(RemoveAdminResult.UserIsNotAdmin):
                    return BadRequest("User in not admin");
                default:
                    return StatusCode(500);
            }
        }
    }
}
