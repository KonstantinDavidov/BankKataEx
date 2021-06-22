using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public class BusinessAccountShould : BaseAccountTests
	{
		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter)
		{
			return new BusinessAccount(1, transactionMoq.Object, statementPrinter);
		}

		[Test]
		public void Should_NOT_have_negative_balance()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.AreEqual(0, account.Balance);

			Assert.Throws<WithdrawNotAllowedException>(() => account.Withdraw(500));
		}

		[Test]
		public void Should_have_restriction_by_max_deposit()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.Throws<DepositNotAllowedException>(() => account.Deposit(100001));
		}
	}
}
