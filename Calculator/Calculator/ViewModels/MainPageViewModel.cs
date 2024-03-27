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
		public Calculation Calculation { get; set; } = null;
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

		public ICommand AddOperationCommand { private set;  get; }
		public ICommand AddDigitCommand { private set; get; }
        public ICommand AddDecimalCommand { private set; get; }
		public ICommand ClearCommand { private set; get; }
		public ICommand EqualCommand { private set; get; }

        public MainPageViewModel()
		{
			//Calculation = new Calculation(new Step(0m, Operation.STOP));

            AddOperationCommand = new Command<Operation>(AddOperation);

			AddDigitCommand = new Command<string>((value_string) =>
			{
				int value = int.Parse(value_string);
				if(value < 0 || value > 9)
				{
					throw new Exception("Digits must be 0-9");
				}
				AddDigit(value);
			});

			AddDecimalCommand = new Command(AddDecimal);

			ClearCommand = new Command(ClearAll);
			EqualCommand = new Command(Solve);
        }

		public void AddDigit(int digit)
		{
			if(Calculation != null && Calculation.LastStep.Operation != Operation.STOP)
			{
				CurrentValue = "";
			}
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

			if(Calculation == null)
			{
				Calculation = new Calculation(newStep);
			} else
			{
				Calculation.AddStep(newStep);
			}

			CurrentOperation = operation;
		}

		public void ClearCurrentValue()
		{
			CurrentValue = "0";
		}

		public void ClearAll()
		{
			ClearCurrentValue();
			Calculation = null;
		}

		public void Solve()
		{
			if(Calculation.LastStep.Operation != Operation.STOP)
			{
                Calculation.AddStep(new Step(CurrentValueDecimal));
            }
            decimal solution = Calculation.ComputeSolution();
			CurrentValue = solution.ToString();
		}

    }
}
