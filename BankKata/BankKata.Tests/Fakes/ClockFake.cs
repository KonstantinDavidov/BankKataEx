using System;
using BankKata.Business;

namespace BankKata.Tests.Fakes
{
	public class ClockFake : Clock
	{
		protected override DateTime DateNow => new DateTime(2099, 12, 07);
	}
}
