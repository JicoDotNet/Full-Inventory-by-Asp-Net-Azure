namespace System
{
    public static class DecimalExtension
    {
        public static string ToDisplayDecimal(this decimal decimalValue)
        {
            try
            {
                return Math.Round(decimalValue, 2).ToString();
                //return decimalValue.ToString("0.00###");
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ToDisplayDouble(this double decimalValue)
        {
            try
            {
                return Math.Round(decimalValue, 2).ToString();
                //return decimalValue.ToString("0.00###");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
