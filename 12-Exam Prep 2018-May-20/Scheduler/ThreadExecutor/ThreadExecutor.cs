using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private readonly Dictionary<int, Task> byId = new Dictionary<int, Task>();
    private readonly List<Task> byInsertion = new List<Task>();
    private int totalCycles = 0;

    public ThreadExecutor()
    { }

    public int Count => this.byId.Count;

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var task = this.byId[id];
        task.TaskPriority = newPriority;
    }

    public bool Contains(Task task)
        => task != null
        && this.byId.ContainsKey(task.Id);

    public int Cycle(int cycles)
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        this.totalCycles += cycles;

        var tasksToRemove = new List<Task>();
        foreach (var task in this.byInsertion)
        {
            if (task.Consumption <= this.totalCycles)
            {
                tasksToRemove.Add(task);
            }
        }

        tasksToRemove.ForEach(t =>
        {
            this.byId.Remove(t.Id);
            this.byInsertion.Remove(t);
        });

        return tasksToRemove.Count;
    }

    public void Execute(Task task)
    {
        if (task == null || this.byId.ContainsKey(task.Id))
        {
            throw new ArgumentException();
        }

        this.byId[task.Id] = task;
        this.byInsertion.Add(task);
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        if (inclusive)
        {
            return this.byInsertion
                .Where(t => lo <= t.Consumption - this.totalCycles && t.Consumption - this.totalCycles <= hi)
                .OrderBy(t => t);
        }

        return this.byInsertion
            .Where(t => lo < t.Consumption - this.totalCycles && t.Consumption - this.totalCycles < hi)
            .OrderBy(t => t);
    }

    public Task GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.byId[id];
    }

    public Task GetByIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.byInsertion[index];
    }

    public IEnumerable<Task> GetByPriority(Priority type)
        => this.byInsertion
        .Where(t => t.TaskPriority == type)
        .OrderByDescending(t => t.Id);

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
        => this.byInsertion
        .Where(t => t.TaskPriority == priority)
        .Where(t => lo <= t.Consumption)
        .OrderByDescending(t => t.Id);

    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var task in this.byInsertion)
        {
            yield return task;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
