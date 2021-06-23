using BankAccount.Common;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;
using BankKata.Business.Models;
using Moq;

namespace BankKata.Tests.UnitTests
{
	public class BusinessAccountShould : BaseAccountTests
	{
		protected override int MinAllowedBalance => Constants.MinAllowedBusinessAccountBalance;

		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter)
		{
			return new BusinessAccount(1, 1, transactionMoq.Object, statementPrinter);
		}
	}
}
