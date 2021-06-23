using System;

namespace BankKata.Business.Exceptions
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
