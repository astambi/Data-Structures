using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class ShoppingCenter : IShoppingCenter
{
    private readonly Dictionary<string, OrderedBag<Product>> byProducer = new Dictionary<string, OrderedBag<Product>>();
    private readonly Dictionary<string, OrderedBag<Product>> byName = new Dictionary<string, OrderedBag<Product>>();
    private readonly Dictionary<string, OrderedBag<Product>> byNameAndProducer = new Dictionary<string, OrderedBag<Product>>();
    private readonly SortedDictionary<decimal, OrderedBag<Product>> byPrice = new SortedDictionary<decimal, OrderedBag<Product>>();
    //private readonly OrderedDictionary<decimal, OrderedBag<Product>> byPrice = new OrderedDictionary<decimal, OrderedBag<Product>>();

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            return;
        }

        this.AddToDictionary(this.byName, product.Name, product);
        this.AddToDictionary(this.byProducer, product.Producer, product);
        this.AddToDictionary(this.byNameAndProducer, this.GetNameProducer(product.Name, product.Producer), product);
        this.AddToDictionary(this.byPrice, product.Price, product);
    }

    public int DeleteProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return 0;
        }

        var count = 0;
        this.byProducer[producer]
            .ToList()
            .ForEach(p =>
            {
                this.byProducer[producer].Remove(p);
                this.byName[p.Name].Remove(p);
                this.byNameAndProducer[this.GetNameProducer(p.Name, producer)].Remove(p);
                this.byPrice[p.Price].Remove(p);
                count++;
            });

        return count;
    }

    public int DeleteProductsByNameAndProducer(string name, string producer)
    {
        var nameProducer = this.GetNameProducer(name, producer);
        if (!this.byNameAndProducer.ContainsKey(nameProducer))
        {
            return 0;
        }

        var count = 0;
        this.byNameAndProducer[nameProducer]
            .ToList()
            .ForEach(p =>
            {
                this.byName[name].Remove(p);
                this.byProducer[producer].Remove(p);
                this.byNameAndProducer[nameProducer].Remove(p);
                this.byPrice[p.Price].Remove(p);
                count++;
            });

        return count;
    }

    public IEnumerable<Product> FindProductsByName(string name)
        => this.byName.ContainsKey(name)
        ? this.byName[name].ToList()
        : new List<Product>();

    public IEnumerable<Product> FindProductsByProducer(string producer)
        => this.byProducer.ContainsKey(producer)
        ? this.byProducer[producer].ToList()
        : new List<Product>();

    public IEnumerable<Product> FindProductsByPriceRange(decimal startPrice, decimal endPrice)
    {
        var result = new OrderedBag<Product>();

        this.byPrice
            .Keys
            .Where(price => startPrice <= price && price <= endPrice)
            .ToList()
            .ForEach(price => result.AddMany(this.byPrice[price]));

        //// Worse performance Ordered Dictionary
        //this.byPrice
        //    .Range(startPrice, true, endPrice, true)
        //    .Values
        //    .ToList()
        //    .ForEach(productList => result.AddMany(productList));

        return result;
    }

    private void AddToDictionary<T>(IDictionary<T, OrderedBag<Product>> dictionary, T key, Product product)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary[key] = new OrderedBag<Product>();
        }

        dictionary[key].Add(product);
    }

    private string GetNameProducer(string name, string producer)
        => $"{name}&&{producer}";
}
