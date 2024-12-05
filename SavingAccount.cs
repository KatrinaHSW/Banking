using System;

public class SavingAccount : Account, ITransaction
{
    private static readonly decimal COST_PER_TRANSACTION = 0.05m;
    private static readonly decimal INTEREST_RATE = 0.015m;

    public SavingAccount(decimal balance = 0)
        : base("SV-", balance) {}

    public new void Deposit(decimal amount, Person person)
    {
        base.Deposit(amount, person);
        OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
    }

    public void Withdraw(decimal amount, Person person)
    {
        if (!IsUser(person.Name))
        {
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, false));
            throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
        }

        if (!person.IsAuthenticated)
        {
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, false));
            throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
        }

        if (Balance < amount)
        {
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, false));
            throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
        }

        base.Deposit(-amount, person);
        OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, true));
    }

    public override void PrepareMonthlyStatement()
    {
        decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;
        decimal interest = (Balance * INTEREST_RATE)/12;
        Balance += interest - serviceCharge;
        transactions.Clear();
    }
}
