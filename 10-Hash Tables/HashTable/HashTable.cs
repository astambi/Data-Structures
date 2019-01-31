using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots;

    public HashTable(int capacity = DefaultCapacity)
    {
        this.InitializeHashTable(capacity);
    }

    public int Count { get; private set; }

    public int Capacity
        => this.slots.Length;

    public void Add(TKey key, TValue value)
    {
        this.GrowIfNeeded();

        int index = this.FindIndex(key);
        this.InitializeElementsInSlot(index);

        // Throw an exception on duplicate key
        var elements = this.slots[index];
        foreach (var element in elements)
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException($"Key already exists: {key}");
            }
        }

        this.AddElement(key, value, index);
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.GrowIfNeeded();

        int index = this.FindIndex(key);
        this.InitializeElementsInSlot(index);

        // Replace element value 
        var elements = this.slots[index];
        foreach (var element in this.slots[index])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return false;
            }
        }

        this.AddElement(key, value, index);
        return true;
    }

    public TValue Get(TKey key)
    {
        var element = this.Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException();
        }

        return element.Value;
    }

    public TValue this[TKey key]
    {
        get => this.Get(key);
        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = this.Find(key);
        if (element != null)
        {
            value = element.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var index = this.FindIndex(key);
        var elements = this.slots[index];

        if (elements != null)
        {
            foreach (var element in elements)
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
        => this.Find(key) != null;

    public bool Remove(TKey key)
    {
        var index = this.FindIndex(key);
        var elements = this.slots[index];

        if (elements != null)
        {
            var current = elements.First;
            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    elements.Remove(current);
                    this.Count--;
                    return true;
                }

                current = current.Next;
            }
        }

        return false;
    }

    public void Clear()
        => this.InitializeHashTable();

    public IEnumerable<TKey> Keys
        => this.Select(e => e.Key);

    public IEnumerable<TValue> Values
        => this.Select(e => e.Value);

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var slot in this.slots)
        {
            if (slot != null)
            {
                foreach (var element in slot)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private void AddElement(TKey key, TValue value, int index)
    {
        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[index].AddLast(newElement);
        this.Count++;
    }

    private float FillFactor
        => (float)(this.Count + 1) / this.Capacity;

    private int FindIndex(TKey key)
        => Math.Abs(key.GetHashCode()) % this.Capacity;

    private void GrowIfNeeded()
    {
        if (this.FillFactor > LoadFactor)
        {
            this.Grow();
        }
    }

    private void Grow()
    {
        // Double capacity
        var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);

        // Add elements
        foreach (var element in this)
        {
            newHashTable.Add(element.Key, element.Value);
        }

        // Replace hash tables
        this.slots = newHashTable.slots;
        this.Count = newHashTable.Count;
    }

    private void InitializeHashTable(int capacity = DefaultCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    private void InitializeElementsInSlot(int index)
    {
        if (this.slots[index] == null)
        {
            this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }
    }
}
