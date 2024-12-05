using System;

public class VisaAccount : Account, ITransaction
{
    private static readonly decimal INTEREST_RATE = 0.1995m;
    private decimal creditLimit;

    public VisaAccount(decimal balance = 0, decimal creditLimit = 1200)
        : base("VS-", balance)
    {
        this.creditLimit = creditLimit;
    }

    public void DoPayment(decimal amount, Person person)
    {
        base.Deposit(amount, person);
        OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
    }

    public void DoPurchase(decimal amount, Person person)
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

        if (Balance - amount < -creditLimit)
        {
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, false));
            throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
        }
        else
        {
            base.Deposit(-amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, true));
        }
        }
    public void Withdraw(decimal amount, Person person)
    {
        // Call DoPurchase for consistency
        DoPurchase(amount, person);
    }

    public override void PrepareMonthlyStatement()
    {
        decimal interest = (Balance * INTEREST_RATE) / 12;
        Balance += interest;
        transactions.Clear();
    }
}
