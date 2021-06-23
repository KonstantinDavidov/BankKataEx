using System;
using BankKata.Business;
using BankKata.Business.Formatters;
using BankKata.Business.Models;
using BankKata.Business.Storages;
using BankKata.Business.Writers;

namespace BankKata.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var clock = new Clock();
			var transactionStore = new TransactionInMemoryRepository(clock);

			var console = new ConsoleOutputWriter();
			var statementPrinter = new StatementPrinter(console);

			var account = new StudentAccount(1, 1, transactionStore, statementPrinter);

			account.Deposit(1000);
			account.Withdraw(300);
			account.Withdraw(50);
			account.Deposit(500);

			account.PrintStatement();

			Console.ReadKey();
		}
	}
}
