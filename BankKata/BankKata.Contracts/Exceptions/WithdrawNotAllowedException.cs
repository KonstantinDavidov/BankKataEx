using System;

namespace BankKata.Contracts.Exceptions
{
	public class WithdrawNotAllowedException : Exception
	{
		public WithdrawNotAllowedException()
		{
		}

		public WithdrawNotAllowedException(string message)
			: base(message)
		{
		}

		public WithdrawNotAllowedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
