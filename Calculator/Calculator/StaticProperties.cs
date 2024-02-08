using System;
using Xamarin.Forms;

namespace Calculator
{
    public static class StaticProperties
    {
        public const int BUTTON_SIZE = 90; // Size as an integer
        public static readonly int CORNER_RADIUS = BUTTON_SIZE / 2; // Computed at runtime

        public static readonly Color COLOR_PRIMARY = Color.FromHex("#f2a33c");
        public static readonly Color COLOR_SECONDARY = Color.FromHex("#333");
        public static readonly Color COLOR_TERTIARY = Color.FromHex("#a5a5a5");

        public static readonly Color COLOR_BLACK = Color.Black;
        public static readonly Color COLOR_WHITE = Color.FromHex("#f5f5f5");
    }
}
