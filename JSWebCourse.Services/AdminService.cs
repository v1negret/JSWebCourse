using JSWebCourse.Data;
using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JSWebCourse.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;

    public AdminService(ApplicationDbContext db, UserManager<IdentityUser> userManager, IConfiguration config)
    {
        _db = db;
        _userManager = userManager;
        _config = config;
    }
    
    public async Task<AddAdminResult> AddAsync(IdentityUser user, string key)
    {
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return AddAdminResult.AlreadyInRole;
        }
        
        if (key.Equals(_config["Keys:AdminKey"]))
        {
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return AddAdminResult.Succeeded;
            }

            return AddAdminResult.Failed;
        }
        else
        {
            return AddAdminResult.WrongKey;
        }
    }

    public async Task<RemoveAdminResult> RemoveAsync(IdentityUser user)
    {
        if (!await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return RemoveAdminResult.UserIsNotAdmin;
        }
        var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
        
        return result.Succeeded ? RemoveAdminResult.Succeeded : RemoveAdminResult.Failed;
    }

    public async Task<List<GetUserDto>> GetAllAsync()
    {
        var request = await _userManager.GetUsersInRoleAsync("Admin");
        var result = new List<GetUserDto>();
        foreach (var user in request)
        {
            result.Add(new GetUserDto()
            {
                UserName = user.Email
            });
        }
        return result;
    }
}