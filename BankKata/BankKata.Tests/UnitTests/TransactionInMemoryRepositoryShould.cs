using System.Linq;
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
		public void Create_and_store_a_deposit_transaction()
		{
			const int depositAmount = 12345;
			_repository.Add(depositAmount);
			var result = _repository.AllTransactions();

			Assert.AreEqual(1, result.Count);
			var currentTransaction = result[0];

			var actual = TransactionBuilder.Transaction()
				.With(_clockMoq.Object.DateTimeNowAsString())
				.With(depositAmount)
				.Build(1);

			Assert.AreEqual(currentTransaction, actual);
		}

		[Test]
		public void Create_and_store_a_withdraw_transaction()
		{
			const int withdrawAmount = -12345;
			_repository.Add(withdrawAmount);
			var result = _repository.AllTransactions();

			Assert.AreEqual(1, result.Count);

			var expected = result[0];
			var actual = TransactionBuilder.Transaction()
				.With(_clockMoq.Object.DateTimeNowAsString())
				.With(withdrawAmount)
				.Build(1);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Create_should_increment_id()
		{
			const int withdrawAmount = -12345;
			_repository.Add(withdrawAmount);
			_repository.Add(withdrawAmount);
			_repository.Add(withdrawAmount);

			var result = _repository.AllTransactions().OrderBy(x => x.Id).ToList();

			Assert.AreEqual(1, result[0].Id);
			Assert.AreEqual(2, result[1].Id);
			Assert.AreEqual(3, result[2].Id);
		}
	}
}
