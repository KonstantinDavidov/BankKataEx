using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Business;
using BankKata.Business.Enums;
using BankKata.Business.Formatters;
using BankKata.Business.Interfaces;
using BankKata.Business.Models;
using BankKata.Business.Storages;
using BankKata.Business.Writers;

namespace BankKata.Infrastructure.Fabrics
{
	/// <summary>
	/// Simple account fabric that is responsible for creation different types of account.
	/// This fabric might be more complex if we need to support different types of transaction stores and statement printers,
	/// but for now I created a simple fabric without over engineering it.
	/// </summary>
	public class AccountCreationFabric : IAccountCreationFabric
	{
		public Account CreateAccount(int accountId, AccountType type, int? entityId)
		{
			var transactionStore = new TransactionInMemoryRepository(new Clock());
			var statementPrinter = new StatementPrinter(new ConsoleOutputWriter());

			switch (type)
			{
				case AccountType.Student:
				{
					if (!entityId.HasValue)
						throw new InvalidOperationException("Student Id should be provided.");

					return new StudentAccount(entityId.Value, accountId, transactionStore, statementPrinter);
				}
				case AccountType.Business:
				{
					if (!entityId.HasValue)
						throw new InvalidOperationException("Business Id should be provided.");

					return new BusinessAccount(entityId.Value, accountId, transactionStore, statementPrinter);
				}
				case AccountType.Giro:
				{
					if (entityId.HasValue)
						throw new InvalidOperationException("Giro Account should not have EntityId.");

					return new GiroAccount(accountId, transactionStore, statementPrinter);
				}

				case AccountType.None:
					throw new InvalidOperationException("None type of account is not allowed for creation.");
				default:
					throw new InvalidOperationException("Unknown type of account.");
			}
		}
	}
}
