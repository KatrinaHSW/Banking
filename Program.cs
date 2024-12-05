using System;

namespace BankingDemo

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("All accounts:");
            foreach (var account in Bank.ACCOUNTS.Values)
                Console.WriteLine(account);

            Console.WriteLine("\nAll Users:");
            foreach (var user in Bank.USERS.Values)
                Console.WriteLine(user);
                Console.WriteLine();

            // Users
            Person p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10;
            p0 = Bank.GetUser("Narendra");
            p1 = Bank.GetUser("Ilia");
            p2 = Bank.GetUser("Mehrdad");
            p3 = Bank.GetUser("Vinay");
            p4 = Bank.GetUser("Arben");
            p5 = Bank.GetUser("Patrick");
            p6 = Bank.GetUser("Yin");
            p7 = Bank.GetUser("Hao");
            p8 = Bank.GetUser("Jake");
            p9 = Bank.GetUser("Mayy");
            p10 = Bank.GetUser("Nicoletta");

            // Logins
            p0.Login("123"); p1.Login("234");
            p2.Login("345"); p3.Login("456");
            p4.Login("567"); p5.Login("678");
            p6.Login("789"); p7.Login("890");
            p10.Login("234"); p8.Login("901");

            // Test accounts and transactions
            VisaAccount a = Bank.GetAccount("VS-100000") as VisaAccount;
            a.DoPayment(1500, p0);
            a.DoPurchase(200, p1);
            a.DoPurchase(25, p2);
            a.DoPurchase(15, p0);
            a.DoPurchase(39, p1);
            a.DoPayment(400, p0);
            Console.WriteLine(a);

            a = Bank.GetAccount("VS-100001") as VisaAccount;
            a.DoPayment(500, p0);
            a.DoPurchase(25, p3);
            a.DoPurchase(20, p4);
            a.DoPurchase(15, p5);
            Console.WriteLine(a);

            SavingAccount b = Bank.GetAccount("SV-100002") as SavingAccount;
            b.Withdraw(300, p6);
            b.Withdraw(32.90m, p6);
            b.Withdraw(50, p7);
            b.Withdraw(111.11m, p8);
            Console.WriteLine(b);

            b = Bank.GetAccount("SV-100003") as SavingAccount;
            b.Deposit(300, p3);
            b.Deposit(32.90m, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11m, p10);
            Console.WriteLine(b);

            CheckingAccount c = Bank.GetAccount("CK-100004") as CheckingAccount;
            c.Deposit(33.33m, p7);
            c.Deposit(40.44m, p7);
            c.Withdraw(150, p2);
            c.Withdraw(200, p4);
            c.Withdraw(645, p6);
            c.Withdraw(350, p6);
            Console.WriteLine(c);

            c = Bank.GetAccount("CK-100005") as CheckingAccount;
            c.Deposit(33.33m, p8);
            c.Deposit(40.44m, p7);
            c.Withdraw(450, p10);
            c.Withdraw(500, p8);
            c.Withdraw(645, p10);
            c.Withdraw(850, p10);
            Console.WriteLine(c);

            a = Bank.GetAccount("VS-100006") as VisaAccount;
            a.DoPayment(700, p0);
            a.DoPurchase(20, p3);
            a.DoPurchase(10, p1);
            a.DoPurchase(15, p1);
            Console.WriteLine(a);

            b = Bank.GetAccount("SV-100007") as SavingAccount;
            b.Deposit(300, p3);
            b.Deposit(32.90m, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11m, p7);
            Console.WriteLine(b);

            // Exceptions
            Console.WriteLine("\nExceptions:");
            try { p8.Login("911"); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { p3.Logout(); a.DoPurchase(12.5m, p3); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { a.DoPurchase(12.5m, p0); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { a.DoPurchase(5825, p4); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { c.Withdraw(1500, p6); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { Bank.GetAccount("CK-100018"); } catch (AccountException e) { Console.WriteLine(e.Message); }
            try { Bank.GetUser("Trudeau"); } catch (AccountException e) { Console.WriteLine(e.Message); }

            // Transactions and reports
            Console.WriteLine("\nAll transactions:");

            foreach (var account in Bank.ACCOUNTS.Values)
            {
                foreach (var transaction in account.transactions)
                {
                    Console.WriteLine(transaction);
                }
            }

            foreach (var account in Bank.ACCOUNTS.Values)
            {
                Console.WriteLine("--------------\nBefore PrepareMonthlyReport()");
                Console.WriteLine(account);

                account.PrepareMonthlyStatement(); // Ensure this matches the method name

                Console.WriteLine("\nAfter PrepareMonthlyReport()");
                Console.WriteLine(account);
            }

            // Log events
            Logger.ShowLoginEvents("LoginEvents.txt");
            Logger.ShowTransactionEvents("TransactionEvents.txt");
        }
    }
}