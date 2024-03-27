using Calculator.Extensions;

namespace Calculator.Models
{
	public class Calculation
	{
		public Step FirstStep;

		public Step LastStep {
			get {
				Step cur = FirstStep;
				while(cur != null && cur.Next != null)
				{
					cur = FirstStep.Next;
				}
				return cur;
			}
		}

		public Calculation(Step firstStep)
		{
			FirstStep = firstStep;
		}

		public decimal ComputeSolution()
		{
			decimal result = FirstStep.Val;

			Step cur = FirstStep;
			Step next = FirstStep.Next;

			while (next != null && cur.Operation != Operation.STOP)
			{
				result = cur.Operation.Apply(result, next.Val);
				cur = cur.Next;
				next = next.Next;
			}

			return result;
        }

		public Calculation AddStep(Step step)
		{
			Step cur = FirstStep;

			while (cur.Next != null && cur.Next.Operation != Operation.STOP)
			{
				cur = cur.Next;
			}

			// Cur is at the last one

			cur.Next = step;

			return this;
		}

		public Calculation ModifyStep(Step oldStep, Step newStep)
		{
			Step cur = FirstStep;

			while (cur != null)
			{
				if(cur.Equals(oldStep))
				{
					cur.Operation = newStep.Operation;
					cur.Val = newStep.Val;
				}
				cur = cur.Next;
			}

			return this;
		}

	}
}

