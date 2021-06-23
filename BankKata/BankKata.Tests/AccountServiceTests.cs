using BankKata.Infrastructure;
using BankKata.Infrastructure.Fabrics;
using BankKata.Infrastructure.RequestModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankAccount.Common;
using BankKata.Business.Exceptions;
using BankKata.Business.Storages;

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
		public void CreateAccount_increments_ids(AccountCreateRequest createRequest)
		{
			var newAccount1 = _accountService.Create(createRequest);
			var newAccount2 = _accountService.Create(createRequest);
			var newAccount3 = _accountService.Create(createRequest);

			Assert.AreEqual(1, newAccount1.Id);
			Assert.AreEqual(2, newAccount2.Id);
			Assert.AreEqual(3, newAccount3.Id);
		}

		[TestCaseSource(nameof(NegativeBalance_TestData), new object[] { true })]
		public int NegativeBalanceAllowed(AccountCreateRequest createRequest, int amount)
		{
			var account = _accountService.Create(createRequest);

			_accountService.WithdrawalFromAccount(account.Id, new AccountWithdrawalRequest(amount));

			return _accountService.GetAccountBalance(account.Id);
		}

		[TestCaseSource(nameof(NegativeBalance_TestData), new object[] { false })]
		public void NegativeBalance_NOT_allowed(AccountCreateRequest createRequest, int amount)
		{
			var account = _accountService.Create(createRequest);

			Assert.Throws<WithdrawNotAllowedException>(() => _accountService.WithdrawalFromAccount(account.Id, new AccountWithdrawalRequest(amount)));
		}

		[TestCaseSource(nameof(CreateAccountTypes), new object[] { true })]
		public void Can_do_deposits(AccountCreateRequest createRequest)
		{
			var account = _accountService.Create(createRequest);

			const int depositAmount = 1000;
			_accountService.DepositToAccount(account.Id, new AccountDepositRequest { Amount = depositAmount });

			Assert.AreEqual(depositAmount, _accountService.GetAccountBalance(account.Id));
		}

		[TestCaseSource(nameof(TransactionBetweenAccounts_TestData), new object[] { true })]
		public void TransactionBetweenAccountsTest(AccountCreateRequest createRequest1, AccountCreateRequest createRequest2,
			int depositAmountAcc1, int depositAmountAcc2, int transferAmount)
		{
			var account1 = _accountService.Create(createRequest1);
			var account2 = _accountService.Create(createRequest2);

			_accountService.DepositToAccount(account1.Id, new AccountDepositRequest(depositAmountAcc1));
			_accountService.DepositToAccount(account2.Id, new AccountDepositRequest(depositAmountAcc2));

			_accountService.TransactionBetweenAccounts(account1.Id, account2.Id, new AccountDepositRequest(transferAmount));

			var balance1 = _accountService.GetAccountBalance(account1.Id);
			var balance2 = _accountService.GetAccountBalance(account2.Id);

			Assert.AreEqual(depositAmountAcc1 - transferAmount, balance1);
			Assert.AreEqual(depositAmountAcc2 + transferAmount, balance2);
		}

		[TestCaseSource(nameof(TransactionBetweenAccounts_TestData), new object[] { false })]
		public void TransactionBetweenAccountsTest_restriction(AccountCreateRequest createRequest1, AccountCreateRequest createRequest2,
			int depositAmountAcc1, int depositAmountAcc2, int transferAmount)
		{
			var account1 = _accountService.Create(createRequest1);
			var account2 = _accountService.Create(createRequest2);

			_accountService.DepositToAccount(account1.Id, new AccountDepositRequest(depositAmountAcc1));
			_accountService.DepositToAccount(account2.Id, new AccountDepositRequest(depositAmountAcc2));

			Assert.Throws<WithdrawNotAllowedException>(() =>
				_accountService.TransactionBetweenAccounts(account1.Id, account2.Id, new AccountDepositRequest(transferAmount)));
		}

		[Test]
		public void Get_statement()
		{
			var expectedForAccount1 = new List<string>
			{
				"Date || Amount || Balance",
				"23/06/2021 || -10 || 890",
				"23/06/2021 || -100 || 900",
				"23/06/2021 || 500 || 1000",
				"23/06/2021 || 500 || 500"
			};

			var expectedForAccount2 = new List<string>
			{
				"Date || Amount || Balance",
				"23/06/2021 || 10 || 110",
				"23/06/2021 || -100 || 100",
				"23/06/2021 || 100 || 200",
				"23/06/2021 || 100 || 100"
			};
			const int depositAmountAcc1 = 500;
			const int depositAmountAcc2 = 100;
			const int transferAmount = 10;
			var account1 = _accountService.Create(new AccountCreateRequest.Business(1));
			var account2 = _accountService.Create(new AccountCreateRequest.Business(2));

			_accountService.DepositToAccount(account1.Id, new AccountDepositRequest(depositAmountAcc1));
			_accountService.DepositToAccount(account1.Id, new AccountDepositRequest(depositAmountAcc1));
			_accountService.DepositToAccount(account2.Id, new AccountDepositRequest(depositAmountAcc2));
			_accountService.DepositToAccount(account2.Id, new AccountDepositRequest(depositAmountAcc2));
			_accountService.WithdrawalFromAccount(account2.Id, new AccountWithdrawalRequest(depositAmountAcc2));
			_accountService.WithdrawalFromAccount(account1.Id, new AccountWithdrawalRequest(depositAmountAcc2));

			_accountService.TransactionBetweenAccounts(account1.Id, account2.Id, new AccountDepositRequest(transferAmount));

			var accountStatementAccount1 = _accountService.GetAccountStatement(account1.Id);
			var accountStatementAccount2 = _accountService.GetAccountStatement(account2.Id);

			CollectionAssert.AreEqual(expectedForAccount1, accountStatementAccount1);
			CollectionAssert.AreEqual(expectedForAccount2, accountStatementAccount2);
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

		private static IEnumerable<TestCaseData> TransactionBetweenAccounts_TestData(bool isHappyPath)
		{
			if (isHappyPath)
			{
				yield return new TestCaseData(new AccountCreateRequest.Student(1), new AccountCreateRequest.Student(2), 1000, 1000, 500);
				yield return new TestCaseData(new AccountCreateRequest.Business(1), new AccountCreateRequest.Business(2), 1000, 1000, 500);
				yield return new TestCaseData(new AccountCreateRequest.Giro(), new AccountCreateRequest.Giro(), 1000, 1000, 500);

				yield return new TestCaseData(new AccountCreateRequest.Student(1), new AccountCreateRequest.Business(2), 1000, 1000, 500);
				yield return new TestCaseData(new AccountCreateRequest.Student(1), new AccountCreateRequest.Giro(), 1000, 1000, 500);

				yield return new TestCaseData(new AccountCreateRequest.Business(1), new AccountCreateRequest.Student(2), 1000, 1000, 500);
				yield return new TestCaseData(new AccountCreateRequest.Business(1), new AccountCreateRequest.Giro(), 1000, 1000, 500);

				yield return new TestCaseData(new AccountCreateRequest.Giro(), new AccountCreateRequest.Student(2), 1000, 1000, 500);
				yield return new TestCaseData(new AccountCreateRequest.Giro(), new AccountCreateRequest.Business(2), 1000, 1000, 500);
			}
			else
			{
				//Send more than stored on the deposit
				yield return new TestCaseData(new AccountCreateRequest.Student(1), new AccountCreateRequest.Student(2), 1000, 1000, 1500);
				yield return new TestCaseData(new AccountCreateRequest.Business(1), new AccountCreateRequest.Business(2), 1000, 1000, 1500);
				yield return new TestCaseData(new AccountCreateRequest.Giro(), new AccountCreateRequest.Giro(), 1000, 1000, 1500);
			}
		}
		#endregion
	}
}
