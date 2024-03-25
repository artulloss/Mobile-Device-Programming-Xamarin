using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Calculator.Models;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
	public class MainPageViewModel : BindableObject
	{
		public Calculation Calculation { get; set; }

        // This one isn't bound anywhere so it doesn't need to be in OnPropertyChanged
        private decimal CurrentValueDecimal
		{
			get
			{
				if (decimal.TryParse(CurrentValue, out decimal result))
				{
					return result;
				}
				return 0m; // Or handle this case appropriately
			}
		}


        private string _currentValue = "0";
        public string CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

		private Operation _currentOperation = Operation.STOP;
		public Operation CurrentOperation {
			get => _currentOperation;
			set {
				if(_currentOperation != value)
				{
                    _currentOperation = value;
					OnPropertyChanged(nameof(CurrentOperation));
                }
            }
		}

		public readonly ICommand AddOperationCommand;
		public readonly ICommand AddDigitCommand;
		public readonly ICommand AddDecimalCommand;

		public MainPageViewModel()
		{
			Calculation = new Calculation(new Step(0m, Operation.STOP));

            AddOperationCommand = new Command<Operation>((operation) =>
			{
				AddOperation(operation);
			});

			AddDigitCommand = new Command<int>((value) =>
			{
				if(value < 0 || value > 9)
				{
					throw new Exception("Digits must be 0-9");
				}
				AddDigit(value);
			});

			AddDecimalCommand = new Command(() =>
			{
				AddDecimal();
			});
        }

		public void AddDigit(int digit)
		{
			CurrentValue += digit.ToString();
		}

		public void AddDecimal()
		{
			if (CurrentValue.Contains(".")) return;
			CurrentValue += ".";
		}

		public void AddOperation(Operation operation)
		{
			var newStep = new Step(CurrentValueDecimal, operation);
            Calculation.AddStep(newStep);
			CurrentOperation = operation;
		}

		public void ClearCurrentValue()
		{
			CurrentValue = "0";
		}

		public void ClearAll()
		{
			ClearCurrentValue();
            var start = new Step(0m, Operation.STOP);
            Calculation = new Calculation(start);
		}

    }
}
