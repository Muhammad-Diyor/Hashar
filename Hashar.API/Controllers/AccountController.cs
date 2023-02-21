using Hashar.API.Data.Entities;
using Hashar.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hashar.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> signInManager;

    public AccountController(SignInManager<AppUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    [HttpPost]
    public IActionResult SignUp(SignUpOrUpdateProfileDto dto)
    {
        throw new NotImplementedException();
    }
}
