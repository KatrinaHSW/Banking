using System;
using System.Collections.Generic;

public abstract class Account
{
    private static int LAST_NUMBER = 100_000;
    protected readonly List<Person> users = new List<Person>();
    public readonly List<Transaction> transactions = new List<Transaction>();
    public virtual event EventHandler OnLogin;
    public virtual event EventHandler OnTransaction;
    public string Number { get; }
    public decimal Balance { get; protected set; }
    public decimal LowestBalance { get; protected set; }

    public Account(string type, decimal balance)
    {
        Number = $"{type}{LAST_NUMBER++}";
        Balance = balance;
        LowestBalance = balance;
    }

    public void Deposit(decimal amount, Person person)
    {
        Balance += amount;
      
        if (Balance < LowestBalance)
        {
            LowestBalance = Balance;
        }

        Transaction transaction = new Transaction(Number, amount, person, Utils.Now);
        transactions.Add(transaction);
    }

    public void AddUser(Person person)
    {
        users.Add(person);
    }
    public bool IsUser(string name)
    {
        foreach (var user in users)
        {
            if (user.Name == name)
                return true;
        }
        return false;
    }
    public virtual void OnTransactionOccur(object sender, EventArgs e)
    {
        OnTransaction?.Invoke(sender, e);
    }

    public abstract void PrepareMonthlyStatement();

    public override string ToString()
    {
        // Initialize the return value with the account number and balance
        string result = $"[{Number} ";

        // Add the names of each user associated with the account
        string userList = string.Join(", ", users.ConvertAll(user => user.Name));
        result += $"{userList} {Balance:C}]\n";

        // Add details of all transactions
        foreach (var transaction in transactions)
        {
            result += $"   {transaction}\n";
        }

        return result.Trim(); // Remove any trailing whitespace
    }
}
