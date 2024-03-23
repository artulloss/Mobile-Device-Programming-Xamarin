using System;
using Calculator.Models;
using Calculator.Extensions;

namespace Calculator.ViewModels
{
	public class Calculation
	{
		public Step FirstStep;

		public Calculation(Step firstStep)
		{
			FirstStep = firstStep;
		}

		public decimal ComputeSolution()
		{
            if (FirstStep == null)
			{
                throw new InvalidOperationException("Calculation steps cannot be null.");
            }

			decimal result = FirstStep.Val;
			Step cur = FirstStep.Next;

			while (cur != null && cur.Operation != Operation.STOP)
			{
				result = cur.Operation.Apply(result, cur.Val);
				cur = cur.Next;
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


	}
}

