using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private decimal? Memory = null;
        private Operation? operation = null;

        public Command<int> NumberButtonCommand { get; }
        public Command<Operation> OperationButtonCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            NumberButtonCommand = new Command<int>(HandleNumberButtonClicked);
            OperationButtonCommand = new Command<Operation>(HandleOperationButtonClicked);
            BindingContext = this;
        }

        void SetCurrent(decimal val)
        {
            Display.Text = val.ToString();
        }

        decimal GetCurrent()
        {
            return Convert.ToDecimal(Display.Text);
        }

        void Button_Clicked_Flip_Negative(System.Object sender, System.EventArgs e)
        {
            SetCurrent(GetCurrent() * -1);
        }

        void HandleNumberButtonClicked(int number)
        {
            var current = GetCurrent();
            var newValue = current.ToString() + number.ToString();
            SetCurrent(Convert.ToDecimal(newValue));
        }

        void HandleOperationButtonClicked(Operation operation)
        {
            Memory = GetCurrent();
            this.operation = operation;
        }

        void Button_Clicked_Clear(System.Object sender, System.EventArgs e)
        {
            this.operation = null;
            SetCurrent(0);
            Memory = null;
        }
    }
}

