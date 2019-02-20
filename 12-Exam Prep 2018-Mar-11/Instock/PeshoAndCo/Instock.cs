using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Instock : IProductStock
{
    private readonly List<Product> byInsertion = new List<Product>();
    private readonly Dictionary<string, Product> byLabel = new Dictionary<string, Product>();
    //// Better performance than SortedDictionry byLabel
    private readonly SortedSet<string> labels = new SortedSet<string>();
    //// SortedDictionary byPrice with no better performance
    private readonly Dictionary<double, HashSet<Product>> byPrice = new Dictionary<double, HashSet<Product>>();
    private readonly Dictionary<int, LinkedList<Product>> byQuantity = new Dictionary<int, LinkedList<Product>>();

    public int Count
        => this.byInsertion.Count;

    public void Add(Product product)
    {
        if (product == null || this.Contains(product))
        {
            return;
        }

        // By Insertion
        this.byInsertion.Add(product);

        // By Label
        this.byLabel[product.Label] = product;
        this.labels.Add(product.Label);

        // By Price
        if (!this.byPrice.ContainsKey(product.Price))
        {
            this.byPrice[product.Price] = new HashSet<Product>();
        }
        this.byPrice[product.Price].Add(product);

        // By Quantity
        if (!this.byQuantity.ContainsKey(product.Quantity))
        {
            this.byQuantity[product.Quantity] = new LinkedList<Product>();
        }
        this.byQuantity[product.Quantity].AddLast(product);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (product == null || !this.byLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var prod = this.byLabel[product];
        this.byQuantity[prod.Quantity].Remove(prod); // update order of insertion/change

        prod.Quantity = quantity; // NB!!! incorrect problem description
        if (prod.Quantity < 0)
        {
            prod.Quantity = 0;
        }

        // Update order of insertion/change
        if (!this.byQuantity.ContainsKey(prod.Quantity))
        {
            this.byQuantity[prod.Quantity] = new LinkedList<Product>();
        }
        this.byQuantity[prod.Quantity].AddLast(prod);
    }

    public bool Contains(Product product)
    {
        if (product == null || product.Label == null)
        {
            return false;
        }

        return this.byLabel.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (0 <= index && index < this.Count)
        {
            return this.byInsertion[index];
        }

        throw new IndexOutOfRangeException();
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        if (!this.byPrice.ContainsKey(price))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byPrice[price];
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (this.byQuantity.ContainsKey(quantity))
        {
            return this.byQuantity[quantity].ToList();
        }

        return Enumerable.Empty<Product>();
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        var result = new List<Product>();

        this.byPrice
            .Keys
            .Where(p => lo < p && p <= hi)
            .OrderByDescending(p => p)
            .ToList()
            .ForEach(p => result.AddRange(this.byPrice[p]));

        return result;
    }

    public Product FindByLabel(string label)
    {
        if (label == null || !this.byLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.byLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (0 <= count && count <= this.Count)
        {
            if (count == 0)
            {
                return Enumerable.Empty<Product>();
            }

            if (count == this.Count)
            {
                return this.byLabel.Values;
            }

            return this.labels
                .Take(count)
                .Select(l => this.byLabel[l]);
        }

        throw new ArgumentException();
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (0 <= count && count <= this.Count)
        {
            var result = new List<Product>();

            this.byPrice
                .Keys
                .OrderByDescending(p => p)
                .ToList()
                .ForEach(p => result.AddRange(this.byPrice[p]));

            return result.Take(count);
        }

        throw new ArgumentException();
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in this.byInsertion)
        {
            yield return product;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
