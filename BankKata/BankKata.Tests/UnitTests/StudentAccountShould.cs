using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Interfaces;
using BankKata.Contracts.Models;
using Moq;

namespace BankKata.Tests.UnitTests
{
	public class StudentAccountShould : BaseAccountTests
	{

		protected override Account CreateAccountEntity(Mock<ITransactionStorage> transactionMoq)
		{
			return new StudentAccount(1, transactionMoq.Object);
		}
	}
}
