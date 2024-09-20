using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serbom.Domain;
using Serbom.Domain.Model;

namespace Serbom.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        [HttpPost]
        public IActionResult Insert([FromBody] Client clientData)
        {
            try{
                if (clientData.Name == null || clientData.Type == null || clientData.Phone == null)
                {
                    return BadRequest("Invalid data");
                }

                GetService().Insert(clientData);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult List() 
        {
            try {
                var clients = GetService().List();
                return Ok(clients);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var client = GetService().Get(id);
                return Ok(client);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("contracts/{id}")]
        public IActionResult GetContracts(int id)
        {
            try {
                var contracts = GetService().GetContracts(id);
                return Ok(contracts);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("bydoc/{id}")]
        public IActionResult GetByDocument(string id)
        {
            try {
                var contracts = GetService().GetByDocument(id);
                return Ok(contracts);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Client clientData)
        {
            try {
                ValidateData(clientData);
                GetService().Update(clientData);
                return Ok();
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        private ClientService GetService()
        {
            return new ClientService(User.Identity?.Name);
        }

        private void ValidateData(Client clientData)
        {
            if (clientData.Name == null || clientData.Type == null || clientData.Phone == null)
            {
                throw new Exception("Invalid data");
            }
        }

    }
}
