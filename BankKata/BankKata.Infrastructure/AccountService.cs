﻿using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Enums;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Interfaces.Services;
using BankKata.Contracts.Interfaces.Storages;
using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;

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
	}
}