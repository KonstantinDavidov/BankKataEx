using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Enums;

namespace BankKata.Infrastructure.RequestModels
{
	public class AccountCreateRequest
	{
		public AccountType AccountType { get; }
		public int? EntityId { get; set; }
	}
}
