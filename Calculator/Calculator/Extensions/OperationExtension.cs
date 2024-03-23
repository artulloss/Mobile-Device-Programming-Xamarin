using System;
using Calculator.Models;

namespace Calculator.Extensions
{
	public static class OperationExtension
	{
		public static decimal Apply(this Operation operation, decimal x, decimal y)
		{
            switch (operation)
            {
                case Operation.STOP:
                    throw new Exception("The stop operation should not be applied and merely indicates the end");
                case Operation.ADDITION:
                    return x + y;
                case Operation.SUBTRACTION:
                    return x - y;
                case Operation.MULTIPLICATION:
                    return x * y;
                case Operation.DIVISION:
                    if (y == 0)
                        throw new DivideByZeroException("Cannot divide by zero.");
                    return x / y;
            }
            throw new Exception("Invalid operation"); // Never happens
        }
	}
}

