using System;

namespace Calculator.Extensions
{
	public static class OperationExtension
	{
		public static decimal Apply(this Operation operation, decimal x, decimal y)
		{
            switch (operation)
            {
                case Operation.STOP:
                    return x;
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

        public static int Precedence(this Operation operation)
        {
            return operation switch
            {
                Operation.STOP => 0,
                Operation.ADDITION or Operation.SUBTRACTION => 1,
                Operation.MULTIPLICATION or Operation.DIVISION => 2,
                _ => throw new Exception("Invalid operation"),// Never happens
            };
        }
    }
}

