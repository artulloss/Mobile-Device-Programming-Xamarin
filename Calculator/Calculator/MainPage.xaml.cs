using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public Operation? Operation;
        public decimal? Memory;

        public decimal? Current = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public void UpdateDisplay(string val)
        {
            Display.Text = val;
        }

        void SetCurrent(decimal val)
        {
            Current = val;
        }

        decimal? ExecuteCalculation()
        {
            if (Memory == null)
                return null;

            switch(this.Operation)
            {
                case Calculator.Operation.ADDITION:
                    return Memory + Current;
                case Calculator.Operation.SUBTRACTION:
                    return Memory - Current;
                case Calculator.Operation.MULTIPLICATION:
                    return Memory * Current;
                case Calculator.Operation.DIVISION:
                    if(Current == 0m)
                    {
                        throw new DivideByZeroException();
                    }
                    return Memory / Current;
            }
            throw new Exception("Invalid operand");
        }

        void Button_Clicked_Flip_Negative(System.Object sender, System.EventArgs e)
        {
            SetCurrent(Current.Value * -1);
            UpdateDisplay(Current.ToString());
        }

        private void OnNumberButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var number = button.Text; // This gives you the button's text
                                          // Handle the number button click, e.g., append the number to the current display

                if(Operation != null)
                {
                    Memory = Current;
                    Current = 0m;
                }


                var newValue = $"{Current}{number}";

                if(newValue.Length > 9)
                {
                    return; // iOS calculator limit is 9
                }

                if (Current == 0m)
                {
                    newValue = number;
                }


                if(Display.Text.Contains(".") && Current % 1 == 0)
                {
                    // Handle setting a decimal IF the display contains a decimal but the current value is a whole number
                    newValue = $"{Current}.{number}";
                }

                SetCurrent(Convert.ToDecimal(newValue));

                UpdateDisplay(newValue);
            }
        }

        private void OnOperationButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var operation = button.Text; // This gives you the button's text
                                             // Handle the operation button click
                                             // You might need to translate the text to an Operation enum value

                if (Memory != null && Current != null && Operation != null)
                {
                    var result = ExecuteCalculation();
                    UpdateDisplay(result.ToString());
                    Current = result;
                }


                switch (operation)
                {
                    case "+":
                        this.Operation = Calculator.Operation.ADDITION;
                        break;
                    case "-":
                        this.Operation = Calculator.Operation.SUBTRACTION;
                        break;
                    case "×":
                        this.Operation = Calculator.Operation.MULTIPLICATION;
                        break;
                    case "÷":
                        this.Operation = Calculator.Operation.DIVISION;
                        break;
                }


            }
        }

        void Button_Clicked_Clear(System.Object sender, System.EventArgs e)
        {
            Operation = null;
            SetCurrent(0);
            UpdateDisplay("0");
            Memory = null;
        }

        void OnDecimalButtonClicked(System.Object sender, System.EventArgs e)
        {
            var remainder = Current % 1;
            if (remainder == 0m)
            {
                OnNumberButtonClicked(sender, e);
            }
            
        }

        void OnSolveButtonClicked(System.Object sender, System.EventArgs e)
        {
            if (Memory != null && Current != null && Operation != null)
            {
                var result = ExecuteCalculation();
                UpdateDisplay(result.ToString());
                Current = result;
            }
        }
    }
}

