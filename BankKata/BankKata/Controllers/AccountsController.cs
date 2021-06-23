using BankKata.Infrastructure.Contracts;
using BankKata.Infrastructure.Dtos;
using BankKata.Infrastructure.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankKata.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountService _bankAccountService;

		public AccountsController(IAccountService bankAccountService)
		{
			_bankAccountService = bankAccountService;
		}

		/// <summary>
		/// Creates a new Business Account.
		/// </summary>
		/// <param name="createRequest">Request object that contains parameters for create a new account.</param>
		/// <returns>Returns newly created account.</returns>
		[HttpPost("business")]
		public AccountDto CreateBusinessAccount([FromBody] AccountCreateRequest.Business createRequest)
		{
			return _bankAccountService.Create(createRequest);
		}

		/// <summary>
		/// Creates a new Student Account.
		/// </summary>
		/// <param name="createRequest">Request object that contains parameters for create a new account.</param>
		/// <returns>Returns newly created account.</returns>
		[HttpPost("student")]
		public AccountDto CreateStudentAccount([FromBody] AccountCreateRequest.Student createRequest)
		{
			return _bankAccountService.Create(createRequest);
		}

		/// <summary>
		/// Creates a new Giro Account.
		/// </summary>
		/// <returns>Returns newly created account.</returns>
		[HttpPost("giro")]
		public AccountDto CreateGiroAccount()
		{
			return _bankAccountService.Create(new AccountCreateRequest.Giro());
		}

		/// <summary>
		/// Gets total balance by account id.
		/// </summary>
		/// <param name="id">Account Id</param>
		/// <returns>Total Balance</returns>
		[HttpGet("{id}/balance")]
		public int GetAccountBalance([FromRoute] int id)
		{
			return _bankAccountService.GetAccountBalance(id);
		}

		/// <summary>
		/// Executes Withdrawal operation.
		/// </summary>
		/// <param name="id">Account Id</param>
		/// <param name="withdrawalRequest">Withdrawal operation request object that defines parameters.</param>
		/// <returns>Returns status 200 if operation completes successfully</returns>
		[HttpPut("{id}/withdrawal")]
		public IActionResult Withdraw([FromRoute] int id, [FromBody] AccountWithdrawalRequest withdrawalRequest)
		{
			_bankAccountService.WithdrawalFromAccount(id, withdrawalRequest);

			return Ok();
		}

		/// <summary>
		/// Executes Deposit operation.
		/// </summary>
		/// <param name="id">Account Id</param>
		/// <param name="depositRequest">Deposit operation request object that defines parameters.</param>
		/// <returns>Returns status 200 if operation completes successfully</returns>
		[HttpPut("{id}/deposit")]
		public IActionResult Deposit([FromRoute] int id, [FromBody] AccountDepositRequest depositRequest)
		{
			_bankAccountService.DepositToAccount(id, depositRequest);

			return Ok();
		}

		/// <summary>
		/// Transfer money to other account.
		/// </summary>
		/// <param name="accountFromId">Account Id that you want to transfer money from.</param>
		/// <param name="accountToId">Account Id that will accept money from other account.</param>
		/// <param name="depositRequest">Request object that defines parameters for the request.</param>
		/// <returns>Returns status 200 if operation completes successfully</returns>
		[HttpPut("{accountFromId}/deposit/{accountToId}")]
		public IActionResult DepositToAnotherAccount(
			[FromRoute] int accountFromId,
			[FromRoute] int accountToId,
			[FromBody] AccountDepositRequest depositRequest
		)
		{
			_bankAccountService.TransactionBetweenAccounts(accountFromId, accountToId, depositRequest);

			return Ok();
		}

		/// <summary>
		/// Returns a list of strings that defines Account Statement document.
		/// </summary>
		/// <param name="id">Account Id</param>
		/// <returns>List of statement records in String format.</returns>
		[HttpGet("{id}/statement")]
		public IEnumerable<string> GetAccountStatement([FromRoute] int id)
		{
			return _bankAccountService.GetAccountStatement(id);
		}
	}
}
