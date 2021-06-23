using System;

namespace BankAccount.Common
{
	public static class Constants
	{
		public static string DateTodayStr = "09/03/2092";
		public static string StatementHeader = "Date || Amount || Balance";
		public static int MinAllowedBusinessAccountBalance = -100000;
		public static int MinAllowedGiroAccountBalance = -4000;
	}
}
