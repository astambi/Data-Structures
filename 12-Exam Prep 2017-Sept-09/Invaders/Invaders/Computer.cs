using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private int energy;
    private List<Invader> invaders = new List<Invader>(); // by appearance

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }

        this.energy = energy;
    }

    public int Energy => this.energy;

    public void Skip(int turns)
    {
        this.invaders
            .ForEach(i =>
            {
                i.Distance -= turns;

                if (i.Distance <= 0 && this.energy >= i.Damage)
                {
                    this.energy -= i.Damage; // damage to computer => energy >= 0
                }
            });

        this.invaders = this.invaders
            .Where(i => i.Distance > 0) // remove destroyed invaders
            .ToList();
    }

    public void AddInvader(Invader invader)
    {
        if (invader == null) return;

        this.invaders.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        if (count <= 0) return;
        if (count >= this.invaders.Count)
        {
            this.invaders = new List<Invader>();
            return;
        }

        this.invaders = this.invaders
            .Except(this.invaders.OrderBy(x => x).Take(count)) // remove by priority
            .ToList();
    }

    public void DestroyTargetsInRadius(int radius)
    {
        if (radius <= 0) return;

        this.invaders = this.invaders
            .Where(i => i.Distance > radius)
            .ToList();
    }

    public IEnumerable<Invader> Invaders()
        => this.invaders;
}
