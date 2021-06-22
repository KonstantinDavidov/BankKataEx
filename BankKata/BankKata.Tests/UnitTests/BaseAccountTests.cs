using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public abstract class BaseAccountTests
	{
		protected abstract Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq);

		[Test]
		public void Should_store_deposit()
		{
			var transactionMoq = new Mock<ITransactionStorage>();
			var account = CreateAccountEntity(transactionMoq);

			account.Deposit(100);

			transactionMoq.Verify(transaction => transaction.Add(100), Times.Once());
		}
	}
}
