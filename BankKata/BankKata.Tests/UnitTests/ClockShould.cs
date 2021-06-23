using BankKata.Tests.Fakes;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public class ClockShould
	{
		[Test]
		public void Clock_should_return_today_date_in_dd_MM_YYYY()
		{
			var clockMoq = new ClockFake();
			var date = clockMoq.DateTimeNowAsString();

			Assert.AreEqual("07/12/2099", date);
		}
	}
}
