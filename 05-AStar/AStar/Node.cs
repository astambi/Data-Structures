using System;

public class Node : IComparable<Node>
{
    public Node(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public int Row { get; set; }

    public int Col { get; set; }

    // fCost = gCost + hCost
    // gCost = distance from current to start
    // hCost = distance from current to goal
    public int F { get; set; }

    public int CompareTo(Node other)
        => this.F.CompareTo(other.F);

    public override bool Equals(object obj)
    {
        var other = (Node)obj;
        return this.Col == other.Col
            && this.Row == other.Row;
    }

    public override int GetHashCode()
    {
        var hash = 17;
        hash = 31 * hash + this.Row.GetHashCode();
        hash = 31 * hash + this.Col.GetHashCode();
        return hash;
    }

    public override string ToString()
        => this.Row + " " + this.Col;
}
