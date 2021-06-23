using BankKata.Contracts.Formatters;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using BankKata.Contracts.Storages;
using Moq;
using NUnit.Framework;
using System;

namespace BankKata.Tests
{
	public class AccountStorageFeature
	{
		private TransactionInMemoryRepository _transactionRep;
		private StatementPrinter _statement;

		[SetUp]
		public void SetUp()
		{
			var clockMock = new Mock<IClock>();
			clockMock.SetupSequence(x => x.DateTimeNowAsString()).Returns("10/01/2012");
			var outputWriter = new Mock<IOutputWriter>();
			_transactionRep = new TransactionInMemoryRepository(clockMock.Object);
			_statement = new StatementPrinter(outputWriter.Object);
		}

		[Test]
		public void AccountStorage_should_not_contain_equal_accounts()
		{
			var account1 = new StudentAccount(1, 1, _transactionRep, _statement);
			var account2 = new StudentAccount(1, 1, _transactionRep, _statement);

			var storage = new BankAccountStorage();
			storage.Add(account1);

			Assert.Throws<InvalidOperationException>(() => storage.Add(account2));
		}

		[Test]
		public void AccountStorage_allow_to_get_account()
		{
			var account1 = new StudentAccount(1, 1, _transactionRep, _statement);
			var account2 = new StudentAccount(1, 2, _transactionRep, _statement);
			var account3 = new StudentAccount(1, 3, _transactionRep, _statement);

			var storage = new BankAccountStorage();
			storage.Add(account1);
			storage.Add(account2);
			storage.Add(account3);

			var accountFromStorage = storage.GetById(2);
			Assert.AreEqual(account2, accountFromStorage);
		}

		[Test]
		public void AccountStorage_get_valid_nextId()
		{
			var account1 = new StudentAccount(1, 1, _transactionRep, _statement);

			var storage = new BankAccountStorage();
			storage.Add(account1);

			Assert.AreEqual(2, storage.GetNextId());
		}
	}
}
