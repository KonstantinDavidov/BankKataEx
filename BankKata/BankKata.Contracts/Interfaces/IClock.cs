using System;

namespace BankKata.Contracts.Interfaces
{
	public interface IClock
	{
		string DateTimeNowAsString();
		DateTime GetDate();
	}
}
