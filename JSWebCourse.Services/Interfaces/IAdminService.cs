using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace JSWebCourse.Services.Interfaces;

public interface IAdminService
{
    public Task<AddAdminResult> AddAsync(IdentityUser user, string key);
    public Task<RemoveAdminResult> RemoveAsync(IdentityUser user);
    public Task<List<GetUserDto>> GetAllAsync();
}