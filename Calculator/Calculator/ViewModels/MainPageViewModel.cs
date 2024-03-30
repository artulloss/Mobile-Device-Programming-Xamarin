using System;
using System.Windows.Input;
using Calculator.Models;
using Xamarin.Forms;
using System.Linq;

namespace Calculator.ViewModels
{
    public class MainPageViewModel : BindableObject
	{
#nullable enable
		public Calculation? Calculation { get; set; } = null;
#nullable disable
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

		private decimal? PrevSolution;

		public ICommand AddOperationCommand { private set;  get; }
		public ICommand AddDigitCommand { private set; get; }
        public ICommand AddDecimalCommand { private set; get; }
		public ICommand ClearCommand { private set; get; }
		public ICommand EqualCommand { private set; get; }
		public ICommand PercentCommand { private set; get; }
		public ICommand FlipSignCommand { private set; get; }

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
			PercentCommand = new Command(Percent);
			FlipSignCommand = new Command(InvertSign);

        }

		public void AddDigit(int digit)
		{
			string[] stopValues =
			{
				"0",
				"NaN",
				"Error"
			};
			if(stopValues.Contains(CurrentValue) || CurrentOperation != Operation.STOP)
			{
				CurrentValue = "";
				CurrentOperation = Operation.STOP;
			}

            CurrentValue += digit.ToString();
        }

		public void AddDecimal()
		{
			if (CurrentValue.Contains(".")) return;
			if(CurrentValue == "NaN" || CurrentValue == "Error")
			{
				CurrentValue = "0.";
				return;
			}

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
			CurrentOperation = Operation.STOP;
		}

		public void Solve()
		{
			if (Calculation == null) return;

			Step last = Calculation.LastStep;

			Step newLast = PrevSolution == CurrentValueDecimal
				? new Step(last.Val, last.Operation)
				: new Step(CurrentValueDecimal, last.Operation);

			Calculation.AddStep(new Step(newLast.Val, newLast.Operation));

			CurrentOperation = Operation.STOP;
			decimal? solution = GetSolution();

			if(solution == null)
			{
                Calculation = null;
                CurrentOperation = Operation.STOP;
                return;
			}

			// Flatten the calculation

			Step start = new ((decimal)solution);
            Calculation = new Calculation(start);

			PrevSolution = solution;
			CurrentValue = solution.ToString();
		}

		public void Percent()
		{
			CurrentValue = (CurrentValueDecimal / 100).ToString();
		}

		public void InvertSign()
		{
			CurrentValue = (CurrentValueDecimal * -1).ToString();
		}

		public decimal? GetSolution()
		{
			try
			{
                decimal solution = Calculation.ComputeSolution();
                return solution;
            } catch (DivideByZeroException _)
			{
				CurrentValue = "NaN";
			} catch (Exception _) {
				CurrentValue = "Error";
			}
			return null;
        }

    }
}
