public interface ITransaction
{
    void Withdraw(decimal amount, Person person);
    void Deposit(decimal amount, Person person);
}
