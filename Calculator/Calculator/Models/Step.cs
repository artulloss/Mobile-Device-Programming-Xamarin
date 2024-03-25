namespace Calculator.Models
{
	public class Step
	{
		public decimal Val { get; set; }

		public Operation Operation { get; set; }

		public Step Next { get; set; }

		public Step(decimal val, Operation operation = Operation.STOP, Step next = null)
		{
			Val = val;
			Operation = operation;
			Next = next;
		}
	}
}

