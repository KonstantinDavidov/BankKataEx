using BankAccount.Common;
using BankKata.Business.Enums;
using BankKata.Business.Interfaces;
using BankKata.Business.Interfaces.Storages;

namespace BankKata.Business.Models
{
	/// <summary>
	/// Business: max 100,000.00 negative balance
	/// When creating a business account, a business id number must be provided
	/// </summary>
	public class BusinessAccount : Account
	{
		protected override int MinAllowedBalance => Constants.MinAllowedBusinessAccountBalance;
		public override AccountType AccountType => AccountType.Business;

		public int BusinessId { get; }

		public BusinessAccount(int businessId, int accountId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(accountId, transactionStorage, statementPrinter)
		{
			BusinessId = businessId;
		}
	}
}
