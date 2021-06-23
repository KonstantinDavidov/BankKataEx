using BankKata.Business.Formatters;
using BankKata.Business.Interfaces;
using BankKata.Business.Models;
using BankKata.Business.Storages;
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
				var account = new StudentAccount(1, 1, transactionRep, statement);

				account.Deposit(1000);
				account.Deposit(2000);
				account.Withdraw(500);

				account.PrintStatement();

				outputWriter.Verify(x => x.Write("Date || Amount || Balance"));
				outputWriter.Verify(x => x.Write("14/01/2012 || -500 || 2500"));
				outputWriter.Verify(x => x.Write("13/01/2012 || 2000 || 3000"));
				outputWriter.Verify(x => x.Write("10/01/2012 || 1000 || 1000"));
			}

			[Test]
			public void PrintStatement_rollbackTransactionsTest()
			{
				var clockMock = new Mock<IClock>();
				clockMock.SetupSequence(x => x.DateTimeNowAsString())
					.Returns("10/01/2012")
					.Returns("13/01/2012")
					.Returns("14/01/2012")
					.Returns("14/09/2012");

				var transactionRep = new TransactionInMemoryRepository(clockMock.Object);
				var outputWriter = new Mock<IOutputWriter>();
				var statement = new StatementPrinter(outputWriter.Object);
				var account = new StudentAccount(1, 1, transactionRep, statement);

				var depositTransactionToRollback = account.Deposit(1000);
				account.Deposit(2000);
				account.Withdraw(50);
				var withdrawTransactionToRollback = account.Withdraw(20);

				account.DepositRollback(depositTransactionToRollback);
				account.WithdrawRollback(withdrawTransactionToRollback);

				account.PrintStatement();

				outputWriter.Verify(x => x.Write("Date || Amount || Balance"));
				outputWriter.Verify(x => x.Write("14/01/2012 || -50 || 1950"));
				outputWriter.Verify(x => x.Write("13/01/2012 || 2000 || 2000"));
			}
		}
	}
}