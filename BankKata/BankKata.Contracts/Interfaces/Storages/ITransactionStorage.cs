using BankKata.Contracts.Models;
using System.Collections.Generic;

namespace BankKata.Contracts.Interfaces.Storages
{
	public interface ITransactionStorage
	{
		void Add(int amount);
		List<Transaction> AllTransactions();
	}
}
