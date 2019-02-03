using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextEditor : ITextEditor
{
    //// NB Dictionary uses less memory than Trie => comment/uncomment to compare performance

    //private readonly Trie<StringBuilder> usersCurrent = new Trie<StringBuilder>(); // Trie
    private readonly Dictionary<string, StringBuilder> usersCurrent = new Dictionary<string, StringBuilder>(); // Dict
    private readonly Dictionary<string, Stack<StringBuilder>> usersCache = new Dictionary<string, Stack<StringBuilder>>();

    public void Clear(string username)
    {
        if (!this.IsLoggedIn(username)) return;

        this.Cache(username);
        this.GetCurrent(username).Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        if (!this.IsLoggedIn(username)) return;

        this.Cache(username);
        this.GetCurrent(username).Remove(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        if (!this.IsLoggedIn(username)) return;

        this.Cache(username);
        this.GetCurrent(username).Insert(index, str);
    }

    public int Length(string username)
        => this.IsLoggedIn(username)
        ? this.GetCurrent(username).Length
        : 0;

    public void Login(string username)
    {
        if (!this.IsValid(username)) return;

        this.UpdateCurrent(username, new StringBuilder());
        this.usersCache[username] = new Stack<StringBuilder>();
    }

    public void Logout(string username)
    {
        if (!this.IsLoggedIn(username)) return;

        this.RemoveCurrent(username);
        this.usersCache.Remove(username);
    }

    public void Prepend(string username, string str)
    {
        if (!this.IsLoggedIn(username)) return;

        this.Cache(username);
        this.GetCurrent(username).Insert(0, str);
    }

    public string Print(string username)
        => this.IsLoggedIn(username)
        ? this.GetCurrent(username).ToString()
        : string.Empty;

    public void Substring(string username, int startIndex, int length)
    {
        if (!this.IsLoggedIn(username)) return;

        this.Cache(username);
        var current = this.GetCurrent(username);
        var substr = this.GetSubstring(current, startIndex, length);
        this.UpdateCurrent(username, substr);
    }

    public void Undo(string username)
    {
        if (!this.IsLoggedIn(username)) return;

        var cacheStack = this.usersCache[username];
        if (cacheStack.Any())
        {
            var cache = cacheStack.Pop();
            this.UpdateCurrent(username, cache);
        }
    }

    public IEnumerable<string> Users(string prefix = "")
        => prefix == string.Empty
        ? this.usersCache.Keys
        : this.usersCache.Keys.Where(u => u.StartsWith(prefix));

    private void Cache(string username)
    {
        var current = this.GetCurrent(username);
        var cache = new StringBuilder().Append(current);
        this.usersCache[username].Push(cache);
    }

    private bool CurrentContains(string username)
        //=> this.usersCurrent.Contains(username); // Trie
        => this.usersCurrent.ContainsKey(username); // Dict

    private StringBuilder GetCurrent(string username)
        //=> this.usersCurrent.GetValue(username); // Trie
        => this.usersCurrent[username]; // Dict

    private StringBuilder GetSubstring(StringBuilder current, int startIndex, int length)
    {
        var builder = new StringBuilder();
        for (int i = startIndex; i < length; i++)
        {
            builder.Append(current[i]);
        }

        return builder;
    }

    private bool IsLoggedIn(string username)
        => this.usersCache.ContainsKey(username)
        && this.CurrentContains(username)
        ? true
        : false;

    private bool IsValid(string username)
        => !string.IsNullOrWhiteSpace(username);

    private void RemoveCurrent(string username)
        //=> this.UpdateCurrent(username, null); // Trie
        => this.usersCurrent.Remove(username); // Dict

    private void UpdateCurrent(string username, StringBuilder value)
        //=> this.usersCurrent.Insert(username, value); // Trie
        => this.usersCurrent[username] = value; // Dict
}
