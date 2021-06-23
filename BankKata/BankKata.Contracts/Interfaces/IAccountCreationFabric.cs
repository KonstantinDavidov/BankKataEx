using BankKata.Business.Enums;
using BankKata.Business.Models;

namespace BankKata.Business.Interfaces
{
	public interface IAccountCreationFabric
	{
		Account CreateAccount(int accountId, AccountType type, int? entityId);
	}
}
