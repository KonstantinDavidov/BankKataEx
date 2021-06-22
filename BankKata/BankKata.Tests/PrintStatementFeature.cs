using BankKata.Contracts.Formatters;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using BankKata.Contracts.Storages;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests
{
	public class Tests
	{
		public class PrintStatementFeature
		{
			[Test]
			public void PrintStatementBaseTest()
			{
				var clockMock = new Mock<IClock>();
				clockMock.SetupSequence(x => x.DateTimeNowAsString())
					.Returns("10/01/2012")
					.Returns("13/01/2012")
					.Returns("14/01/2012");

				var transactionRep = new TransactionInMemoryRepository(clockMock.Object);
				var outputWriter = new Mock<IOutputWriter>();
				var statement = new StatementPrinter(outputWriter.Object);
				var account = new StudentAccount(1, transactionRep, statement);

				account.Deposit(1000);
				account.Deposit(2000);
				account.Withdraw(500);

				account.PrintStatement();

				outputWriter.Verify(x => x.Write("Date || Amount || Balance"));
				outputWriter.Verify(x => x.Write("14/01/2012 || -500 || 2500"));
				outputWriter.Verify(x => x.Write("13/01/2012 || 2000 || 3000"));
				outputWriter.Verify(x => x.Write("10/01/2012 || 1000 || 1000"));
			}
		}
	}
}