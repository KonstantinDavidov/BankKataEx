using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Models;

namespace BankKata.Contracts.Interfaces
{
	public interface ITransactionStorage
	{
		void Add(int amount);
		List<Transaction> AllTransactions();
	}
}
