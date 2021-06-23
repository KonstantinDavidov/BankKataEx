﻿using BankKata.Contracts.Enums;
using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Storages;

namespace BankKata.Contracts.Models
{
	public abstract class Account
	{
		private readonly ITransactionStorage _transactionStorage;
		private readonly IStatementPrinter _statementPrinter;

		public int Id { get; }
		public int Balance { get; protected set; }
		public abstract AccountType AccountType { get; }

		protected virtual int MaxAllowedBalance => int.MaxValue;
		protected virtual int MinAllowedBalance => 0;

		protected Account(int id, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
		{
			Id = id;
			_transactionStorage = transactionStorage;
			_statementPrinter = statementPrinter;
		}

		protected Account()
		{
		}

		public void Deposit(int amount)
		{
			if (amount <= 0)
			{
				throw new DepositNotAllowedException(nameof(amount));
			}

			if (!IsDepositAllowed(amount))
			{
				throw new DepositNotAllowedException("Deposit is not allowed.");
			}

			Balance += amount;
			_transactionStorage.Add(amount);
		}
		
		public void Withdraw(int amount)
		{
			if (amount <= 0)
			{
				throw new WithdrawNotAllowedException("Withdrawal amount should be a positive number.");
			}

			if (!IsWithdrawAllowed(amount))
			{
				throw new WithdrawNotAllowedException("Withdrawal is not allowed.");
			}

			Balance -= amount;
			_transactionStorage.Add(-amount);
		}

		public void PrintStatement()
		{
			_statementPrinter.Print(_transactionStorage.AllTransactions());
		}

		/// <summary>
		/// Validates whether a withdrawal operation allowed or not.
		/// </summary>
		private bool IsWithdrawAllowed(int amount)
		{
			var balanceAfterWithdrawal = Balance - amount;

			return balanceAfterWithdrawal >= MinAllowedBalance;
		}

		private bool IsDepositAllowed(int amount)
		{
			var balanceAfterDeposit = Balance + amount;

			return balanceAfterDeposit <= MaxAllowedBalance;
		}

		#region Equals override

		protected bool Equals(Account other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Account)obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		#endregion
	}
}
