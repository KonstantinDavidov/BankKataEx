using BankKata.Contracts.Storages;
using BankKata.Infrastructure;
using BankKata.Infrastructure.Fabrics;
using BankKata.Infrastructure.RequestModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankKata.Tests
{
	public class AccountServiceTests
	{
		private AccountService _accountService;

		[SetUp]
		public void SetUp()
		{
			var bankAccountStorage = new BankAccountStorage();
			var accountCreationFabric = new AccountCreationFabric();
			_accountService = new AccountService(bankAccountStorage, accountCreationFabric);
		}


		[TestCaseSource(nameof(CreateAccountTypes), new object[] { true })]
		public void CreateAccountTest_HappyPath(AccountCreateRequest createRequest)
		{
			var newAccount = _accountService.Create(createRequest);

			Assert.AreEqual(createRequest.AccountType, newAccount.AccountType);
			Assert.AreEqual(1, newAccount.Id);
		}

		[TestCaseSource(nameof(CreateAccountTypes), new object[] { false })]
		public void CreateAccountTest_Invalid_Path(AccountCreateRequest createRequest)
		{
			Assert.Throws<InvalidOperationException>(() => _accountService.Create(createRequest));
		}

		[TestCaseSource(nameof(CreateAccountTypes), new object[] { true })]
		public void CreateAccount_Increments_Ids(AccountCreateRequest createRequest)
		{
			var newAccount1 = _accountService.Create(createRequest);
			var newAccount2 = _accountService.Create(createRequest);
			var newAccount3 = _accountService.Create(createRequest);

			Assert.AreEqual(1, newAccount1.Id);
			Assert.AreEqual(2, newAccount2.Id);
			Assert.AreEqual(3, newAccount3.Id);
		}

		#region TestData
		private static IEnumerable<AccountCreateRequest> CreateAccountTypes(bool isHappyPath)
		{
			if (isHappyPath)
			{
				yield return new AccountCreateRequest.Business { EntityId = 1 };
				yield return new AccountCreateRequest.Giro();
				yield return new AccountCreateRequest.Student { EntityId = 2 };
			}
			else
			{
				yield return new AccountCreateRequest.Business();
				yield return new AccountCreateRequest.Student();
				yield return new AccountCreateRequest.Giro { EntityId = 1 };
			}
		}
		#endregion
	}
}
