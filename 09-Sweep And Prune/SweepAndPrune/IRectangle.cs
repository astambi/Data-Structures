using System;

public interface IRectangle : IComparable<IRectangle>
{
    string Name { get; }

    int X1 { get; }

    int Y1 { get; }

    int X2 { get; }

    int Y2 { get; }

    bool Intersects(IRectangle other);

    bool IsLeftOf(IRectangle other);

    void Update(int newX1, int newY1);
}
