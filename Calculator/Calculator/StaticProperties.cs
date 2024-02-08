using System;
using Xamarin.Forms;

namespace Calculator
{
    public static class StaticProperties
    {
        public const int BUTTON_SIZE = 100; // Size as an integer
        public static readonly int CORNER_RADIUS = BUTTON_SIZE / 2; // Computed at runtime

        public static readonly Color COLOR_PRIMARY = Color.FromHex("#efa24b");
        public static readonly Color COLOR_SECONDARY = Color.FromHex("#555555");
        public static readonly Color COLOR_TERTIARY = Color.FromHex("#aaaaaa");

        public static readonly Color COLOR_BLACK = Color.Black;
        public static readonly Color COLOR_WHITE = Color.FromHex("#f5f5f5");
    }
}
