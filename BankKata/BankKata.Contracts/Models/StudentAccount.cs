using System;
using System.Collections.Generic;
using System.Text;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Student can have 0 negative balance
	/// When creating a student account, a student id number must be provided
	/// </summary>
	public class StudentAccount
	{
		public int StudentId { get; }

		public StudentAccount(int studentId)
		{
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
