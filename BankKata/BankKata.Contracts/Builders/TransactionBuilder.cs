using BankKata.Business.Models;

namespace BankKata.Business.Builders
{
	public class TransactionBuilder
	{
		private string _dateStr;
		private int _amount;

		public static TransactionBuilder Transaction()
		{
			return new TransactionBuilder();
		}

		public TransactionBuilder With(string dateStr)
		{
			_dateStr = dateStr;
			return this;
		}

		public TransactionBuilder With(int amount)
		{
			_amount = amount;
			return this;
		}

		public Transaction Build(int id)
		{
			return new Transaction(id, _dateStr, _amount);
		}
	}
}
