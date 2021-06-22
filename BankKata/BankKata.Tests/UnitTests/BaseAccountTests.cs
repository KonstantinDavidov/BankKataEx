using System.Collections.Generic;
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

		[Test]
		public void Should_print_statement()
		{
			var clockMock = new Mock<IClock>();
			var transactions = new List<Transaction> { new Transaction(clockMock.Object.DateTimeNowAsString(), 123) };
			var repositoryMoq = new Mock<ITransactionStorage>();
			repositoryMoq.Setup(repository => repository.AllTransactions()).Returns(transactions);
			var statementMoq = new Mock<IStatementPrinter>();

			var account = CreateAccountEntity(repositoryMoq, statementMoq.Object);

			account.PrintStatement();

			statementMoq.Verify(x => x.Print(transactions));
		}
	}
}
