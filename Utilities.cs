using System;
using System.Globalization;
using KG_Task4.Structures;

namespace KG_Task4
{
    internal static class Utilities
    {
        private static CultureInfo dotSeparatorCI;

        static Utilities()
        {
            dotSeparatorCI = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dotSeparatorCI.NumberFormat.CurrencyDecimalSeparator = ".";
        }

        public static float DegToRad(float deg) => (float)(deg * Math.PI / 180);

        public static Vector3 DegToRad(Vector3 deg) => new Vector3(DegToRad(deg.X), DegToRad(deg.Y), DegToRad(deg.Z));

        public static float RadToDeg(float rad) => (float)(rad * 180 / Math.PI);

        public static Vector3 RadToDeg(Vector3 rad) => new Vector3(RadToDeg(rad.X), RadToDeg(rad.Y), RadToDeg(rad.Z));

        public static float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;
        
        public static bool FloatTryParse(string str, out float r)
        {
             return float.TryParse(str, NumberStyles.Any, dotSeparatorCI, out r);
        }

      
    
    }
}
