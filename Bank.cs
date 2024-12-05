using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Bank
{
    public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>();
    public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>();

    static Bank()
    {
        // Initialize USERS collection
        AddUser("Narendra", "1234-5678");
        AddUser("Ilia", "2345-6789");
        AddUser("Mehrdad", "3456-7890");
        AddUser("Vinay", "4567-8901");
        AddUser("Arben", "5678-9012");
        AddUser("Patrick", "6789-0123");
        AddUser("Yin", "7890-1234");
        AddUser("Hao", "8901-2345");
        AddUser("Jake", "9012-3456");
        AddUser("Mayy", "1224-5678");
        AddUser("Nicoletta", "2344-6789");

        // Initialize ACCOUNTS collection
        AddAccount(new VisaAccount());
        AddAccount(new VisaAccount(150, -500));
        AddAccount(new SavingAccount(5000));
        AddAccount(new SavingAccount());
        AddAccount(new CheckingAccount(2000));
        AddAccount(new CheckingAccount(1500, true));
        AddAccount(new VisaAccount(50, -550));
        AddAccount(new SavingAccount(1000));

        // Associate users with accounts
        AddUserToAccount("VS-100000", "Narendra");
        AddUserToAccount("VS-100000", "Ilia");
        AddUserToAccount("VS-100000", "Mehrdad");

        AddUserToAccount("VS-100001", "Vinay");
        AddUserToAccount("VS-100001", "Arben");
        AddUserToAccount("VS-100001", "Patrick");

        AddUserToAccount("SV-100002", "Yin");
        AddUserToAccount("SV-100002", "Hao");
        AddUserToAccount("SV-100002", "Jake");

        AddUserToAccount("SV-100003", "Mayy");
        AddUserToAccount("SV-100003", "Nicoletta");

        AddUserToAccount("CK-100004", "Mehrdad");
        AddUserToAccount("CK-100004", "Arben");
        AddUserToAccount("CK-100004", "Yin");

        AddUserToAccount("CK-100005", "Jake");
        AddUserToAccount("CK-100005", "Nicoletta");

        AddUserToAccount("VS-100006", "Ilia");
        AddUserToAccount("VS-100006", "Vinay");

        AddUserToAccount("SV-100007", "Patrick");
        AddUserToAccount("SV-100007", "Hao");
    }
    public static void AddUser(string name, string sin)
    {
        var person = new Person(name, sin);
        person.OnLogin += Logger.LoginHandler;
        USERS.Add(name, person);
    }

    public static void AddAccount(Account account)
    {
        account.OnTransaction += Logger.TransactionHandler;
        ACCOUNTS.Add(account.Number, account);
    }

    public static void AddUserToAccount(string number, string name)
    {
        var account = GetAccount(number);
        var user = GetUser(name);
        account.AddUser(user);
    }

    public static Person GetUser(string name)
    {
        if (USERS.TryGetValue(name, out var person))
        {
            return person;
        }
        throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
    }

    public static Account GetAccount(string number)
    {
        if (ACCOUNTS.TryGetValue(number, out var account))
        {
            return account;
        }
        throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
    }

    public static void SaveAccounts(string filename)
    {
        string json = JsonSerializer.Serialize(ACCOUNTS.Values, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    public static void SaveUsers(string filename)
    {
        string json = JsonSerializer.Serialize(USERS.Values, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }
}
