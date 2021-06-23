using System.Collections.Generic;
using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;

namespace BankKata.Infrastructure.Contracts
{
	public interface IAccountService
	{
		public AccountDto Create(AccountCreateRequest createRequest);
		void WithdrawalFromAccount(int accountId, AccountWithdrawalRequest withdrawalRequest);
		int GetAccountBalance(int accountId);
		void DepositToAccount(int accountId, AccountDepositRequest depositRequest);
		void TransactionBetweenAccounts(int accountIdFrom, int accountIdTo, AccountDepositRequest depositRequest);
		IEnumerable<string> GetAccountStatement(int accountId);
	}
}
