using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Console.WriteLine();

        //Console.WriteLine(list.Count);
        //Console.WriteLine(string.Join(" ", list.First(2)));
        //Console.WriteLine(string.Join(" ", list.Last(1)));

        //Console.WriteLine(string.Join(" ", list.Max(3)));
        //Console.WriteLine(string.Join(" ", list.Max(0)));
        //Console.WriteLine(string.Join(" ", list.Min(5)));

        //Console.WriteLine(string.Join(" ", list.Min(3)));
        //Console.WriteLine(string.Join(" ", list.Min(0)));
        //Console.WriteLine(string.Join(" ", list.Max(5)));

        //list.Clear();
        //Console.WriteLine(list.Count);

        //list.RemoveAll(1);
        //Console.WriteLine(list.Count);
        //Console.WriteLine(string.Join(" ", list.Max(3)));
    }
}
