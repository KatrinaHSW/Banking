﻿using System;

public class Person
{
    private string password;
    public event EventHandler OnLogin;

    public string Sin { get; }
    public string Name { get; }
    public bool IsAuthenticated { get; private set; }

    public Person(string name, string sin)
    {
        Name = name;
        Sin = sin;
        password = sin.Substring(0, 3); // First three characters of SIN
    }

    public void Login(string password)
    {
        if (this.password != password)
        {
            IsAuthenticated = false;
            OnLogin?.Invoke(this, new LoginEventArgs(Name, false));
            throw new AccountException(ExceptionType.PASSWORD_INCORRECT);
        }
        else
        {
            IsAuthenticated = true;
            OnLogin?.Invoke(this, new LoginEventArgs(Name, true));
        }
    }
    public void Logout()
    {
        IsAuthenticated = false;
    }
    public override string ToString()
    {
        return $"[{Name}]";
    }
}
