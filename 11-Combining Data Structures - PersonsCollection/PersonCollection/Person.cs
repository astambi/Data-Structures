using System;

public class Person : IComparable<Person>
{
    public string Email { get; set; } // unique

    public string Name { get; set; }

    public int Age { get; set; }

    public string Town { get; set; }

    public int CompareTo(Person other)
        => this.Email.CompareTo(other.Email);
}
