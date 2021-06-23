using System.Collections.Generic;
using BankKata.Business.Models;

namespace BankKata.Business.Interfaces.Storages
{
	public interface ITransactionStorage
	{
		int Add(int amount);
		Transaction GetById(int id);
		List<Transaction> AllTransactions();
		bool DeleteById(Transaction transaction);
	}
}
