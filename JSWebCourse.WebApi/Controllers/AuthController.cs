using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JSWebCourse.WebApi.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManagerManager;

    public AuthController(SignInManager<IdentityUser> signInManager)
    {
        _signInManagerManager = signInManager;
    }
    
    [Authorize]
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManagerManager.SignOutAsync();
        return Ok();
    }
}