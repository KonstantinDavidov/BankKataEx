using BankAccount.Common;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Giro 4,000.00
	/// </summary>
	public class GiroAccount : Account
	{
		protected override int MaxAllowedBalance => Constants.MaxAllowedGiroAccountBalance;

		public GiroAccount(ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(transactionStorage, statementPrinter)
		{
		}
	}
}
