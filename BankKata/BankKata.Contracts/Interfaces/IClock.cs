using System;

namespace BankKata.Business.Interfaces
{
	public interface IClock
	{
		string DateTimeNowAsString();
		DateTime GetDate();
	}
}
