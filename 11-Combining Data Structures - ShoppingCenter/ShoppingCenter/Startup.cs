using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var shoppingCenter = new ShoppingCenter();

        var linesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < linesCount; i++)
        {
            var line = Console.ReadLine(); // {command} {arg1};{arg2};{...}
            var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 2) // {command} {args}
            {
                continue;
            }

            var command = tokens[0];
            var args = GetCommandArgs(line);

            switch (command)
            {
                case "AddProduct": AddProduct(shoppingCenter, args); break;
                case "DeleteProducts": DeleteProducts(shoppingCenter, args); break;
                case "FindProductsByName": FindProductsByName(shoppingCenter, args); break;
                case "FindProductsByProducer": FindProductsByProducer(shoppingCenter, args); break;
                case "FindProductsByPriceRange": FindProductsByPriceRange(shoppingCenter, args); break;
                default: break;
            }
        }
    }

    private static string[] GetCommandArgs(string line)
    {
        var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        // Strip command from arg1: {command} {arg1} => {arg1}
        var firstArg = args[0];
        args[0] = firstArg.Substring(firstArg.IndexOf(' ') + 1).TrimStart();
        return args;
    }

    private static void AddProduct(ShoppingCenter shoppingCenter, string[] args)
    {
        if (args.Length < 3) return;

        var name = args[0];
        var price = decimal.Parse(args[1]);
        var producer = args[2];
        var product = new Product(name, price, producer);
        shoppingCenter.AddProduct(product);
        Console.WriteLine("Product added");
    }

    private static void DeleteProducts(ShoppingCenter shoppingCenter, string[] args)
    {
        if (!args.Any()) return;

        var count = 0;
        if (args.Length > 1)
        {
            var name = args[0];
            var producer = args[1];
            count = shoppingCenter.DeleteProductsByNameAndProducer(name, producer);
        }
        else
        {
            count = shoppingCenter.DeleteProductsByProducer(args[0]);
        }

        Print(count);
    }

    private static void FindProductsByName(ShoppingCenter shoppingCenter, string[] args)
    {
        if (!args.Any()) return;

        var result = shoppingCenter.FindProductsByName(args[0]);
        Print(result);
    }

    private static void FindProductsByProducer(ShoppingCenter shoppingCenter, string[] args)
    {
        if (!args.Any()) return;

        var result = shoppingCenter.FindProductsByProducer(args[0]);
        Print(result);
    }

    private static void FindProductsByPriceRange(ShoppingCenter shoppingCenter, string[] args)
    {
        if (args.Length < 2) return;

        var startPrice = decimal.Parse(args[0]);
        var endPrice = decimal.Parse(args[1]);
        var products = shoppingCenter.FindProductsByPriceRange(startPrice, endPrice);
        Print(products);
    }

    private static void Print(IEnumerable<Product> products)
        => Console.WriteLine(products.Any()
            ? string.Join(Environment.NewLine, products)
            : "No products found");

    private static void Print(int count)
        => Console.WriteLine(count != 0
            ? $"{count} products deleted"
            : "No products found");
}
