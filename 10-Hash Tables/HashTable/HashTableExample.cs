using System;
using System.Collections.Generic;
using System.Linq;

public class Example
{
    public static void Main()
    {
        //DemoHashTable();
        //DemoSet();
        //DemoHashSet();

        //CountSymbols();
        //PhoneBook();
    }

    private static void DemoHashTable()
    {
        HashTable<string, int> grades = new HashTable<string, int>();

        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        grades.Add("Peter", 3);
        grades.Add("Maria", 6);
        grades["George"] = 5;
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        grades.AddOrReplace("Peter", 33);
        grades.AddOrReplace("Tanya", 4);
        grades["George"] = 55;
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
        Console.WriteLine("Values: " + string.Join(", ", grades.Values));
        Console.WriteLine("Count = " + string.Join(", ", grades.Count));
        Console.WriteLine("--------------------");

        grades.Remove("Peter");
        grades.Remove("George");
        grades.Remove("George");
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
        Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
        Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
        Console.WriteLine("--------------------");
    }

    private static void DemoSet()
    {
        var setA = new Set<int>();
        setA.Add(1);
        setA.Add(5);
        setA.Add(2);
        setA.Add(11);
        setA.Add(8);
        setA.Add(34);

        var setB = new Set<int>();
        setB.Add(3);
        setB.Add(2);
        setB.Add(9);
        setB.Add(34);
        setB.Add(8);
        setB.Add(0);

        Console.WriteLine($"A: {setA}");
        Console.WriteLine($"B: {setB}");
        Console.WriteLine($"A Contains {1}: {setA.Contains(1)}");
        Console.WriteLine($"B Contains {1}: {setB.Contains(1)}");
        Console.WriteLine($"A UnionWith B: {setA.UnionWith(setB)}");
        Console.WriteLine($"A IntersectWith B: {setA.IntersectWith(setB)}");
        Console.WriteLine($"A ExceptWith B: {setA.ExceptWith(setB)}");
        Console.WriteLine($"A SymmetricExceptWith B: {setA.SymmetricExceptWith(setB)}");
    }

    private static void DemoHashSet()
    {
        var hashSetA = new HashSet<int> { 1, 5, 2, 11, 8, 34 };
        var hashSetB = new HashSet<int> { 3, 2, 9, 34, 8, 0 };

        Console.WriteLine($"A: {string.Join(" ", hashSetA)}");
        Console.WriteLine($"A: {string.Join(" ", hashSetB)}");

        Console.WriteLine($"A Contains {1}: {hashSetA.Contains(1)}");
        Console.WriteLine($"B Contains {1}: {hashSetB.Contains(1)}");

        var result = new HashSet<int>(hashSetA);
        result.UnionWith(hashSetB);
        Console.WriteLine($"A UnionWith B: {string.Join(" ", result)}");

        result = new HashSet<int>(hashSetA);
        result.IntersectWith(hashSetB);
        Console.WriteLine($"A IntersectWith B: {string.Join(" ", result)}");

        result = new HashSet<int>(hashSetA);
        result.ExceptWith(hashSetB);
        Console.WriteLine($"A ExceptWith B: {string.Join(" ", result)}");

        result = new HashSet<int>(hashSetA);
        result.SymmetricExceptWith(hashSetB);
        Console.WriteLine($"A SymmetricExceptWith B: {string.Join(" ", result)}");
    }

    private static void CountSymbols()
    {
        var dictionary = FillDictionary();
        Print(dictionary);
    }

    private static HashTable<char, int> FillDictionary()
    {
        var dictionary = new HashTable<char, int>();
        var symbols = Console.ReadLine().ToCharArray();

        foreach (var symbol in symbols)
        {
            if (!dictionary.ContainsKey(symbol))
            {
                dictionary[symbol] = 0;
            }

            dictionary[symbol]++;
        };

        return dictionary;
    }

    private static void Print(HashTable<char, int> dictionary)
    {
        var orderedKeys = dictionary.Keys.OrderBy(x => x);
        foreach (var key in orderedKeys)
        {
            Console.WriteLine($"{key}: {dictionary[key]} time/s");
        }
    }

    private static void PhoneBook()
    {
        var phonebook = FillPhonebook();
        SearchPhonebook(phonebook);
    }

    private static HashTable<string, string> FillPhonebook()
    {
        var phonebook = new HashTable<string, string>();

        while (true)
        {
            var line = Console.ReadLine();
            if (line == "search")
            {
                break;
            }

            var tokens = line.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 2)
            {
                continue;
            }

            var contactName = tokens[0];
            var phoneNumber = tokens[1];
            phonebook[contactName] = phoneNumber;
        }

        return phonebook;
    }

    private static void SearchPhonebook(HashTable<string, string> phonebook)
    {
        const string invalidContact = "Contact {0} does not exist.";
        const string validContact = "{0} -> {1}";

        while (true)
        {
            var contactName = Console.ReadLine();
            if (contactName == "end")
            {
                break;
            }

            phonebook.TryGetValue(contactName, out string phoneNumber);
            Console.WriteLine(
                phoneNumber == null
                ? string.Format(invalidContact, contactName)
                : string.Format(validContact, contactName, phoneNumber));
        }
    }
}
