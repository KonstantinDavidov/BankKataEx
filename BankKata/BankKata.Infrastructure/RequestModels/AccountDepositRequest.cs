namespace BankKata.Infrastructure.RequestModels
{
	public class AccountDepositRequest
	{
		public int Amount { get; set; }

		public AccountDepositRequest()
		{
		}

		public AccountDepositRequest(int amount)
		{
			Amount = amount;
		}
	}
}
