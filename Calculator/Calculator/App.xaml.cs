﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Calculator
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        //protected override void OnStart ()
        //{
        //}

        //protected override async void OnSleep ()
        //{
        //    if (MainPage is MainPage calculatorPage)
        //    {
        //    }
        //}

        //protected override void OnResume ()
        //{
        //}
    }
}

