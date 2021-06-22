using System;

namespace BankKata.Contracts.Exceptions
{
	public class DepositNotAllowedException : Exception
	{
		public DepositNotAllowedException()
		{
		}

		public DepositNotAllowedException(string message)
			: base(message)
		{
		}

		public DepositNotAllowedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
