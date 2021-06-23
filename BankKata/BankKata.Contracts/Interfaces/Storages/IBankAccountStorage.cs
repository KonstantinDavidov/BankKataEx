using BankKata.Business.Models;

namespace BankKata.Business.Interfaces.Storages
{
	public interface IBankAccountStorage
	{
		void Add(Account account);
		Account GetById(int id);
		int GetNextId();
	}
}
