using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using System.Collections.Generic;

namespace BankKata.Contracts.Storages
{
	public class TransactionInMemoryRepository : ITransactionStorage
	{
		private readonly IClock _clock;
		private readonly List<Transaction> _transactions = new List<Transaction>();

		public TransactionInMemoryRepository(IClock clock)
		{
			_clock = clock;
		}

		public void Add(int amount)
		{
			var transaction = new Transaction(_clock.DateTimeNowAsString(), amount);
			_transactions.Add(transaction);
		}

		public List<Transaction> AllTransactions()
		{
			return _transactions;
		}
	}
}
