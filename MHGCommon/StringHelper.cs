using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MHGCommon
{
    public static class StringHelper
    {
        #region Constants
        public const string ECLIPSE_STRING = "...";
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string DEFAULT_DECIMAL_SEPARATOR = ".";
        /// <summary>
        /// fr-CH culture
        /// </summary>
        public const string FRENCH_CULTURE = "fr-CH";

        /// <summary>
        /// de-CH culture
        /// </summary>
        public const string GERMAN_CULTURE = "de-CH";

        /// <summary>
        /// it-CH culture
        /// </summary>
        public const string ITALIAN_CULTURE = "it-CH";

        /// <summary>
        /// en-US culture
        /// </summary>
        public const string US_CULTURE = "en-US";
        #endregion

        #region SMethods

        public static string DefaultGroupSeparator {
            get {
                return SPACE;
            }
        }

        public static string DefaultDecimalSeparator {
            get {
                return DEFAULT_DECIMAL_SEPARATOR;
            }
        }

        public static NumberFormatInfo GetNumberFormat(string decimalSeparator, string groupSeparator, int decimalLength = 2) {
            var numberFormat = new NumberFormatInfo();
            decimalSeparator = string.IsNullOrEmpty(decimalSeparator) ? DefaultDecimalSeparator : decimalSeparator;

            numberFormat.CurrencyDecimalSeparator = decimalSeparator;
            numberFormat.NumberDecimalSeparator = decimalSeparator;
            numberFormat.PercentDecimalSeparator = decimalSeparator;

            numberFormat.CurrencyGroupSeparator = groupSeparator;
            numberFormat.NumberGroupSeparator = groupSeparator;
            numberFormat.PercentGroupSeparator = groupSeparator;

            numberFormat.NumberDecimalDigits = decimalLength;

            return numberFormat;
        }

        #region Crop String

        /// <summary>
        /// Returns the cropped string if it is longer than maxLength.
        /// </summary>
        /// <param name="input">The input string to crop.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>Cropped string if it is longer than maxLength</returns>
        public static string CropString(this string input, int maxLength)
        {
            return CropString(input, maxLength, false);
        }

        /// <summary>
        /// Returns the cropped string if it is longer than maxLength.
        /// When useSuspensionPoints is true, the return string will end with 
        /// suspension points "..." if cropped.
        /// </summary>
        /// <param name="input">The input string to crop.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <param name="useSuspensionPoints">When true, the cropped string ends with "...".</param>
        /// <returns>Cropped string if it is longer than maxLength.</returns>
        public static string CropString(this string input, int maxLength, bool useSuspensionPoints)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
            {
                return input;
            }
            if (useSuspensionPoints)
            {
                return input.Substring(0, maxLength - ECLIPSE_STRING.Length) + ECLIPSE_STRING;
            }
            return input.Substring(0, maxLength);
        }

        #endregion

        #region Make Upper/Lower

        /// <summary>
        /// To the upper.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string MakeUpper(this string text)
        {
            return (text != null) ? text.ToUpper(CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// Makes the lower.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string MakeLower(this string text)
        {
            return (text != null) ? text.ToLower(CultureInfo.InvariantCulture) : null;
        }

        #endregion

        #region Join/Split string

        /// <summary>
        /// Joins the with space in between.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithSeparator(string separator, params string[] items)
        {
            return (items != null) ? string.Join(separator, items.Where(i => !string.IsNullOrEmpty(i))) : string.Empty;
            // NOTE: Using string.Join(separator, items) will join empty elements
        }

        /// <summary>
        /// Joins the with comma in between.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithCommaInBetween(params string[] items)
        {
            return JoinWithSeparator(COMMA, items);
        }

        /// <summary>
        /// Joins the with space in between.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithSpaceInBetween(params string[] items)
        {
            return JoinWithSeparator(SPACE, items);
        }

        /// <summary>
        /// Splits a string to list (spaces or empty elements are removed).
        /// </summary>        
        public static List<string> SplitToList(string arrayText, string separator = COMMA)
        {
            if (string.IsNullOrEmpty(arrayText))
            {
                return new List<string>();
            }
            return arrayText.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(s => !string.IsNullOrWhiteSpace(s.Trim()))
                            .Select(s => s.Trim())
                            .ToList();
        }

        #endregion

        #region Misc

        /// <summary>
        /// Removes the diacritics.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Initializes the cap of text, with the first letter of each word in uppercase, all other letters in lowercase
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string InitCap(this string text)
        {
            return (text != null) ? CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text.ToLower()) : null;

        }

        /// <summary>
        /// Initializes the cap of text, with the first letter of each word in uppercase, all other letters in lowercase.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string InitCap(this string text, string cultureInfo)
        {
            return (text != null) ? CultureInfo.CreateSpecificCulture(cultureInfo).TextInfo.ToTitleCase(text.ToLower()) : null;

        }

        /// <summary>
        /// Initializes the cap of text, with the first letter of each word in uppercase, all other letters in lowercase.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public static string InitCap(this string text, CultureInfo cultureInfo)
        {
            return (text != null) ? cultureInfo.TextInfo.ToTitleCase(text.ToLower()) : null;
        }

        #endregion

        #region Convert String to double

        /// <summary>
        /// Convert a string to a double
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <param name="cultureInfo">Culture info</param>
        /// <returns>the corresponding double value, null 
        /// is returned if the string format is not valid</returns>
        public static double? StringToDouble(this string str, IFormatProvider cultureInfo)
        {
            double result;
            if (Double.TryParse(str, NumberStyles.Any, cultureInfo, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert Strings to double.
        /// Using this function if threr is no special requirements
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static double? StringToDouble(this string str)
        {
            return StringToDouble(str, CultureInfo.InvariantCulture);
        }

        public static double? StringToDoubleCurrentCulture(this string str)
        {
            return StringToDouble(str, CultureInfo.CurrentCulture);
        }

        public static double? StringToDouble(this string str, string cultureInfo)
        {
            return StringToDouble(str, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        public static double? StringToDouble(this string str, string decimalSeparator, string groupSeparator) {

            return StringToDouble(str, GetNumberFormat(decimalSeparator, groupSeparator));
        }

        public static double? StringToDouble(this string str, string decimalSeparator, string groupSeparator, int decimalDigits) {

            return StringToDouble(str, GetNumberFormat(decimalSeparator, groupSeparator, decimalDigits));
        }

        #endregion

        #region Convert String to decimal. For money, always decimal. It's why it was created.

        /// <summary>
        /// Convert a string to a decimal
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <param name="cultureInfo">Culture info</param>
        /// <returns>the corresponding double value, null 
        /// is returned if the string format is not valid</returns>
        public static decimal? StringToDecimal(this string str, IFormatProvider cultureInfo)
        {
            decimal result;
            if (decimal.TryParse(str, NumberStyles.Any, cultureInfo, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert a string to a decimal.
        /// </summary>
        /// <param name="str">The string value.</param>
        /// <returns></returns>
        public static decimal? StringToDecimal(this string str)
        {
            return StringToDecimal(str, CultureInfo.InvariantCulture);
        }

        public static decimal? StringToDecimalCurrentCulture(this string str)
        {
            return StringToDecimal(str, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert a string to a decimal.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public static decimal? StringToDecimal(this string str, string cultureInfo)
        {
            return StringToDecimal(str, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        public static decimal? StringToDecimal(this string str, string decimalSeparator, string groupSeparator) {

            return StringToDecimal(str, GetNumberFormat(decimalSeparator, groupSeparator));
        }

        public static decimal? StringToDecimal(this string str, string decimalSeparator, string groupSeparator, int decimalDigits) {

            return StringToDecimal(str, GetNumberFormat(decimalSeparator, groupSeparator, decimalDigits));
        }

        #endregion

        #region Convert string to Integer. Long is Int64

        /// <summary>
        /// Convert a string to a integer
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <returns>the corresponding integer value</returns>
        public static int? StringToInt32(this string str)
        {
            int result;
            if (Int32.TryParse(str, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert a string to a long
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <returns>the corresponding long value</returns>
        public static long? StringToLong(this string str)
        {
            long result;
            if (Int64.TryParse(str, out result))
            {
                return result;
            }
            return null;
        }

        #endregion

        #region Convert Decimal to string

        /// <summary>
        /// Convert Decimals to string with current Culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string DecimalToString(this decimal value) {
            return DecimalToString(value, CultureInfo.InvariantCulture);
        }

        public static string DecimalToStringCurrentCulture(this decimal value)
        {
            return DecimalToString(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert Decimals to string with culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string DecimalToString(this decimal value, string cultureInfo)
        {
            return DecimalToString(value, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        /// <summary>
        /// Decimals to string with culture info.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information CultureInfo.</param>
        /// <returns></returns>
        public static string DecimalToString(this decimal value, IFormatProvider cultureInfo)
        {
            return Convert.ToString(value, cultureInfo);
        }

        public static string DecimalToString(this decimal value, string decimalSeparator, string groupSeparator) {
            return Convert.ToString(value, GetNumberFormat(decimalSeparator, groupSeparator));
        }

        public static string DecimalToString(this decimal value, string decimalSeparator, string groupSeparator, int decimalDigits) {
            return Convert.ToString(value, GetNumberFormat(decimalSeparator, groupSeparator, decimalDigits));
        }

        #endregion

        #region Convert Double to string

        /// <summary>
        /// Convert double to string with current Culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value)
        {
            return DoubleToString(value, CultureInfo.InvariantCulture);
        }

        public static string DoubleToStringCurrentCulture(this double value)
        {
            return DoubleToString(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert double to string with culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value, string cultureInfo)
        {
            return DoubleToString(value, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        /// <summary>
        /// Convert double to string with culture info.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information CultureInfo.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value, IFormatProvider cultureInfo)
        {
            return Convert.ToString(value, cultureInfo);
        }

        public static string DoubleToString(this double value, string decimalSeparator, string groupSeparator) {
            return Convert.ToString(value, GetNumberFormat(decimalSeparator, groupSeparator));
        }

        public static string DoubleToString(this double value, string decimalSeparator, string groupSeparator, int decimalDigits) {
            return Convert.ToString(value, GetNumberFormat(decimalSeparator, groupSeparator, decimalDigits));
        }

        #endregion

        #region Convert Integer to string

        /// <summary>
        /// Convert integer to string with current Culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Int32ToString(this int value)
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        public static string Int64ToString(this long value)
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        #endregion

        #endregion
    }
}
