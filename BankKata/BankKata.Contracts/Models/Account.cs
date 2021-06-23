using System.Collections.Generic;
using BankKata.Contracts.Enums;
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

		public int Deposit(int amount)
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
			return _transactionStorage.Add(amount);
		}
		
		public int Withdraw(int amount)
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
			return _transactionStorage.Add(-amount);
		}

		public void WithdrawRollback(int transactionId)
		{
			var transactionForRollback = _transactionStorage.GetById(transactionId);
			Balance += transactionForRollback.Amount;
			_transactionStorage.DeleteById(transactionForRollback);
		}

		public void DepositRollback(int transactionId)
		{
			var transactionForRollback = _transactionStorage.GetById(transactionId);
			Balance -= transactionForRollback.Amount;
			_transactionStorage.DeleteById(transactionForRollback);
		}

		public void PrintStatement()
		{
			_statementPrinter.Print(_transactionStorage.AllTransactions());
		}

		/// <summary>
		/// Validates whether a withdrawal operation allowed or not.
		/// </summary>
		public bool IsWithdrawAllowed(int amount)
		{
			if (amount <= 0)
			{
				throw new WithdrawNotAllowedException(nameof(amount));
			}

			var balanceAfterWithdrawal = Balance - amount;

			return balanceAfterWithdrawal >= MinAllowedBalance;
		}

		public bool IsDepositAllowed(int amount)
		{
			if (amount <= 0)
			{
				throw new DepositNotAllowedException(nameof(amount));
			}

			var balanceAfterDeposit = Balance + amount;

			return balanceAfterDeposit <= MaxAllowedBalance;
		}

		/// <summary>
		/// Validates whether transfer money to other account allowed or not.
		/// We need this method, because sometimes accounts may have negative balance (for example, Business Account).
		/// In this case system should NOT allow to send money to other accounts,
		/// because if balance is less than Amount (which is always positive), there is lack of money to send to other accounts :(
		/// </summary>
		public bool IsTransferToOtherAccountAllowed(int amount)
		{
			if (amount <= 0)
			{
				throw new DepositNotAllowedException(nameof(amount));
			}

			return Balance >= amount;
		}

		public IEnumerable<string> GetStatementList()
		{
			return _statementPrinter.GetStatementLines(_transactionStorage.AllTransactions());
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
