using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public class StudentAccountShould : BaseAccountTests
	{

		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq, IStatementPrinter statementPrinter)
		{
			return new StudentAccount(1, 1, transactionMoq.Object, statementPrinter);
		}

		[Test]
		public void Should_be_able_to_have_negative_balance()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq, Statement);

			Assert.AreEqual(0, account.Balance);

			//verify that Withdraw doesn't throw an exception to make sure this operation is valid on negative balance for Student Account.
			Assert.DoesNotThrow(() => account.Withdraw(500));
			Assert.AreEqual(-500, account.Balance);
		}
	}
}
