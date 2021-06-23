using BankKata.Contracts.Storages;
using BankKata.Infrastructure;
using BankKata.Infrastructure.Fabrics;
using BankKata.Infrastructure.RequestModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using BankAccount.Common;
using BankKata.Contracts.Exceptions;

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

		[TestCaseSource(nameof(NegativeBalance_TestData), new object[] { true })]
		public int AccountBalance_NegativeBalanceAllowed(AccountCreateRequest createRequest, int amount)
		{
			var account = _accountService.Create(createRequest);

			_accountService.WithdrawalFromAccount(account.Id, new AccountWithdrawalRequest(amount));

			return _accountService.GetAccountBalance(account.Id);
		}

		[TestCaseSource(nameof(NegativeBalance_TestData), new object[] { false })]
		public void AccountBalance_NegativeBalance_NOT_Allowed(AccountCreateRequest createRequest, int amount)
		{
			var account = _accountService.Create(createRequest);

			Assert.Throws<WithdrawNotAllowedException>(() => _accountService.WithdrawalFromAccount(account.Id, new AccountWithdrawalRequest(amount)));
		}

		[TestCaseSource(nameof(CreateAccountTypes), new object[] { true })]
		public void AccountBalance_can_do_deposits(AccountCreateRequest createRequest)
		{
			var account = _accountService.Create(createRequest);

			const int depositAmount = 1000;
			_accountService.DepositToAccount(account.Id, new AccountDepositRequest { Amount = depositAmount });

			Assert.AreEqual(depositAmount, _accountService.GetAccountBalance(account.Id));
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

		private static IEnumerable<TestCaseData> NegativeBalance_TestData(bool isNegativeBalanceAllowed)
		{
			if (isNegativeBalanceAllowed)
			{
				yield return new TestCaseData(new AccountCreateRequest.Business { EntityId = 1 }, Constants.MinAllowedBusinessAccountBalance * -1).Returns(Constants.MinAllowedBusinessAccountBalance);
				yield return new TestCaseData(new AccountCreateRequest.Giro(), Constants.MinAllowedGiroAccountBalance * -1).Returns(Constants.MinAllowedGiroAccountBalance);
			}
			else
			{
				yield return new TestCaseData(new AccountCreateRequest.Student { EntityId = 1 }, 500);
			}
		}
		#endregion
	}
}
