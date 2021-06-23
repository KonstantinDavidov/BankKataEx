using NUnit.Framework;
using System;
using BankKata.Business.Exceptions;
using BankKata.Business.Storages;

namespace BankKata.Tests.UnitTests
{
	public class AccountStorageShould
	{
		private BankAccountStorage _storage;

		[SetUp]
		public void SetUp()
		{
			_storage = new BankAccountStorage();
		}

		[Test]
		public void Should_throw_when_add_null()
		{
			Assert.Throws<ArgumentNullException>(() => _storage.Add(null));
		}

		[Test]
		public void Should_throw_when_get_not_existed_account()
		{
			Assert.Throws<EntityNotFoundException>(() => _storage.GetById(5));
		}

		[Test]
		public void Should_add_new_account()
		{
			Assert.Throws<ArgumentNullException>(() => _storage.Add(null));
		}
	}
}
