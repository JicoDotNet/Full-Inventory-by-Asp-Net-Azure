namespace System
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class GenericLogic
    {
        public static DateTime IstNow => DateTime.UtcNow.AddMinutes(330);

        public static string DisplayLongDateFormat => "MMM-dd-yyyy, hh:mm:ss tt, ddd";

        public static string DisplayShortDateFormat => "MMM-dd-yyyy, ddd";

        public static string DateMaskFormat => "dd/MM/yyyy";

        public static string SqlSchema { get { return "[SingleIB]"; } }

        public static string StringGenerate(int lengths = 16)
        {
            NameGenerator();
            Random random = new Random();
            StringBuilder buffer = new StringBuilder(lengths);
            for (int i = 0; i < lengths; i++)
            {
                int randomNumber = random.Next(0, _characters.Count);
                char randomChar = _characters[randomNumber];
                buffer.Append(randomChar);
            }
            return buffer.ToString();
        }
        private static List<char> _characters;
        private static void NameGenerator()
        {
            _characters = new List<char>();
            // Fill character list with A-Z.
            for (int i = 65; i <= 90; i++)
            {
                _characters.Add((char)i);
            }
            // Fill character list with a-z.
            for (int i = 97; i <= 122; i++)
            {
                _characters.Add((char)i);
            }
            // Fill character list with 0-9.
            for (int i = 48; i <= 57; i++)
            {
                _characters.Add((char)i);
            }
        }

        public static Dictionary<string, string> State()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "01", "Jammu and Kashmir" },
                { "02", "Himachal Pradesh" },
                { "03", "Punjab" },
                { "04", "Chandigarh" },
                { "05", "Uttarakhand" },
                { "06", "Haryana" },
                { "07", "Delhi" },
                { "08", "Rajasthan" },
                { "09", "Uttar Pradesh" },
                { "10", "Bihar" },
                { "11", "Sikkim" },
                { "12", "Arunachal Pradesh" },
                { "13", "Nagaland" },
                { "14", "Manipur" },
                { "15", "Mizoram" },
                { "16", "Tripura" },
                { "17", "Meghalaya" },
                { "18", "Assam" },
                { "19", "West Bengal" },
                { "20", "Jharkhand" },
                { "21", "Odisha" },
                { "22", "Chhattisgarh" },
                { "23", "Madhya Pradesh" },
                { "24", "Gujarat" },
                { "25", "Daman & Diu" },
                { "26", "Dadra & Nagar Haveli" },
                { "27", "Maharashtra" },
                { "28", "Andhra Pradesh (Before Division)" },
                { "29", "Karnataka" },
                { "30", "Goa" },
                { "31", "Lakshadweep" },
                { "32", "Kerala" },
                { "33", "Tamil Nadu" },
                { "34", "Puducherry" },
                { "35", "Andaman And Nicobar Islands" },
                { "36", "Telangana" },
                { "37", "Andhra Pradesh (New)" },
                { "38", "Ladakh" }
            };

            return pairs.OrderBy(a => a.Value).ToDictionary(b => b.Key, b => b.Value);
        }

        public static Dictionary<string, string> Months()
        {
            return new Dictionary<string, string>
            {
                { "1", "January" },
                { "2", "February" },
                { "3", "March" },
                { "4", "April" },
                { "5", "May" },
                { "6", "June" },
                { "7", "July" },
                { "8", "August" },
                { "9", "September" },
                { "10", "October" },
                { "11", "November" },
                { "12", "December" }
            };
        }

        public static List<string> UnionTerritoryCode()
        {
            return new List<string>
            {
                "04", // "Chandigarh"
                "25", // "Daman & Diu"
                "26", // "Dadra & Nagar Haveli"
                "31", // "Lakshadweep"
                "35"  // "Andaman And Nicobar Islands"
            };
        }

        public static string GstStateCode(string GSTNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(GSTNumber))
                    return null;
                if (GSTNumber.Length != 15)
                    return null;

                string stateCode = GSTNumber.Substring(0, 2);
                if (stateCode != "99")
                {
                    if (State().FirstOrDefault(a => a.Key == stateCode).Key != null)
                    {
                        return stateCode;
                    }
                }
                else
                    return stateCode;
            }
            catch
            {
                return null;
            }
            return null;
        }

        public static bool IsValidGSTNumber(string GSTNumber)
        {
            if (string.IsNullOrEmpty(GSTNumber)) return false;

            string strRegex = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(GSTNumber))
                return true;
            return false;
        }

        public static Dictionary<string, string> CompanyType()
        {
            return new Dictionary<string, string>
            {
                { "Personal", "Personal" },
                { "Proprietorship", "Sole Proprietorship" },
                { "Partnership", "General Partnership" },
                { "Limited Liability Partnership", "Limited Liability Partnership (LLP)" },
                { "One Person Company", "One Person Company (OPC)" },
                { "Private Limited", "Private Limited" },
                { "Limited", "Limited" },
                { "ORG or NGO", "ORG or NGO - Non Profitable" },
                { "Government Firm", "Government Firm" },
                { "Others", "Others" }
            };
        }

        public static IDictionary<bool, string> YesNo()
        {
            return new Dictionary<bool, string>
            {
                { true, "Yes" },
                { false, "No" }
            };
        }

        public static IDictionary<int, string> ProductCategory()
        {
            return new Dictionary<int, string>
            {
                { 0, "Purchase & Sale Both Allowed" },
                { 1, "Only Purchase Allowed" },
                { 2, "Only Sale Allowed" }
            };
        }

        public static IDictionary<short, string> PaymentMode()
        {
            return new Dictionary<short, string>
            {
                { 1, "Cash" },
                { 2, "NEFT" },
                { 3, "Cheque" },
                { 4, "Wallet" },
                { 5, "POS-Card" },
                { 9, "Others" }
            };
        }

        public static IDictionary<Microsoft.WindowsAzure.Storage.Table.EdmType, string> DataType()
        {
            return new Dictionary<Microsoft.WindowsAzure.Storage.Table.EdmType, string>
            {
                { Microsoft.WindowsAzure.Storage.Table.EdmType.String, "Text/Content" },
                { Microsoft.WindowsAzure.Storage.Table.EdmType.Boolean, "Yes/No" },
                { Microsoft.WindowsAzure.Storage.Table.EdmType.Double, "Number/Amount/Price" },
                { Microsoft.WindowsAzure.Storage.Table.EdmType.DateTime, "Date" }
            };
        }

        #region Number To Words
        private static readonly string[] Units = { "Zero", "One", "Two", "Three",
                "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
                "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty",
                "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static string ConvertToWord(long i)
        {
            if (i < 20)
            {
                return Units[i];
            }
            if (i < 100)
            {
                return Tens[i / 10] + (i % 10 > 0 ? " " + ConvertToWord(i % 10) : "");
            }
            if (i < 1000)
            {
                return Units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " " + ConvertToWord(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertToWord(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + ConvertToWord(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertToWord(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertToWord(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertToWord(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertToWord(i % 10000000) : "");
            }
            return ConvertToWord(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertToWord(i % 1000000000) : "");
        }
        public static string ConvertToWord(double amount)
        {
            try
            {
                long amountRupee = (long)amount;
                long amountPaisa = (long)Math.Round((amount - amountRupee) * 100);
                if (amountPaisa == 0)
                {
                    return ConvertToWord(amountRupee) + " Rupee(s) Only.";
                }

                return ConvertToWord(amountRupee) + " Rupee(s) and " + ConvertToWord(amountPaisa) + " Paisa Only.";
            }
            catch
            {
                // TODO: handle exception  
            }
            return "";
        }
        public static string ConvertToWord(decimal amount)
        {
            try
            {
                ConvertToWord((double)amount);
            }
            catch
            {
                // TODO: handle exception  
            }
            return "";
        }
        #endregion

        public static string EncodeHex(long s)
        {
            IDictionary<string, string> characters = new Dictionary<string, string>
            {
                { "0", "_" },
                { "1", "W" },
                { "2", "e" },
                { "3", "R" },
                { "4", "t" },
                { "5", "Y" },
                { "6", "u" },
                { "7", "P" },
                { "8", "a" },
                { "9", "S" },

                { "A", "9" },
                { "B", "8" },
                { "C", "6" },
                { "D", "5" },
                { "E", "3" },
                { "F", "1" },
            };

            string r = "";
            char[] cs = s.ToString("X").ToCharArray();
            foreach (var t in cs)
            {
                r += characters[t.ToString()];
            }
            return r;
        }

        public static long DecodeHex(string encodeHexStr)
        {
            IDictionary<string, string> characters = new Dictionary<string, string>
            {
                { "_", "0" },
                { "W", "1" },
                { "e", "2" },
                { "R", "3" },
                { "t", "4" },
                { "Y", "5" },
                { "u", "6" },
                { "P", "7" },
                { "a", "8" },
                { "S", "9" },

                { "9", "A" },
                { "8", "B" },
                { "6", "C" },
                { "5", "D" },
                { "3", "E" },
                { "1", "F" },
            };

            string r = "";
            char[] cs = encodeHexStr.ToCharArray();
            for (int i = 0; i < cs.Length; i++)
            {
                r += characters[cs[i].ToString()];
            }
            return Convert.ToInt64(r, 16);
        }
    }

    //public class NumberToWords
    //{
    //    private static readonly string[] Units = { "Zero", "One", "Two", "Three",
    //            "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    //            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    //            "Seventeen", "Eighteen", "Nineteen" };
    //    private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty",
    //            "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

    //    public static string ConvertAmount(double amount)
    //    {
    //        try
    //        {
    //            long amountInt = (long)amount;
    //            long amountDec = (long)Math.Round((amount - amountInt) * 100);
    //            if (amountDec == 0)
    //            {
    //                return Convert(amountInt) + " Only.";
    //            }
    //            return Convert(amountInt) + " Point " + Convert(amountDec) + " Only.";
    //        }
    //        catch (Exception e)
    //        {
    //            // TODO: handle exception  
    //        }
    //        return "";
    //    }

    //    private static string Convert(long i)
    //    {
    //        if (i < 20)
    //        {
    //            return Units[i];
    //        }
    //        if (i < 100)
    //        {
    //            return Tens[i / 10] + ((i % 10 > 0) ? " " + Convert(i % 10) : "");
    //        }
    //        if (i < 1000)
    //        {
    //            return Units[i / 100] + " Hundred"
    //                    + ((i % 100 > 0) ? " And " + Convert(i % 100) : "");
    //        }
    //        if (i < 100000)
    //        {
    //            return Convert(i / 1000) + " Thousand "
    //            + ((i % 1000 > 0) ? " " + Convert(i % 1000) : "");
    //        }
    //        if (i < 10000000)
    //        {
    //            return Convert(i / 100000) + " Lakh "
    //                    + ((i % 100000 > 0) ? " " + Convert(i % 100000) : "");
    //        }
    //        if (i < 1000000000)
    //        {
    //            return Convert(i / 10000000) + " Crore "
    //                    + ((i % 10000000 > 0) ? " " + Convert(i % 10000000) : "");
    //        }
    //        return Convert(i / 1000000000) + " Arab "
    //                + ((i % 1000000000 > 0) ? " " + Convert(i % 1000000000) : "");
    //    }
    //}
}
