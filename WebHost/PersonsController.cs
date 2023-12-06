using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace WebHost;

[ApiController]
public class PersonsController : ControllerBase
{
    private readonly PersonService _service;

    public PersonsController(PersonService service)
    {
        _service = service;
    }

    [Route("people/{id}")]
    [HttpGet]
    public IActionResult GetPerson(string id)
    {
        var result = _service.GetPerson(id);
        if (result.IsFailed) return NotFound(result.Errors.First().Message);
        return Ok(result.Value);
    }
}

public abstract class Person
{
    protected Person(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public abstract string Type { get; }
}

public class EmployedPerson : Person
{
    public EmployedPerson(string id, string firstName, string lastName, string jobTitle) : base(id, firstName, lastName)
    {
        JobTitle = jobTitle;
    }

    public override string Type => "Employed";
    public string JobTitle { get; }
}

public class UnemployedPerson : Person
{
    public UnemployedPerson(string id, string firstName, string lastName, string reason, Instant since) : base(id,
        firstName, lastName)
    {
        Reason = reason;
        Since = since;
    }

    public override string Type => "Unemployed";
    public string Reason { get; }
    public Instant Since { get; }
}

public class PersonService
{
    private static readonly List<Person> Db = new()
    {
        new EmployedPerson("1", "Jean", "Louis", "Developer"),
        new UnemployedPerson("2", "Andreas", "Knudsen", "Fired", Instant.FromUtc(2022, 1, 1, 12, 0))
    };

    public Result<PersonDto> GetPerson(string id)
    {
        var person = Db.FirstOrDefault(s => s.Id == id);

        return person switch
        {
            EmployedPerson employedPerson => new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Type = person.Type,
                JobTitle = employedPerson.JobTitle
            },
            UnemployedPerson unemployedPerson => new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Type = person.Type,
                Reason = unemployedPerson.Reason,
                Since = unemployedPerson.Since.ToString(),
                SinceInstant = unemployedPerson.Since.ToDateTimeOffset()
            },
            _ => Result.Fail<PersonDto>("Not Found")
        };
    }
}