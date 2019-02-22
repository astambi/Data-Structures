using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private readonly Dictionary<Guid, Employee> byGuid = new Dictionary<Guid, Employee>();

    public int Count => this.byGuid.Count;

    public void Add(Employee employee)
    {
        if (this.Contains(employee))
        {
            return;
        }

        this.byGuid[employee.Id] = employee;
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
        => this.byGuid.Values
        .Where(e => e.Position == position)
        .Where(e => minSalary <= e.Salary);

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.Contains(guid))
        {
            return false;
        }

        this.byGuid[guid] = employee;
        return true;
    }

    public bool Contains(Guid guid)
        => guid != null
        && this.byGuid.ContainsKey(guid);

    public bool Contains(Employee employee)
        => employee != null
        && employee.Id != null
        && this.byGuid.ContainsKey(employee.Id);

    public bool Fire(Guid guid)
    {
        if (!this.Contains(guid))
        {
            return false;
        }

        this.byGuid.Remove(guid);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (guid == null || !this.byGuid.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this.byGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        var result = this.byGuid.Values
            .Where(e => e.Position == position);

        if (!result.Any())
        {
            throw new ArgumentException(); // incomplete problem description
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = this.byGuid.Values
            .Where(e => minSalary <= e.Salary);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        var result = this.byGuid.Values
            .Where(e => e.Position == position)
            .Where(e => e.Salary == salary);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public Position PositionByGuid(Guid guid)
    {
        if (guid == null || !this.byGuid.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this.byGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var success = false;

        this.byGuid.Values
            .Where(e => e.HireDate.AddMonths(months) <= DateTime.Now)
            .ToList()
            .ForEach(e =>
            {
                e.Salary *= (1 + percent / 100.0);
                success = true;
            });

        return success;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
        => this.byGuid.Values
        .Where(e => e.FirstName == firstName);

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
        => this.byGuid.Values
        .Where(e => e.Position == position)
        .Where(e => e.FirstName == firstName)
        .Where(e => e.LastName == lastName);

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
        => this.byGuid.Values
        .Where(e => positions.Contains(e.Position));

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
        => this.byGuid.Values
        .Where(e => minSalary <= e.Salary && e.Salary <= maxSalary);

    public IEnumerator<Employee> GetEnumerator()
    {
        var employees = this.byGuid.Values;
        foreach (var employee in employees)
        {
            yield return employee;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
