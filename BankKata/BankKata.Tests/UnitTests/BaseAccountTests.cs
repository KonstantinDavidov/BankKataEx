using System.Collections.Generic;
using BankAccount.Common;
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

		//Created a field here that I will override in child tests,
		//because I want to keep Account.MinAllowedBalance encapsulated (it's a protected field now.)
		protected virtual int MinAllowedBalance => 0;

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

		[Test]
		public void Should_have_restricted_negative_balance()
		{
			//we need this, because I don't allow Withdraw(0), because it doesn't make sense.
			var depositAmount = 100;
			var withdrawAmount = MinAllowedBalance - depositAmount;
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.AreEqual(0, account.Balance);
			
			account.Deposit(depositAmount);
			Assert.AreEqual(depositAmount, account.Balance);
			
			//Withdraw takes only positive numbers, to reuse constant, I multiply to -1 to make it positive.
			Assert.DoesNotThrow(() => account.Withdraw(withdrawAmount * -1));

			Assert.AreEqual(MinAllowedBalance, account.Balance);
		}

		[Test]
		public void Should_not_allow_to_have_less_than_max_negative_balance()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.AreEqual(0, account.Balance);

			var amount = (MinAllowedBalance - 1) * -1; //Try to withdraw more than allowed negative balance. It should not be allowed.
			Assert.Throws<WithdrawNotAllowedException>(() => account.Withdraw(amount));
		}
	}
}
