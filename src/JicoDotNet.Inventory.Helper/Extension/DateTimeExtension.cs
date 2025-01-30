namespace System
{

    public static class DateTimeExtension
    {
        public static long TimeStamp(this DateTime DTobj)
        {
            try
            {
                return ((DTobj.Ticks - 621355968000000000) / 10000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ToDisplayDateString(this DateTime DTobj)
        {
            try
            {
                return DTobj.ToString(GenericLogic.DisplayLongDateFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ToDisplayShortDateString(this DateTime DTobj)
        {
            try
            {
                return DTobj.ToString(GenericLogic.DisplayShortDateFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ToDisplayShortDateString(this DateTime? DTobj)
        {
            try
            {
                if (DTobj.HasValue)
                    return DTobj.Value.ToString(GenericLogic.DisplayShortDateFormat);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Before 1st Jan 2001 12:00AM, returns null, 
        /// </summary>
        /// <param name="DTobj"></param>
        /// <returns>
        /// returns null if it's before 1st Jan 2001 12:00AM
        /// </returns>
        public static string ToDisplayableShortDateString(this DateTime DTobj)
        {
            try
            {
                return DTobj > new DateTime(2001, 1, 1) ? DTobj.ToDisplayShortDateString() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ToDateMaskString(this DateTime DTobj)
        {
            try
            {
                return DTobj.ToString(GenericLogic.DateMaskFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ShortDuration(this TimeSpan timeSpan)
        {
            int val = 0;
            string txt = "";

            if (timeSpan.TotalSeconds < 60)
            {
                val = (int)timeSpan.TotalMinutes;
                txt = "secs";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                val = (int)timeSpan.TotalMinutes;
                txt = "mins";
            }
            else if (timeSpan.TotalMinutes > 60 && timeSpan.TotalHours < 24)
            {
                val = (int)timeSpan.TotalHours;
                txt = "hrs";
            }
            else if (timeSpan.TotalHours > 24)
            {
                val = (int)timeSpan.TotalDays;
                txt = "days";
            }
            return val + " " + txt;
        }
        public static string LongDuration(this TimeSpan timeSpan)
        {
            string txt = "";
            if (timeSpan.Days < 1)
            {
                txt = timeSpan.Hours + " hrs, "
                + timeSpan.Minutes + " mins, " + timeSpan.Seconds + " secs";
            }
            else
            {
                txt = timeSpan.Days + " days, " + timeSpan.Hours + " hrs, "
                + timeSpan.Minutes + " mins, " + timeSpan.Seconds + " secs";
            }
            return txt;
        }
    }
}
