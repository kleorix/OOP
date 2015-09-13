using System;
using System.Globalization;

namespace MHGCommon {
    public static class DateHelper {
        /// <summary>
        // The date format: dd.MM.yyyy
        /// </summary>
        public const string DATE_PATTERN = "dd.MM.yyyy";

        /// <summary>
        /// The date format: dd.MM.yyyy HH:mm:ss.fff (24h format)
        /// </summary>
        public const string FULL_DATE_PATTERN = "dd.MM.yyyy HH:mm:ss";

        /// <summary>
        /// The date format: dd.MM.yyyy HH:mm (24h format)
        /// </summary>
        public const string LITE_DATE_PATTERN = "dd.MM.yyyy HH:mm";

        /// <summary>
        /// The date with 3-character in month.
        /// </summary>
        public const string DATE_3MONTH_PATTERN = "dd MMM yyyy";

        #region For not null Datetime
        /// <summary>
        /// Change a date to a string with the short format
        /// </summary>
        public static string DateToString(this DateTime dateObj) {
            return DateToString(dateObj, DATE_PATTERN);
        }

        /// <summary>
        /// Dates to string.
        /// </summary>
        /// <param name="dateObj">The date obj.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public static string DateToString(this DateTime dateObj, string pattern) {
            return DateToString(dateObj, pattern, DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Dates to string.
        /// </summary>
        /// <param name="dateObj">The date obj.</param>
        /// <param name="pattern">The pattern.</param>
        /// /// <param name="cultureInfo">The Culture Info.</param>
        /// <returns></returns>
        public static string DateToString(this DateTime dateObj, string pattern, DateTimeFormatInfo cultureInfo) {
            return dateObj.ToString(pattern, cultureInfo);
        } 

        #endregion

        #region For Nullable Datetime?

        /// <summary>
        /// Change a date to a string with the short format
        /// </summary>
        public static string DateToString(this DateTime? dateObj) {
            return dateObj.HasValue ? DateToString(dateObj.Value, DATE_PATTERN) : string.Empty;
        }

        /// <summary>
        /// Change a date to a string with the short format
        /// </summary>
        public static string DateToString(this DateTime? dateObj, string pattern) {
            return dateObj.HasValue ? DateToString(dateObj.Value, pattern) : string.Empty;
        }

        #endregion

        #region String To date

        public static DateTime? StringToDate(string dateObj) {
            return StringToDate(dateObj, DATE_PATTERN, DateTimeFormatInfo.CurrentInfo);
        }

        public static DateTime? StringToDate(string dateObj, string pattern) {
            return StringToDate(dateObj, DATE_PATTERN, DateTimeFormatInfo.CurrentInfo);
        }

        public static DateTime? StringToDate(string date, string pattern, IFormatProvider provider) {
            DateTime result;
            if (DateTime.TryParseExact(date.Trim(), pattern, provider, DateTimeStyles.None, out result)) {
                return result;
            }
            return null;
        } 

        #endregion
    }
}
