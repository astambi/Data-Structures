using System;

public class Program
{
    public static void Main()
    {
        var stock = new Instock();

        var productA = new Product("A", 50, 100);
        var productC = new Product("C", 100, 10);
        var productB = new Product("B", 50, 3);

        stock.Add(productA);
        stock.Add(productC);
        stock.Add(productB);

        var contains = stock.Contains(productC);
        var find = stock.Find(0);
        stock.ChangeQuantity("C", -10);
        var byLabel = stock.FindByLabel("C");
        var firstCountByAscLabel = stock.FindFirstByAlphabeticalOrder(2);

        var byPriceRange = stock.FindAllInRange(30, 500);
        var byPrice = stock.FindAllByPrice(50);
        var firstMostExpensive = stock.FindFirstMostExpensiveProducts(2);



    }
}

