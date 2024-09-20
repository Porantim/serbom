using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serbom.Domain;
using Serbom.Domain.Model;

namespace Serbom.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    { 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var contract = GetService().Get(id);
                return Ok(contract);
            } catch(Exception e) {
                return BadRequest(new ExceptionData(e.Message));
            }
        }

        [HttpGet("types")]
        public IActionResult ListTypes()
        {
            var types = GetService().ListTypes();
            return Ok(types);
        }

        [HttpGet("statuses")]
        public IActionResult ListStatuses()
        {
            var types = GetService().ListStatuses();
            return Ok(types);
        }

        [HttpGet()]
        public IActionResult List()
        {
            try {
                var contracts = GetService().List();
                return Ok(contracts);
            } catch(Exception e) {
                return BadRequest(new ExceptionData(e.Message));
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ContractData contractData)
        {
            try{
                ValidateData(contractData);
                GetService().Insert(contractData);
    
            } catch(Exception e) {
                return BadRequest(new ExceptionData(e.Message));
            }
            return Ok();
        }
    
        [HttpPut()]
        public IActionResult Update([FromBody] ContractData contractData)
        {
            try {
                ValidateData(contractData);
                GetService().Update(contractData);
                return Ok();
            } catch(Exception e) {
                return BadRequest(new ExceptionData(e.Message));
            }
        }

        private ContractService GetService()
        {
            return new ContractService(User.Identity?.Name);
        }

        private void ValidateData(ContractData contractData)
        {
            if (contractData.Client == null 
                || contractData.Contract.Number == null
                || contractData.Contract.Subject == null
                || contractData.Client.Name == null
                || contractData.Client.Type == null
                || contractData.Client.Email == null
                || contractData.Client.Phone == null
                || contractData.Client.ZipCode == null
                || contractData.Client.City == null
                || contractData.Client.Address1 == null
                || contractData.Client.Document == null
                )
            {
                throw new Exception("Invalid data");
            }
        }
    }
}
