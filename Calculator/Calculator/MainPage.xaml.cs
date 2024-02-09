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

        private int _numClickedInARow = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            UpdateDisplay("0");
        }

        public void UpdateDisplay(string val)
        {
            // Reset font size
            Display.FontSize = StaticProperties.DISPLAY_FONT_SIZE_DEFAULT;

            switch (val.Length)
            {
                case 7:
                    Display.FontSize = StaticProperties.DISPLAY_FONT_SIZE_SEVEN;
                    break;
                case 8:
                    Display.FontSize = StaticProperties.DISPLAY_FONT_SIZE_EIGHT;
                    break;
            }

            if(val.Length >= 9)
            {
                Display.FontSize = StaticProperties.DISPLAY_FONT_SIZE_NINE;
            }

            Display.Text = val;
        }

        public string GetDisplay()
        {
            return Display.Text;
        }

        void SetCurrent(decimal? val)
        {
            Current = val;
        }

        decimal? ExecuteCalculation()
        {
            if (Memory == null)
                return null;

            switch(Operation)
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
                        UpdateDisplay("Error");
                        return null;
                    }
                    return Memory / Current;
            }
            throw new Exception("Invalid operand");
        }

        void Button_Clicked_Flip_Negative(System.Object sender, System.EventArgs e)
        {
            SetCurrent(Current.Value * -1);
            UpdateDisplay(Current.ToString());
            _numClickedInARow = 0;
        }

        private void On_Number_Button_Clicked(object sender, EventArgs e)
        {
            _numClickedInARow++;
            var button = sender as Button;
            if (button != null)
            {
                var number = button.Text;

                if(_numClickedInARow <= 1 && Operation != null && Current != null)
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

                ClearBtn.Text = "C";

                UpdateDisplay(newValue);
            }
        }

        private void On_Operation_Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var operation = button.Text;

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
                _numClickedInARow = 0;

            }
        }

        public void Clear(bool all = false)
        {
            SetCurrent(null);
            UpdateDisplay("0");
            if (all)
            {
                Operation = null;
                Memory = null;
            }
            _numClickedInARow = 0;
        }

        void Button_Clicked_Clear(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            if (button.Text == "C")
            {
                button.Text = "AC";
                Clear();
            }
            else
            {
                Clear(true);
            }
        }

        void On_Decimal_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var remainder = Current % 1;
            if (remainder == 0m)
            {
                On_Number_Button_Clicked(sender, e);
            }
        }

        void On_Solve_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Memory != null && Current != null && Operation != null)
            {
                var result = ExecuteCalculation();
                UpdateDisplay(result.ToString());
                Current = result;
                _numClickedInARow = 0;
                Operation = null;
            }
        }

        void Button_Clicked_Percent(System.Object sender, System.EventArgs e)
        {
            SetCurrent(Current / 100M);
            UpdateDisplay(Current.ToString());
        }
    }
}

