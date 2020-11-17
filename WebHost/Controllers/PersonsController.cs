using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebHost.Controllers
{
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly List<PersonDto> _people = new List<PersonDto>
        {
            new PersonDto{Id="1",FirstName = "Jean",MiddleName = "Gilbert",LastName = "Louis"},
            new PersonDto{Id="2",FirstName = "Andreas",LastName = "Knudsen"},
        };
        
        [Route("people")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonItemDto>))]
        public IActionResult GetPeople()
        {
            var payload = _people.Select(s => new PersonItemDto{Id=s.Id});
            return Ok(payload);
        }

        [Route("people/{id}")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult GetPerson(string id)
        {
            var payload = _people.FirstOrDefault(s => s.Id == id);
            if (payload == null) return NotFound("Not Found");
            return Ok(payload);
        }
    }
}