using BankKata.Contracts.Models;
using System.Collections.Generic;

namespace BankKata.Contracts.Interfaces.Storages
{
	public interface ITransactionStorage
	{
		int Add(int amount);
		Transaction GetById(int id);
		List<Transaction> AllTransactions();
		bool DeleteById(Transaction transaction);
	}
}
