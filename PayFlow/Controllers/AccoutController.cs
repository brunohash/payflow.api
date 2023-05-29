using PayFlow.Business;
using PayFlow.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Auth;

namespace PayFlow.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccoutBusiness _accoutBusiness;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AccoutBusiness accoutBusiness, ILogger<AccountController> logger)
    {
        _accoutBusiness = accoutBusiness;
        _logger = logger;
    }

    [HttpPost("v1/authenticate")]
    public async Task<IActionResult> Login([FromServices] TokenService tokenService, [FromBody] UserData credentials)
    {
        _logger.LogInformation("Iniciando autenticação.");

        try
        {
            if (string.IsNullOrEmpty(credentials.user) || string.IsNullOrEmpty(credentials.pass))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Preencha os campos informados!");
            }

            UserAuthenticate? authenticate = await _accoutBusiness.Authentication(credentials.user, credentials.pass);

            if (authenticate != null)
            {
                TokenBody token = await _accoutBusiness.GenerateToken(tokenService, authenticate);

                return Ok(token);
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Acesso não autorizado!");
            }
        }
        catch (Exception)
        {
            return BadRequest("Ops.. aconteceu algum problema, tente novamente mais tarde!");
        }
    }
}