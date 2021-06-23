using System;
using BankKata.Business.Interfaces;

namespace BankKata.Business.Writers
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
