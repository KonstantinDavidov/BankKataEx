using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BankAccount.Common;
using BankKata.Business.Interfaces;
using BankKata.Business.Models;

namespace BankKata.Business.Formatters
{
	public class StatementPrinter : IStatementPrinter
	{
		private readonly IOutputWriter _outputWriter;
		private int _runningBalance = 0;

		public StatementPrinter(IOutputWriter outputWriter)
		{
			_outputWriter = outputWriter;
		}

		public void Print(IEnumerable<Transaction> transactions)
		{
			_outputWriter.Write(Constants.StatementHeader);
			PrintStatementLines(transactions);
		}

		public IEnumerable<string> GetStatementLines(IEnumerable<Transaction> transactions)
		{
			var statementLines = new List<string> { Constants.StatementHeader };
			statementLines.AddRange(GetRevertedStatements(transactions));

			return statementLines;
		}

		private void PrintStatementLines(IEnumerable<Transaction> transactions)
		{
			GetRevertedStatements(transactions)
				.ForEach(_outputWriter.Write);
		}

		private string StatementLine(Transaction transaction)
		{
			Interlocked.Add(ref _runningBalance, transaction.Amount);

			return
				$"{transaction.Date} || {transaction.Amount} || {_runningBalance}";
		}

		private List<string> GetRevertedStatements(IEnumerable<Transaction> transactions)
		{
			return transactions
				.Select(StatementLine)
				.Reverse()
				.ToList();
		}
	}
}
