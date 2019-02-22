using System;

public class Launcher
{
    public static void Main()
    {
        var enterprise = new Enterprise();
        var emp1 = new Employee("1 firstname", "1 Lastname", 1000, Position.Developer, new DateTime(2019, 2, 1));
        var emp2 = new Employee("2 firstname", "2 Lastname", 1000, Position.Developer, new DateTime(2019, 1, 1));
        var emp3 = new Employee("3 firstname", "3 Lastname", 1000, Position.Developer, new DateTime(2018, 2, 22));
        var emp4 = new Employee("4 firstname", "4 Lastname", 1000, Position.Developer, new DateTime(2018, 1, 1));

        enterprise.Add(emp1);
        enterprise.Add(emp2);
        enterprise.Add(emp3);
        enterprise.Add(emp4);

        enterprise.RaiseSalary(12, 10);


    }
}
