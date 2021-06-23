using BankKata.Contracts.Enums;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Storages;
using BankKata.Infrastructure.Contracts;
using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;
using System;

namespace BankKata.Infrastructure
{
	public class AccountService : IAccountService
	{
		private readonly IBankAccountStorage _bankAccountStorage;
		private readonly IAccountCreationFabric _accountCreationFabric;

		public AccountService(IBankAccountStorage bankAccountStorage, IAccountCreationFabric accountCreationFabric)
		{
			_bankAccountStorage = bankAccountStorage;
			_accountCreationFabric = accountCreationFabric;
		}

		public AccountDto Create(AccountCreateRequest createRequest)
		{
			if (createRequest.AccountType == AccountType.Giro && createRequest.EntityId.HasValue)
			{
				throw new InvalidOperationException("Giro Account should not have EntityId.");
			}

			var newAccount = _accountCreationFabric.CreateAccount(_bankAccountStorage.GetNextId(), createRequest.AccountType, createRequest.EntityId);

			_bankAccountStorage.Add(newAccount);

			return new AccountDto(newAccount);
		}

		public void WithdrawalFromAccount(int accountId, AccountWithdrawalRequest withdrawalRequest)
		{
			if (withdrawalRequest == null)
				throw new ArgumentNullException(nameof(withdrawalRequest));

			if (withdrawalRequest.Amount <= 0)
				throw new InvalidOperationException("Withdrawal amount should be a positive number.");

			var account = _bankAccountStorage.GetById(accountId);

			account.Withdraw(withdrawalRequest.Amount);
		}

		public int GetAccountBalance(int accountId)
		{
			var account = _bankAccountStorage.GetById(accountId);

			return account.Balance;
		}

		public void DepositToAccount(int accountId, AccountDepositRequest depositRequest)
		{
			if (depositRequest == null)
				throw new ArgumentNullException(nameof(depositRequest));

			if (depositRequest.Amount <= 0)
				throw new InvalidOperationException("Deposit amount should be a positive number.");

			var account = _bankAccountStorage.GetById(accountId);

			account.Deposit(depositRequest.Amount);
		}
	}
}
