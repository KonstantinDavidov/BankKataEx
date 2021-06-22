using BankKata.Contracts.Enums;
using BankKata.Infrastructure.Fabrics;
using NUnit.Framework;
using System;

namespace BankKata.Tests
{
	public class AccountCreationFabricTest
	{
		[Test]
		[TestCase(AccountType.Business, 3)]
		[TestCase(AccountType.Student, 4)]
		[TestCase(AccountType.Giro, null)]
		public void Create_should_return_valid_results(AccountType accountType, int? entityType)
		{
			var fabric = new AccountCreationFabric();

			var newAccount = fabric.CreateAccount(1, accountType, entityType);

			Assert.AreEqual(accountType, newAccount.AccountType);
		}

		[Test]
		[TestCase(AccountType.None, 4)]
		[TestCase(AccountType.None, null)]
		[TestCase(AccountType.Giro, 1)]
		public void Create_should_thrown_exception(AccountType accountType, int? entityType)
		{
			var fabric = new AccountCreationFabric();

			Assert.Throws<InvalidOperationException>(() => fabric.CreateAccount(1, accountType, entityType));
		}
	}
}
