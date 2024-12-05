using System;

public struct Transaction
{
    public string AccountNumber { get; }
    public decimal Amount { get; }
    public Person Originator { get; }
    public DayTime Time { get; }

    public Transaction(string accountNumber, decimal amount, Person person, DayTime time)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        Originator = person;
        Time = time;
    }

    public override string ToString()
    {
        string action = Amount > 0 ? "deposited" : "withdrawn";
        return $"{AccountNumber} {Math.Abs(Amount):C} {action} by {Originator.Name} on {Time}";
    }
}
