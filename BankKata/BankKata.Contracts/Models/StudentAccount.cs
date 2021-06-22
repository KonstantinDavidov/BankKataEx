using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Enums;
using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Student can have 0 negative balance
	/// When creating a student account, a student id number must be provided
	/// </summary>
	public class StudentAccount : Account
	{
		protected override int MinAllowedBalance => int.MinValue;
		public override AccountType AccountType => AccountType.Student;

		public int StudentId { get; }

		public StudentAccount(int studentId, int accountId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(accountId, transactionStorage, statementPrinter)
		{
			StudentId = studentId;
		}
	}
}
