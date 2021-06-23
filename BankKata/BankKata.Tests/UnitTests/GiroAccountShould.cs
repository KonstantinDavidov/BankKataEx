using BankAccount.Common;
using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Storages;
using BankKata.Contracts.Models;
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
