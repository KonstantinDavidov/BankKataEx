namespace BankKata.Contracts.Models
{
	public class Transaction
	{
		private readonly string _date;
		private readonly int _amount;

		public string Date => _date;
		public int Amount => _amount;

		public Transaction(string dateStr, int amount)
		{
			_date = dateStr;
			_amount = amount;
		}
	}
}
