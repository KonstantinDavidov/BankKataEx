using BankKata.Contracts.Enums;
using BankKata.Contracts.Models;

namespace BankKata.Contracts.Interfaces
{
	public interface IAccountCreationFabric
	{
		Account CreateAccount(int accountId, AccountType type, int? entityId);
	}
}
