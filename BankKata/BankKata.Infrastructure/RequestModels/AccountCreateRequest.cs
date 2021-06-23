using BankKata.Business.Enums;

namespace BankKata.Infrastructure.RequestModels
{
	public abstract class AccountCreateRequest
	{
		public abstract AccountType AccountType { get; }
		public int? EntityId { get; set; }

		public class Business : AccountCreateRequest
		{
			public override AccountType AccountType => AccountType.Business;

			public Business()
			{
			}

			public Business(int? entityId)
			{
				EntityId = entityId;
			}
		}

		public class Student : AccountCreateRequest
		{
			public override AccountType AccountType => AccountType.Student;

			public Student()
			{
			}

			public Student(int? entityId)
			{
				EntityId = entityId;
			}
		}

		public class Giro : AccountCreateRequest
		{
			public override AccountType AccountType => AccountType.Giro;
		}
	}
}
