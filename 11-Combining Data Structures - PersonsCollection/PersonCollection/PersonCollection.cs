using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    //// O(1)
    private readonly Dictionary<string, Person> personsByEmail = new Dictionary<string, Person>();
    private readonly Dictionary<string, SortedSet<Person>> personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
    private readonly Dictionary<string, SortedSet<Person>> personsByNameAndTown = new Dictionary<string, SortedSet<Person>>();
    //// O(log n)
    //// .Net
    private readonly SortedDictionary<int, SortedSet<Person>> personsByAge = new SortedDictionary<int, SortedSet<Person>>();
    private readonly Dictionary<string, SortedDictionary<int, SortedSet<Person>>> personsByTownByAge = new Dictionary<string, SortedDictionary<int, SortedSet<Person>>>();
    ////Wintellect OrderedDictionary alternative
    //private readonly OrderedDictionary<int, SortedSet<Person>> personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
    //private readonly Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByTownByAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

    public int Count
        => this.personsByEmail.Count;

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        var person = new Person() { Email = email, Age = age, Name = name, Town = town };

        this.personsByEmail[email] = person;
        this.AddByEmailDomain(person);
        this.AddByNameAndTown(person);
        this.AddByAge(person);
        this.AddByTownAndAge(person);

        return true;
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }

        this.personsByEmail.Remove(email);
        this.RemoveByEmailDomain(person);
        this.RemoveByNameAndTown(person);
        this.RemoveByAge(person);
        this.RemoveByTownAndAge(person);

        return true;
    }

    public Person FindPerson(string email)
        => email != null
        && this.personsByEmail.ContainsKey(email)
        ? this.personsByEmail[email]
        : null;

    public IEnumerable<Person> FindPersons(string emailDomain)
        => emailDomain != null
        && this.personsByEmailDomain.ContainsKey(emailDomain)
        ? new List<Person>(this.personsByEmailDomain[emailDomain])
        : new List<Person>();

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameTown = this.GetNameTown(name, town);
        return nameTown != null
            && this.personsByNameAndTown.ContainsKey(nameTown)
            ? new List<Person>(this.personsByNameAndTown[nameTown])
            : new List<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var result = new List<Person>();

        //// .Net
        this.personsByAge
            .Keys
            .Where(age => startAge <= age && age <= endAge)
            .ToList()
            .ForEach(age => result.AddRange(this.personsByAge[age]));

        //// Wintellect OrderedDictionary
        //result = this.personsByAge
        //    .Range(startAge, true, endAge, true)
        //    .SelectMany(kvp => kvp.Value) // Person
        //    .ToList();

        return result.AsReadOnly();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        var result = new List<Person>();
        if (town == null
            || !this.personsByTownByAge.ContainsKey(town))
        {
            return result;
        }

        //// .Net
        this.personsByTownByAge[town]
            .Keys
            .Where(age => startAge <= age && age <= endAge)
            .ToList()
            .ForEach(age => result.AddRange(this.personsByTownByAge[town][age]));

        //// Wintellect OrderedDictionary
        //result = this.personsByTownByAge[town]
        //    .Range(startAge, true, endAge, true)
        //    .SelectMany(kvp => kvp.Value) // Person
        //    .ToList();

        return result.AsReadOnly();
    }

    private void AddByEmailDomain(Person person)
    {
        var emailDomain = this.GetEmailDomain(person.Email);
        if (emailDomain == null)
        {
            return;
        }

        if (!this.personsByEmailDomain.ContainsKey(emailDomain))
        {
            this.personsByEmailDomain[emailDomain] = new SortedSet<Person>();
        }

        this.personsByEmailDomain[emailDomain].Add(person);
    }

    private void AddByNameAndTown(Person person)
    {
        var nameTown = this.GetNameTown(person.Name, person.Town);
        if (nameTown == null)
        {
            return;
        }

        if (!this.personsByNameAndTown.ContainsKey(nameTown))
        {
            this.personsByNameAndTown[nameTown] = new SortedSet<Person>();
        }

        this.personsByNameAndTown[nameTown].Add(person);
    }

    private void AddByAge(Person person)
    {
        var age = person.Age;
        if (!this.personsByAge.ContainsKey(age))
        {
            this.personsByAge[age] = new SortedSet<Person>();
        }

        this.personsByAge[age].Add(person);
    }

    private void AddByTownAndAge(Person person)
    {
        var town = person.Town;
        var age = person.Age;
        if (town == null)
        {
            return;
        }

        if (!this.personsByTownByAge.ContainsKey(town))
        {
            this.personsByTownByAge[town] =
            new SortedDictionary<int, SortedSet<Person>>(); // .Net
            //new OrderedDictionary<int, SortedSet<Person>>(); // Wintellect OrderedDictionary
        }

        if (!this.personsByTownByAge[town].ContainsKey(age))
        {
            this.personsByTownByAge[town][age] = new SortedSet<Person>();
        }

        this.personsByTownByAge[town][age].Add(person);
    }

    private string GetEmailDomain(string email)
    {
        var tokens = email.Split('@');
        return tokens.Length > 1 ? tokens[1] : null;
    }

    private string GetNameTown(string name, string town)
        => $"{name}&{town}";

    private void RemoveByEmailDomain(Person person)
    {
        var emailDomain = this.GetEmailDomain(person.Email);
        if (emailDomain != null
            && this.personsByEmailDomain.ContainsKey(emailDomain))
        {
            this.personsByEmailDomain[emailDomain].Remove(person);
        }
    }

    private void RemoveByNameAndTown(Person person)
    {
        var nameTown = this.GetNameTown(person.Name, person.Town);
        if (nameTown != null
            && this.personsByNameAndTown.ContainsKey(nameTown))
        {
            this.personsByNameAndTown[nameTown].Remove(person);
        }
    }

    private void RemoveByAge(Person person)
    {
        var age = person.Age;
        if (this.personsByAge.ContainsKey(age))
        {
            this.personsByAge[age].Remove(person);
        }
    }

    private void RemoveByTownAndAge(Person person)
    {
        var town = person.Town;
        var age = person.Age;
        if (town != null
            && this.personsByTownByAge.ContainsKey(town)
            && this.personsByTownByAge[town].ContainsKey(age))
        {
            this.personsByTownByAge[town][age].Remove(person);
        }
    }
}
