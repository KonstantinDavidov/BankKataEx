using System.Collections.Generic;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;
using BankKata.Business.Models;

namespace BankKata.Business.Storages
{
	public class TransactionInMemoryRepository : ITransactionStorage
	{
		private readonly IClock _clock;
		private readonly List<Transaction> _transactions = new List<Transaction>();

		public TransactionInMemoryRepository(IClock clock)
		{
			_clock = clock;
		}

		public int Add(int amount)
		{
			var transaction = new Transaction(GetNextId(), _clock.DateTimeNowAsString(), amount);
			_transactions.Add(transaction);

			return transaction.Id;
		}

		public Transaction GetById(int id)
		{
			return _transactions.Find(x => x.Id == id);
		}

		public bool DeleteById(Transaction transaction)
		{
			return _transactions.Remove(transaction);
		}

		public List<Transaction> AllTransactions()
		{
			return _transactions;
		}

		private int GetNextId()
		{
			return _transactions.Count + 1;
		}
	}
}
