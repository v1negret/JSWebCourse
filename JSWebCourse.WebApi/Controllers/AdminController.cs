using JSWebCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public AdminController(IConfiguration configuration,RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;

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
            if(key.Text == _configuration["Keys:AdminKey"])
            {
                if(!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok();
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
    }
}
