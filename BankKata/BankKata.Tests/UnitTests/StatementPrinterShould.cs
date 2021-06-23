using BankAccount.Common;
using BankKata.Contracts.Formatters;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BankKata.Contracts.Builders;

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

		[Test]
		public void Should_always_print_statement_in_reverse_order()
		{
			var statement = new StatementPrinter(_outputWriterMock.Object);

			var transactions = new List<Transaction>
			{
				TransactionBuilder.Transaction().With(1000).With("01/04/2014").Build(1),
				TransactionBuilder.Transaction().With(-100).With("02/04/2014").Build(2),
				TransactionBuilder.Transaction().With(500).With("10/04/2014").Build(3)
			};

			statement.Print(transactions);

			_outputWriterMock.Verify(x => x.Write(Constants.StatementHeader));
			_outputWriterMock.Verify(x => x.Write("10/04/2014 || 500 || 1400"));
			_outputWriterMock.Verify(x => x.Write("02/04/2014 || -100 || 900"));
			_outputWriterMock.Verify(x => x.Write("01/04/2014 || 1000 || 1000"));
		}
	}
}
