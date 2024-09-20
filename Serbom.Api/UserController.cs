using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serbom.Domain;
using Serbom.Domain.Model;

namespace Serbom.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Insert([FromBody] UserData userData)
        {
            var userDomain = GetService();
            
            try{
                if (userData.Name == null || userData.Email == null || userData.Password == null)
                {
                    return BadRequest("Invalid data");
                }

                userDomain.Insert(userData.Name, userData.Email, userData.Password);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        public class UserData {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        [HttpGet]
        public IActionResult List() 
        {
            try{
                var uList = GetService().List().Select(u => new {
                    u.Name,
                    u.Email,
                    u.Active
                });
                return Ok(uList);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        private UserService GetService()
        {
            return new UserService(User.Identity?.Name ?? String.Empty);
        }
    }

}
