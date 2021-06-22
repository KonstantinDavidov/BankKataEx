﻿using System;

namespace BankKata.Contracts.Models
{
	public class Transaction
	{
		private readonly string _date;
		private readonly int _amount;

		public string Date => _date;
		public int Amount => _amount;

		public Transaction(string dateStr, int amount)
		{
			_date = dateStr;
			_amount = amount;
		}

		protected bool Equals(Transaction other)
		{
			return _date == other._date && _amount == other._amount;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Transaction)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(_date, _amount);
		}
	}
}
