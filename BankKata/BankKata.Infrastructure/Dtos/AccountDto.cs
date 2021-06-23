using System;
using BankKata.Business.Enums;
using BankKata.Business.Models;

namespace BankKata.Infrastructure.Dtos
{
	public class AccountDto
	{
		public int Id { get; set; }
		public AccountType AccountType { get; set; }

		public AccountDto(Account account)
		{
			if (account == null)
				throw new ArgumentNullException(nameof(account));

			Id = account.Id;
			AccountType = account.AccountType;
		}
	}
}
