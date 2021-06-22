using BankAccount.Common;
using BankKata.Contracts.Interfaces;

namespace BankKata.Contracts.Models
{
	/// <summary>
	/// Business 100,000.00
	/// When creating a business account, a business id number must be provided
	/// </summary>
	public class BusinessAccount : Account
	{
		protected override int MaxAllowedBalance => Constants.MaxAllowedBusinessAccountBalance;

		public int BusinessId { get; }

		public BusinessAccount(int businessId, ITransactionStorage transactionStorage, IStatementPrinter statementPrinter)
			: base(transactionStorage, statementPrinter)
		{
			BusinessId = businessId;
		}
	}
}
