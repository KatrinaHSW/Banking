using System.Collections.Generic;
using System.IO;
using System;

public static class Logger
{
    private static List<string> loginEvents = new List<string>();
    private static List<string> transactionEvents = new List<string>();

    public static void LoginHandler(object sender, EventArgs args)
    {
        if (args is LoginEventArgs loginArgs)
        {
            string personName = loginArgs.PersonName;
            bool success = loginArgs.Success;
            string status = success ? "successfully" : "unsuccessfully";
            string currentTime = Utils.Now.ToString();

            string log = $"{personName} logged in {status} on {currentTime}";
            loginEvents.Add(log);
        }
    }

    public static void TransactionHandler(object sender, EventArgs args)
    {
        if (args is TransactionEventArgs transactionArgs)
        {
            string personName = transactionArgs.PersonName;
            decimal amount = transactionArgs.Amount;
            string operation = amount > 0 ? "deposit" : "withdraw";
            bool success = transactionArgs.Success;
            string status = success ? "successfully" : "unsuccessfully";
            string currentTime = Utils.Now.ToString();

            string log = $"{personName} {operation} {Math.Abs(amount):C} {status} on {currentTime}";
            transactionEvents.Add(log);
        }
    }
    public static void ShowLoginEvents(string filename)
    {
        Console.WriteLine($"--------------\nLogin Events as of {Utils.Now}:");

        for (int i = 0; i < loginEvents.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {loginEvents[i]}");
        }
    }
public static void ShowTransactionEvents(string v)
    {
        Console.WriteLine($"\nTransaction Events as of {Utils.Now}:");

        for (int i = 0; i < transactionEvents.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {transactionEvents[i]}");
        }
    }
}
