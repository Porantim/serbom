using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Serbom.Domain;

namespace Serbom.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginData loginData)
        {
            var service = new UserService();
            
            try{
                var token = service.Authenticate(loginData.Email, loginData.Password);
                if(token == null) {
                    return Unauthorized(loginData);
                } else {
                    return Ok(new TokenData { Token = token });
                }
            } catch(System.Exception e) {
                return BadRequest(new ExceptionData(e.Message));
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult Renew()
        {
            try
            {
                var token = new TokenService(User.Identity?.Name ?? "").Renew();
                return Ok(new TokenData { Token = token });
            }
            catch (Exception e)
            {
                return Unauthorized(new ExceptionData(e.Message));
            }
        }

        public class LoginData {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        public class TokenData{
            public string? Token { get; set; }
        }
    }
}
