namespace BankKata.Infrastructure.RequestModels
{
	public class AccountWithdrawalRequest
	{
		public int Amount { get; set; }

		public AccountWithdrawalRequest(int amount)
		{
			Amount = amount;
		}

		public AccountWithdrawalRequest()
		{
		}
	}
}
