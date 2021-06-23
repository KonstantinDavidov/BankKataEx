using System;
using System.Collections.Generic;
using System.Text;
using BankKata.Contracts.Models;

namespace BankKata.Contracts.Interfaces.Storages
{
	public interface IBankAccountStorage
	{
		void Add(Account account);
		Account GetById(int id);
		int GetNextId();
	}
}
