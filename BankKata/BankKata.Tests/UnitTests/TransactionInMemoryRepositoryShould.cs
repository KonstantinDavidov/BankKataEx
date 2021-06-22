using BankAccount.Common;
using BankKata.Contracts.Builders;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Storages;
using Moq;
using NUnit.Framework;

namespace BankKata.Tests.UnitTests
{
	public class TransactionInMemoryRepositoryShould
	{
		private Mock<IClock> _clockMoq;
		private TransactionInMemoryRepository _repository;

		[SetUp]
		public void SetUp()
		{
			_clockMoq = new Mock<IClock>();
			_clockMoq.Setup(clock => clock.DateTimeNowAsString()).Returns(Constants.DateTodayStr);
			_repository = new TransactionInMemoryRepository(_clockMoq.Object);
		}

		[Test]
		public void Create_And_Store_A_Deposit_Transaction()
		{
			_repository.Add(12345);
			var result = _repository.AllTransactions();

			Assert.AreEqual(1, result.Count);
			var currentTransaction = result[0];

			var actual = TransactionBuilder.Transaction()
				.With(_clockMoq.Object.DateTimeNowAsString())
				.With(12345)
				.Build();

			Assert.AreEqual(currentTransaction, actual);
		}

		[Test]
		public void Create_And_Store_A_Withdraw_Transaction()
		{
			_repository.Add(-12345);
			var result = _repository.AllTransactions();

			Assert.AreEqual(1, result.Count);

			var expected = result[0];
			var actual = TransactionBuilder.Transaction()
				.With(_clockMoq.Object.DateTimeNowAsString())
				.With(-12345)
				.Build();

			Assert.AreEqual(expected, actual);
		}
	}
}
