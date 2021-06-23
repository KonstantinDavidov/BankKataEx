using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;
using BankKata.Business.Models;
using Moq;

namespace BankKata.Tests.UnitTests
{
	public class StudentAccountShould : BaseAccountTests
	{

		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter)
		{
			return new StudentAccount(1, 1, transactionMoq.Object, statementPrinter);
		}
	}
}
