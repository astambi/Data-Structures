using System;

public class Program
{
    private const int count = 4;

    private class Product : IComparable<Product>
    {
        public Product(decimal price, string title = null)
        {
            this.Price = price;
            this.Title = title;
        }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
            => this.Price.CompareTo(other.Price);

        public override string ToString()
            => $"{this.Price} - {this.Title}";
    }

    public static void Main()
    {
        var products = new FirstLastList<Product>();
        products.Add(new Product(0.50m, "coffee"));
        products.Add(new Product(1.20m, "mint drops"));
        products.Add(new Product(1.20m, "beer"));
        products.Add(new Product(0.35m, "candy"));
        products.Add(new Product(1.20m, "cola"));

        Console.WriteLine("========================= Count");
        Console.WriteLine(products.Count);
        Console.WriteLine(string.Join(Environment.NewLine, products.First(products.Count)));
        Console.WriteLine("========================= First");
        Console.WriteLine(string.Join(Environment.NewLine, products.First(count)));
        Console.WriteLine("========================= Last");
        Console.WriteLine(string.Join(Environment.NewLine, products.Last(count)));
        Console.WriteLine("========================= Min");
        Console.WriteLine(string.Join(Environment.NewLine, products.Min(count)));
        Console.WriteLine("========================= Max");
        Console.WriteLine(string.Join(Environment.NewLine, products.Max(count)));
        Console.WriteLine("========================= Remove All @ Price 1.20");
        var product = new Product(1.20m);
        Console.WriteLine($"Removed {products.RemoveAll(product)}");
        Console.WriteLine(string.Join(Environment.NewLine, products.First(products.Count)));
        Console.WriteLine("========================= Clear");
        products.Clear();
        Console.WriteLine(products.Count);
    }
}
