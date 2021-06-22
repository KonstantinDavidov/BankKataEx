using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Formatters;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public abstract class BaseAccountTests
	{
		protected StatementPrinter Statement;

		protected abstract Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter);

		[SetUp]
		public void Setup()
		{
			var outputWriter = new Mock<IOutputWriter>();
			Statement = new StatementPrinter(outputWriter.Object);
		}

		[Test]
		public void Should_store_deposit()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			account.Deposit(100);

			transactionMoq.Verify(transaction => transaction.Add(100), Times.Once());
		}

		[Test]
		public void Should_throw_deposit_exception()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.Throws<DepositNotAllowedException>(() => account.Deposit(-100));
		}

		[Test]
		public void Should_store_withdraw()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			account.Deposit(100);
			account.Withdraw(100);

			transactionMoq.Verify(transaction => transaction.Add(-100), Times.Once());
		}

		[Test]
		public void Should_throw_withdraw_exception()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.Throws<WithdrawNotAllowedException>(() => account.Withdraw(-100));
		}
	}
}
