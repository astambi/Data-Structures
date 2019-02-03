using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Startup
{
    public static void Main()
    {
        var textEditor = new TextEditor();
        var regex = new Regex("\"(.*)\"");

        while (true)
        {
            var line = Console.ReadLine();
            if (line == "end")
            {
                break;
            }

            var match = regex.Match(line);
            var str = string.Empty;
            if (match.Success)
            {
                str = match.Groups[1].Value;
                line = line.Substring(0, line.IndexOf(str) - 1);
            }

            var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = commandArgs[0];

            switch (command)
            {
                // {command} {username} {params}
                case "login": ProcessLogin(textEditor, commandArgs); break;
                case "logout": ProcessLogout(textEditor, commandArgs); break;
                case "users": ProcessUsers(textEditor, commandArgs); break;
                default:
                    // {username} {command} {params}
                    var username = commandArgs[0];

                    // Ignore commands if user is not logged in
                    if (!textEditor.Users(username).Contains(username))
                    {
                        break;
                    }

                    command = commandArgs[1];
                    commandArgs = commandArgs.Skip(2).ToArray();

                    switch (command)
                    {
                        case "insert": ProcessInsert(textEditor, username, commandArgs, str); break;
                        case "prepend": textEditor.Prepend(username, str); break;
                        case "substring": ProcessSubstring(textEditor, username, commandArgs); break;
                        case "delete": ProcessDelete(textEditor, username, commandArgs); break;
                        case "clear": textEditor.Clear(username); break;
                        case "length": Console.WriteLine(textEditor.Length(username)); break;
                        case "print": Console.WriteLine(textEditor.Print(username)); break;
                        case "undo": textEditor.Undo(username); break;
                        default: break;
                    }
                    break;
            }
        }
    }

    private static void ProcessLogin(TextEditor textEditor, string[] commandArgs)
    {
        if (commandArgs.Length > 1)
        {
            var username = commandArgs[1];
            textEditor.Login(username);
        }
    }

    private static void ProcessLogout(TextEditor textEditor, string[] commandArgs)
    {
        if (commandArgs.Length > 1)
        {
            var username = commandArgs[1];
            textEditor.Logout(username);
        }
    }

    private static void ProcessUsers(TextEditor textEditor, string[] commandArgs)
    {
        var prefix = commandArgs.Length > 1 ? commandArgs[1] : string.Empty;
        var users = textEditor.Users(prefix);
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }

    private static void ProcessInsert(TextEditor textEditor, string username, string[] commandArgs, string str)
    {
        if (commandArgs.Length > 0)
        {
            var index = int.Parse(commandArgs[0]);
            textEditor.Insert(username, index, str);
        }
    }

    private static void ProcessSubstring(TextEditor textEditor, string username, string[] commandArgs)
    {
        if (commandArgs.Length > 1)
        {
            var index = int.Parse(commandArgs[0]);
            var length = int.Parse(commandArgs[1]);
            textEditor.Substring(username, index, length);
        }
    }

    private static void ProcessDelete(TextEditor textEditor, string username, string[] commandArgs)
    {
        if (commandArgs.Length > 1)
        {
            var index = int.Parse(commandArgs[0]);
            var length = int.Parse(commandArgs[1]);
            textEditor.Delete(username, index, length);
        }
    }
}
