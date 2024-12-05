public class TransactionEventArgs : LoginEventArgs
{
    public decimal Amount { get; }
    public TransactionEventArgs(string personName, decimal amount, bool success)
        : base(personName, success)
    {
        Amount = amount;
    }
}
