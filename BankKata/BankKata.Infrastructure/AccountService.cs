using BankKata.Contracts.Enums;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Storages;
using BankKata.Infrastructure.Contracts;
using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;
using System;
using BankKata.Contracts.Exceptions;

namespace BankKata.Infrastructure
{
	public class AccountService : IAccountService
	{
		private readonly IBankAccountStorage _bankAccountStorage;
		private readonly IAccountCreationFabric _accountCreationFabric;
		private readonly object _lock = new object();

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

		public void TransactionBetweenAccounts(int accountIdFrom, int accountIdTo, AccountDepositRequest depositRequest)
		{
			if (depositRequest == null)
				throw new ArgumentNullException(nameof(depositRequest));

			if (depositRequest.Amount <= 0)
				throw new InvalidOperationException("Deposit amount should be a positive number.");

			var accountFrom = _bankAccountStorage.GetById(accountIdFrom);
			var accountTo = _bankAccountStorage.GetById(accountIdTo);

			if (!accountFrom.IsWithdrawAllowed(depositRequest.Amount))
			{
				throw new WithdrawNotAllowedException("Withdraw is not allowed.");
			}

			if (!accountTo.IsDepositAllowed(depositRequest.Amount))
			{
				throw new DepositNotAllowedException("Deposit is not allowed.");
			}

			if (!accountFrom.IsTransferToOtherAccountAllowed(depositRequest.Amount))
			{
				throw new WithdrawNotAllowedException("Sending money to other account is not allowed.");
			}

			//In real application these operations should be wrapped by a transaction if we store values in the database.
			//there could be a separate strategy-class that is responsible for this logic, but I think it would be better to keep it simple for now.
			//
			//In real app. it can use "Software Transactional Memory" approach to do in-memory operation without locks,
			//this will help to avoid potential deadlocks.
			//But since I have a simple app., the AccountService will be injected using "AddScoped",
			//which means that a new instance of AccountService will be created for each request.
			//By this lock() and simple version of rollback, I wanted to show that transferring money to other account is much more complex operation
			//than just Withdraw + Deposit.
			int? accountFromWithdrawTransactionId = null;
			int? accountToDepositTransactionId = null;
			try
			{
				lock (_lock)
				{
					accountFromWithdrawTransactionId = accountFrom.Withdraw(depositRequest.Amount);
					accountToDepositTransactionId = accountTo.Deposit(depositRequest.Amount);
				}
			}
			catch (Exception e)
			{
				if(accountFromWithdrawTransactionId.HasValue)
					accountFrom.WithdrawRollback(accountFromWithdrawTransactionId.Value);

				if (accountToDepositTransactionId.HasValue)
					accountFrom.DepositRollback(accountToDepositTransactionId.Value);

				//we can log exception to file here.

				throw;
			}
		}
	}
}
