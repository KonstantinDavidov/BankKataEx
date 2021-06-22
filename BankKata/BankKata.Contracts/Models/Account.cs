using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	public class Account
	{
		private readonly ITransactionStorage _transactionStorage;
		private readonly IStatementPrinter _statementPrinter;

		public Account(ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
		{
			_transactionStorage = transactionStorage;
			_statementPrinter = statementPrinter;
		}

		public void Deposit(int amount)
		{
			if (amount <= 0)
			{
				throw new DepositNotAllowedException("Deposit amount should be a positive number.");
			}

			_transactionStorage.Add(amount);
		}

		public void Withdraw(int amount)
		{
			if (amount <= 0)
			{
				throw new WithdrawNotAllowedException("Withdrawal amount should be a positive number.");
			}

			_transactionStorage.Add(-amount);
		}

		public void PrintStatement()
		{
			_statementPrinter.Print(_transactionStorage.AllTransactions());
		}
	}
}
