using BankAccount.Common;
using BankKata.Contracts.Formatters;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BankKata.Tests.UnitTests
{
	public class StatementPrinterShould
	{
		private Mock<IOutputWriter> _outputWriterMock;
		private readonly List<Transaction> _emptyCollectionTransactions = Enumerable.Empty<Transaction>().ToList();

		[SetUp]
		public void SetUp()
		{
			_outputWriterMock = new Mock<IOutputWriter>();
		}

		[Test]
		public void Should_always_print_header()
		{
			var statement = new StatementPrinter(_outputWriterMock.Object);

			statement.Print(_emptyCollectionTransactions);

			_outputWriterMock.Verify(x => x.Write(Constants.StatementHeader));
		}
	}
}
