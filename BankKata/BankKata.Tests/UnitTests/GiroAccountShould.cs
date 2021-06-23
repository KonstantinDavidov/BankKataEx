using BankAccount.Common;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;
using BankKata.Business.Models;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public class GiroAccountShould : BaseAccountTests
	{
		protected override int MinAllowedBalance => Constants.MinAllowedGiroAccountBalance;

		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter)
		{
			return new GiroAccount(1, transactionMoq.Object, statementPrinter);
		}
	}
}
