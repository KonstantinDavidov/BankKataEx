using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Student can have 0 negative balance
	/// When creating a student account, a student id number must be provided
	/// </summary>
	public class StudentAccount
	{
		private readonly ITransactionStorage _transactionStorage;

		public int StudentId { get; }

		public StudentAccount(int studentId, ITransactionStorage transactionStorage)
		{
			_transactionStorage = transactionStorage;
			StudentId = studentId;
		}

		public void Deposit(int amount)
		{
			throw new NotImplementedException();
		}

		public void Withdraw(int amount)
		{
			throw new NotImplementedException();
		}

		public void PrintStatement()
		{
			throw new NotImplementedException();
		}
	}
}
