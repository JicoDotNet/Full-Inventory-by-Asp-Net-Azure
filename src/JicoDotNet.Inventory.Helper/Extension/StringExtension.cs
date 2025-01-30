namespace System
{
    using Text;
    public static class StringExtension
    {
        /// <summary>
        /// It will return the string or null if the string is null or Empty ("") or contain with white space (" ")
        /// </summary>
        /// <param name="str">this string object</param>
        /// <returns>null or string</returns>
        public static string ToNullOrValue(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            return str;
        }

        /// <summary>
        /// First char will be Upper
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperFirstChar(this string str)
        {
            str = str.ToLower();
            bool IsNewSentense = true;
            var result = new StringBuilder(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(str[i]))
                {
                    result.Append(char.ToUpper(str[i]));
                    IsNewSentense = false;
                }
                else
                    result.Append(str[i]);

                if (str[i] == '!' || str[i] == '?' || str[i] == '.')
                {
                    IsNewSentense = true;
                }
            }

            return result.ToString();
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

    }
}
