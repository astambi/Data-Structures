using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private readonly Dictionary<int, Transaction> byId = new Dictionary<int, Transaction>();
    private readonly Dictionary<TransactionStatus, HashSet<Transaction>> byStatus = new Dictionary<TransactionStatus, HashSet<Transaction>>();

    public int Count => this.byId.Count;

    public void Add(Transaction tx)
    {
        if (tx == null) return;

        // By Id
        this.byId[tx.Id] = tx;

        // By Status
        if (!this.byStatus.ContainsKey(tx.Status))
        {
            this.byStatus[tx.Status] = new HashSet<Transaction>();
        }
        this.byStatus[tx.Status].Add(tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.Contains(id))
        {
            throw new ArgumentException();
        }

        var transaction = this.byId[id];

        var oldStatus = transaction.Status;
        if (oldStatus == newStatus) return; // No change

        // Remove from old status
        this.byStatus[oldStatus].Remove(transaction);

        transaction.Status = newStatus;

        // Add with new status
        if (!this.byStatus.ContainsKey(newStatus))
        {
            this.byStatus[newStatus] = new HashSet<Transaction>();
        }
        this.byStatus[newStatus].Add(transaction);
    }

    public bool Contains(Transaction tx)
        => tx != null
        && this.byId.ContainsKey(tx.Id);

    public bool Contains(int id)
        => this.byId.ContainsKey(id);

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
        => this.byId.Values
        .Where(t => lo <= t.Amount && t.Amount <= hi);

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
        => this.byId.Values
        .OrderByDescending(t => t.Amount)
        .ThenBy(t => t.Id);

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        this.ValidateWithException(status);

        var result = this.byStatus[status]
            .OrderByDescending(t => t.Amount)
            .Select(t => t.To);

        this.ValidateWithException(result);

        return result;
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        this.ValidateWithException(status);

        var result = this.byStatus[status]
            .OrderByDescending(t => t.Amount)
            .Select(t => t.From);

        this.ValidateWithException(result);

        return result;
    }

    public Transaction GetById(int id)
    {
        this.ValidateWithException(id);

        return this.byId[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.byId.Values
            .Where(t => t.To == receiver)
            .Where(t => lo <= t.Amount && t.Amount < hi);

        this.ValidateWithException(result);

        return result
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.byId.Values
            .Where(t => t.To == receiver);

        this.ValidateWithException(result);

        return result
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.byId.Values
            .Where(t => t.From == sender)
            .Where(t => amount < t.Amount);

        this.ValidateWithException(result);

        return result
            .OrderByDescending(t => t.Amount);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.byId.Values
            .Where(t => t.From == sender);

        this.ValidateWithException(result);

        return result
            .OrderByDescending(t => t.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        this.ValidateWithException(status);

        return this.byStatus[status]
            .OrderByDescending(t => t.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        if (!this.byStatus.ContainsKey(status))
        {
            return Enumerable.Empty<Transaction>();
        }

        return this.byStatus[status]
            .Where(t => t.Amount <= amount)
            .OrderByDescending(t => t.Amount);
    }

    public void RemoveTransactionById(int id)
    {
        this.ValidateWithException(id);

        var transaction = this.byId[id];

        this.byId.Remove(id);
        this.byStatus[transaction.Status].Remove(transaction);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        var transactions = this.byId.Values;
        foreach (var transaction in transactions)
        {
            yield return transaction;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private void ValidateWithException(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
    }

    private void ValidateWithException(TransactionStatus status)
    {
        if (!this.byStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }
    }

    private void ValidateWithException<T>(IEnumerable<T> result)
    {
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
    }
}
