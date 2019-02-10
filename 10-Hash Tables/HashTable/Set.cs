using System.Collections.Generic;
using System.Linq;

public class Set<TKey>
{
    private readonly HashTable<TKey, TKey> table = new HashTable<TKey, TKey>();

    public Set(IEnumerable<KeyValue<TKey, TKey>> enumerable = null)
    {
        if (enumerable == null)
        {
            return;
        }

        foreach (var kvp in enumerable)
        {
            this.table.AddOrReplace(kvp.Key, kvp.Key);
        }
    }

    public void Add(TKey key)
        => this.table.AddOrReplace(key, key);

    public bool Contains(TKey key)
        => this.table.ContainsKey(key);

    public Set<TKey> ExceptWith(Set<TKey> other)
    {
        var keys = this.table
            .Where(kvp => !other.Contains(kvp.Key));
        return new Set<TKey>(keys);
    }

    public Set<TKey> IntersectWith(Set<TKey> other)
    {
        var keys = this.table
            .Where(kvp => other.Contains(kvp.Key));
        return new Set<TKey>(keys);
    }

    public Set<TKey> UnionWith(Set<TKey> other)
    {
        var keys = this.table
            .Concat(other.table)
            .Distinct();
        return new Set<TKey>(keys);
    }

    public Set<TKey> SymmetricExceptWith(Set<TKey> other)
        => this
        .UnionWith(other)
        .ExceptWith(this.IntersectWith(other));

    public override string ToString()
        => string.Join(" ", this.table.Keys.OrderBy(x => x));
}
