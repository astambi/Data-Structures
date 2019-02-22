using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    private readonly List<Person> byInsertion = new List<Person>();
    private readonly Dictionary<string, List<Person>> byName = new Dictionary<string, List<Person>>();
    private readonly Dictionary<int, List<Person>> byNameLength = new Dictionary<int, List<Person>>();

    public int Count
        => this.byInsertion.Count;

    public void Add(Person person)
    {
        if (person == null)
        {
            return;
        }

        // By Insertion
        this.byInsertion.Add(person);

        // By Name
        if (!this.byName.ContainsKey(person.Name))
        {
            this.byName[person.Name] = new List<Person>();
        }
        this.byName[person.Name].Add(person);

        // By Name Length
        var nameLength = person.Name.Length;
        if (!this.byNameLength.ContainsKey(nameLength))
        {
            this.byNameLength[nameLength] = new List<Person>();
        }
        this.byNameLength[nameLength].Add(person);
    }

    public bool Contains(Person person)
    {
        if (person == null
            || !this.byName.ContainsKey(person.Name))
        {
            return false;
        }

        return this.byName[person.Name]
            .Any(p => p.Salary == person.Salary);
    }

    public bool ContainsByName(string name)
        => name != null
        && this.byName.ContainsKey(name);

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
        => this.byInsertion.Take(Math.Max(0, count));

    public Person GetAtIndex(int index)
    {
        if (index < 0 || this.Count < index)
        {
            throw new IndexOutOfRangeException();
        }

        return this.byInsertion[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (name == null
            || !this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        return this.byName[name];
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this.byNameLength.ContainsKey(length))
        {
            throw new ArgumentException();
        }

        return this.byNameLength[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
        => this.byInsertion;

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var result = new List<Person>();

        this.byNameLength.Keys
            .Where(l => minLength <= l && l <= maxLength)
            .ToList()
            .ForEach(l => result.AddRange(this.byNameLength[l]));

        return result;
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in this.byInsertion)
        {
            yield return person;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}