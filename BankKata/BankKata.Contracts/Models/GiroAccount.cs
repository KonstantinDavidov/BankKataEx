using BankAccount.Common;
using BankKata.Contracts.Enums;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Giro 4,000.00
	/// </summary>
	public class GiroAccount : Account
	{
		protected override int MaxAllowedBalance => Constants.MaxAllowedGiroAccountBalance;
		public override AccountType AccountType => AccountType.Giro;

		public GiroAccount(int accountId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(accountId, transactionStorage, statementPrinter)
		{
		}
	}
}
