using System;

public class CheckingAccount : Account, ITransaction
{
    private static readonly decimal COST_PER_TRANSACTION = 0.05m;
    private static readonly decimal INTEREST_RATE = 0.005m;
    private bool hasOverdraft;

    public CheckingAccount(decimal balance = 0, bool hasOverdraft = false)
        : base("CK-", balance)
    {
        this.hasOverdraft = hasOverdraft;
    }

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

        if (Balance < amount && !hasOverdraft)
        {
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, false));
            throw new AccountException(ExceptionType.NO_OVERDRAFT_FOR_THIS_ACCOUNT);
        }
        else 
        {
            base.Deposit(-amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, true));
        }
    }

    public override void PrepareMonthlyStatement()
    {
        decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;
        decimal interest = (Balance * INTEREST_RATE) / 12;
        Balance += interest - serviceCharge;
        transactions.Clear();
    }
}
