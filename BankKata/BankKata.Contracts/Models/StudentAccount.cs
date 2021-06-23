using BankKata.Business.Enums;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;

namespace BankKata.Business.Models
{
	/// <summary>
	/// Student can have 0 negative balance (Student: no negative balance allowed)
	/// When creating a student account, a student id number must be provided
	/// </summary>
	public class StudentAccount : Account
	{
		public override AccountType AccountType => AccountType.Student;

		public int StudentId { get; }

		public StudentAccount(int studentId, int accountId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(accountId, transactionStorage, statementPrinter)
		{
			StudentId = studentId;
		}
	}
}
