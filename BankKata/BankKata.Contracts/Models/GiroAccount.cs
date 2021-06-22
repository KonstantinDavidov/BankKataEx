using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Giro 4,000.00
	/// </summary>
	public class GiroAccount : Account
	{
		public GiroAccount(ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(transactionStorage, statementPrinter)
		{
		}
	}
}
