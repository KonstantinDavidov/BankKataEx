using BankKata.Contracts.Exceptions;
using BankKata.Contracts.Storages;
using NUnit.Framework;
using System;

namespace BankKata.Tests.UnitTests
{
	public class AccountStorageShould
	{
		[Test]
		public void Should_throw_when_add_null()
		{
			var storage = new BankAccountStorage();

			Assert.Throws<ArgumentNullException>(() => storage.Add(null));
		}

		[Test]
		public void Should_throw_when_get_not_existed_account()
		{
			var storage = new BankAccountStorage();

			Assert.Throws<EntityNotFoundException>(() => storage.GetById(5));
		}

		[Test]
		public void Should_add_new_account()
		{
			var storage = new BankAccountStorage();

			Assert.Throws<ArgumentNullException>(() => storage.Add(null));
		}
	}
}
