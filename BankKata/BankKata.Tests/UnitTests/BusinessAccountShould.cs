using BankAccount.Common;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Storages;
using BankKata.Contracts.Models;
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
