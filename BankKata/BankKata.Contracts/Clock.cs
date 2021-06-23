using System;
using System.Globalization;
using BankKata.Business.Interfaces;

namespace BankKata.Business
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
