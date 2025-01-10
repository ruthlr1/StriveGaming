using back_end.Models;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("[controller]")]
public class PasswordController : ControllerBase
{
    private readonly ILogger<PasswordController> _logger;
    private readonly IPasswordService _passwordService;
    
    public PasswordController(ILogger<PasswordController> logger, IPasswordService passwordService)
    {
        _logger = logger;
        _passwordService = passwordService;
    }

    [HttpPost("change")]
    public IActionResult SetPassword(PasswordChangeRequest request)
    {
        _logger.LogInformation("Received password change request");

        string validatePassword = _passwordService.ValidatePassword(request.Password);
        if (string.IsNullOrEmpty(validatePassword) && _passwordService.IsPasswordCommon(request.Password))
            validatePassword = "Password is too easy to guess";

        if (!string.IsNullOrEmpty(validatePassword))
        {
            return BadRequest(validatePassword);
        }

        return Ok();
    }
}