using System.Collections.Generic;
using BankKata.Business.Models;

namespace BankKata.Business.Interfaces
{
	public interface IStatementPrinter
	{
		void Print(IEnumerable<Transaction> transaction);
		IEnumerable<string> GetStatementLines(IEnumerable<Transaction> transactions);
	}
}
