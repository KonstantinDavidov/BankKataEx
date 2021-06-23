using BankAccount.Common;
using BankKata.Business.Enums;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;

namespace BankKata.Business.Models
{
	/// <summary>
	/// Giro: max 4,000.00 negative balance
	/// </summary>
	public class GiroAccount : Account
	{
		protected override int MinAllowedBalance => Constants.MinAllowedGiroAccountBalance;
		public override AccountType AccountType => AccountType.Giro;

		public GiroAccount(int accountId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(accountId, transactionStorage, statementPrinter)
		{
		}
	}
}
