using System;

public class Transaction : IComparable<Transaction>
{
    public Transaction(int id, TransactionStatus st, string from, string to, double amount)
    {
        this.Id = id;
        this.Status = st;
        this.From = from;
        this.To = to;
        this.Amount = amount;
    }

    public int Id { get; set; }

    public TransactionStatus Status { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public double Amount { get; set; }

    public int CompareTo(Transaction other)
        => other.Amount.CompareTo(this.Amount); // by Amount DESC
}