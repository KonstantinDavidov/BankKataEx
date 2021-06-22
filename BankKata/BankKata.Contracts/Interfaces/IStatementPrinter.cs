using BankKata.Contracts.Models;
using System.Collections.Generic;

namespace BankKata.Contracts.Interfaces
{
	public interface IStatementPrinter
	{
		void Print(IEnumerable<Transaction> transaction);
		IEnumerable<string> GetStatementLines(IEnumerable<Transaction> transactions);
	}
}
