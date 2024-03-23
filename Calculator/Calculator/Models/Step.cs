using System;
namespace Calculator.Models
{
	public class Step
	{
		public decimal Val { get; }

		public Operation Operation { get; }

		public Step Next { get; }

		public Step(decimal val, Operation operation = Operation.STOP, Step next = null)
		{
			Val = val;
			Operation = operation;
			Next = next;
		}
	}
}

