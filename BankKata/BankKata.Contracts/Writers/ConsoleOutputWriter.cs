using BankKata.Contracts.Interfaces;
using System;

namespace BankKata.Contracts.Writers
{
	public sealed class ConsoleOutputWriter : IOutputWriter
	{
		public void Write(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(nameof(text));
			}

			Console.WriteLine(text);
		}
	}
}
