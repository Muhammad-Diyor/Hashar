using Hashar.API.Data.Entities;
using Hashar.API.Models.Account;
using Hashar.API.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hashar.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> signInManager;
    private readonly UserManager<AppUser> userManager;
    private readonly IGenericRepository<AppUser> userRepository;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IGenericRepository<AppUser> userRepository)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.userRepository = userRepository;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpOrUpdateProfileDto dto)
    {
        var user = new AppUser()
        {
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

        var result = await userManager.CreateAsync(user, password:dto.Password);
        if (!result.Succeeded)
        {
            return BadRequest("Unable to create user");
        }

        await signInManager.PasswordSignInAsync(user.UserName, dto.Password, true, true);

        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, true, true);

        if (!result.Succeeded)
        {
            return BadRequest("pw xato");
        }

        return Ok("SignIn has completed successfully");
    }

    [HttpGet("/profile")]
    public async Task<AppUser> Profile() 
        => await userManager.GetUserAsync(User);

    [HttpDelete]
    public async Task LogOut() 
        => await signInManager.SignOutAsync();
}
