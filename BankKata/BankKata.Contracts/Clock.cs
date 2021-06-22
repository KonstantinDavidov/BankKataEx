using BankKata.Contracts.Interfaces;
using System;
using System.Globalization;

namespace BankKata.Contracts
{
	public class Clock : IClock
	{
		protected virtual DateTime DateNow => DateTime.UtcNow;

		public string DateTimeNowAsString()
		{
			return DateNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
		}

		public DateTime GetDate()
		{
			return DateNow;
		}
	}
}
