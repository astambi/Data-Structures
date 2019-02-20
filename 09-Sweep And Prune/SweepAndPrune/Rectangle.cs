public class Rectangle : IRectangle
{
    private const int WidthConst = 10;
    private const int HeightConst = 10;

    public Rectangle(string name, int x1, int y1)
    {
        this.Name = name;
        this.X1 = x1;
        this.Y1 = y1;
    }

    public string Name { get; private set; }

    public int X1 { get; private set; }

    public int Y1 { get; private set; }

    public int X2 => this.X1 + WidthConst;

    public int Y2 => this.Y1 + HeightConst;

    public bool Intersects(IRectangle other)
        => this.X1 <= other.X2 && other.X1 <= this.X2
        && this.Y1 <= other.Y2 && other.Y1 <= this.Y2;

    public bool IsLeftOf(IRectangle other)
        => this.X2 < other.X1;

    public void Update(int newX1, int newY1)
    {
        this.X1 = newX1;
        this.Y1 = newY1;
    }

    public int CompareTo(IRectangle other)
        => this.X1.CompareTo(other.X1);

    public override string ToString()
        => $"{this.Name} ({this.X1}, {this.Y1}) - ({this.X2}, {this.Y2})";
}
