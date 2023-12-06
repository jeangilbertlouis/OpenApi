using System;
using NodaTime;

namespace WebHost;

public class PersonDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type { get; set; }
    public string JobTitle { get; set; }
    public string Reason { get; set; }
    public string Since { get; set; }
    public DateTimeOffset SinceInstant { get; set; }
        
}