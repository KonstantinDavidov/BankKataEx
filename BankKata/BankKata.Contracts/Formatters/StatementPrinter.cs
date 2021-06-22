using BankAccount.Common;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BankKata.Contracts.Formatters
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
			return GetRevertedStatements(transactions);
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
