using System.Collections.Generic;

public interface IPersonCollection
{
    bool AddPerson(string email, string name, int age, string town);

    int Count { get; }

    bool DeletePerson(string email);

    Person FindPerson(string email);

    IEnumerable<Person> FindPersons(string emailDomain);

    IEnumerable<Person> FindPersons(string name, string town);

    IEnumerable<Person> FindPersons(int startAge, int endAge);

    IEnumerable<Person> FindPersons(int startAge, int endAge, string town);
}
