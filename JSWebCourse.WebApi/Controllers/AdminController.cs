using JSWebCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JSWebCourse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                var httpContext = HttpContext;
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(403);
                }

                var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok();
        }
    }
}
