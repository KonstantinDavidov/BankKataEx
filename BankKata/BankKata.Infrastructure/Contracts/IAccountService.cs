using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;

namespace BankKata.Contracts.Interfaces.Services
{
	public interface IAccountService
	{
		public AccountDto Create(AccountCreateRequest createRequest);
	}
}
