using System;
public class LoginEventArgs : EventArgs
{
    public string PersonName { get; }
    public bool Success { get; }

    public LoginEventArgs(string personName, bool success)
        : base()
    {
        PersonName = personName;
        Success = success;
    }
}
