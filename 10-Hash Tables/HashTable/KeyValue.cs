using System;

public class KeyValue<TKey, TValue>
{
    public KeyValue(TKey key, TValue value)
    {
        this.Key = key;
        this.Value = value;
    }

    public TKey Key { get; set; }

    public TValue Value { get; set; }

    public override bool Equals(object other)
    {
        KeyValue<TKey, TValue> element = (KeyValue<TKey, TValue>)other;
        return Object.Equals(this.Key, element.Key)
            && Object.Equals(this.Value, element.Value);
    }

    public override int GetHashCode()
        => this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());

    public override string ToString()
        => $" [{this.Key} -> {this.Value}]";

    private int CombineHashCodes(int h1, int h2)
        => ((h1 << 5) + h1) ^ h2;
}
