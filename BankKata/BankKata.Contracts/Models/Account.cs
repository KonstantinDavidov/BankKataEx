using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	public class Account
	{
		private readonly ITransactionStorage _transactionStorage;

		public Account(ITransactionStorage transactionStorage)
		{
			_transactionStorage = transactionStorage;
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
			throw new NotImplementedException();
		}
	}
}
