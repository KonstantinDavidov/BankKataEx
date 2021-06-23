using BankKata.Contracts.Interfaces.Storages;
using BankKata.Contracts.Models;
using System;
using System.Collections.Generic;
using BankKata.Contracts.Exceptions;

namespace BankKata.Contracts.Storages
{
	public class BankAccountStorage : IBankAccountStorage
	{
		private readonly Dictionary<int, Account> _accounts = new Dictionary<int, Account>();

		public void Add(Account account)
		{
			if (account == null)
			{
				throw new ArgumentNullException(nameof(account));
			}

			var isSuccess = _accounts.TryAdd(account.Id, account);
			if (!isSuccess)
			{
				throw new InvalidOperationException("Account already exists.");
			}
		}

		public Account GetById(int id)
		{
			_accounts.TryGetValue(id, out var account);
			if (account == null)
			{
				throw new EntityNotFoundException($"Account with Id = {id} was not found.");
			}

			return account;
		}

		/// <summary>
		/// Generate Id for a new entity, that should be used to create a new account.
		/// </summary>
		public int GetNextId()
		{
			return _accounts.Count + 1;
		}
	}
}
