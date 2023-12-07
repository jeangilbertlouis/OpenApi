using NodaTime;

namespace WebHost;

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