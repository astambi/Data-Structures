using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private readonly List<Person> persons = new List<Person>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        var person = new Person()
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        this.persons.Add(person);
        return true;
    }

    public int Count
        => this.persons.Count;

    public Person FindPerson(string email)
        => this.persons
        .FirstOrDefault(p => p.Email == email);

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }

        this.persons.Remove(person);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
        => this.persons
        .Where(p => p.Email.EndsWith($"@{emailDomain}"))
        .OrderBy(p => p.Email)
        .ToList();

    public IEnumerable<Person> FindPersons(string name, string town)
        => this.persons
        .Where(p => p.Name == name)
        .Where(p => p.Town == town)
        .OrderBy(p => p.Email)
        .ToList();

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
        => this.persons
        .Where(p => startAge <= p.Age && p.Age <= endAge)
        .OrderBy(p => p.Age)
        .ThenBy(p => p.Email)
        .ToList();

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        => this.persons
        .Where(p => p.Town == town)
        .Where(p => startAge <= p.Age && p.Age <= endAge)
        .OrderBy(p => p.Age)
        .ThenBy(p => p.Email)
        .ToList();
}
