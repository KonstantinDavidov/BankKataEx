using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;

namespace BankKata.Infrastructure.Contracts
{
	public interface IAccountService
	{
		public AccountDto Create(AccountCreateRequest createRequest);
		void WithdrawalFromAccount(int accountId, AccountWithdrawalRequest withdrawalRequest);
		int GetAccountBalance(int accountId);
	}
}
