using System;
public class AccountException : Exception
{
    public AccountException(ExceptionType reason)
        : base(reason.ToString()) {}
}
