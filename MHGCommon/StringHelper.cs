using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MHGCommon {
    public static class StringHelper {

        #region Constants
        public const string ECLIPSE_STRING = "...";
        public const string SPACE = " ";
        public const string COMMA = ",";
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
        #endregion

        #region SMethods

        #region Crop String

        /// <summary>
        /// Returns the cropped string if it is longer than maxLength.
        /// </summary>
        /// <param name="input">The input string to crop.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>Cropped string if it is longer than maxLength</returns>
        public static string CropString(this string input, int maxLength) {
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
        public static string CropString(this string input, int maxLength, bool useSuspensionPoints) {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength) {
                return input;
            }
            if (useSuspensionPoints) {
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
        public static string MakeUpper(this string text) {
            return (text != null) ? text.ToUpper(CultureInfo.CurrentCulture) : null;
        }

        /// <summary>
        /// Makes the lower.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string MakeLower(this string text) {
            return (text != null) ? text.ToLower(CultureInfo.CurrentCulture) : null;
        } 

        #endregion

        #region Join/Split string

        /// <summary>
        /// Joins the with space in between.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithSeparator(string separator, params string[] items) {
            // return (items != null) ? string.Join(separator, items.Where(i => !string.IsNullOrEmpty(i))).Trim() : string.Empty;
            if (items == null || items.Length < 1) {
                return string.Empty;
            }
            return items.Aggregate((cur, next) => string.IsNullOrEmpty(next) ? cur : (string.IsNullOrEmpty(cur) ? next : cur + separator + next));
            // NOTE: Using string.Join(separator, items) will join empty elements
        }

        /// <summary>
        /// Joins the with comma in between.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithCommaInBetween(params string[] items) {
            return JoinWithSeparator(COMMA, items);
        }

        /// <summary>
        /// Joins the with space in between.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string JoinWithSpaceInBetween(params string[] items) {
            return JoinWithSeparator(SPACE, items);
        }

        /// <summary>
        /// Splits a string to list (spaces or empty elements are removed).
        /// </summary>        
        public static List<string> SplitToList(string arrayText, string separator = COMMA) {
            if (string.IsNullOrEmpty(arrayText)) {
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
        public static string RemoveDiacritics(this string text) {
            if (string.IsNullOrEmpty(text)) {
                return text;
            }
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString) {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
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
        public static string InitCap(this string text) {
            return (text != null) ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower()) : null;

        }

        /// <summary>
        /// Initializes the cap of text, with the first letter of each word in uppercase, all other letters in lowercase.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string InitCap(this string text, string cultureInfo) {
            return (text != null) ? CultureInfo.CreateSpecificCulture(cultureInfo).TextInfo.ToTitleCase(text.ToLower()) : null;

        }

        /// <summary>
        /// Initializes the cap of text, with the first letter of each word in uppercase, all other letters in lowercase.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public static string InitCap(this string text, CultureInfo cultureInfo) {
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
        public static double? StringToDouble(this string str, CultureInfo cultureInfo) {
            double result;
            if (Double.TryParse(str, NumberStyles.Any, cultureInfo, out result)) {
                return result;
            }
            return null;
        }

        public static double? StringToDouble(this string str) {
            return StringToDouble(str, CultureInfo.CurrentCulture);
        }

        public static double? StringToDoubleInvariantCulture(this string str) {
            return StringToDouble(str, CultureInfo.InvariantCulture);
        }

        public static double? StringToDouble(this string str, string cultureInfo) {
            return StringToDouble(str, CultureInfo.CreateSpecificCulture(cultureInfo));
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
        public static decimal? StringToDecimal(this string str, CultureInfo cultureInfo) {
            decimal result;
            if (decimal.TryParse(str, NumberStyles.Any, cultureInfo, out result)) {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert a string to a decimal.
        /// </summary>
        /// <param name="str">The string value.</param>
        /// <returns></returns>
        public static decimal? StringToDecimal(this string str) {
            return StringToDecimal(str, CultureInfo.CurrentCulture);
        }

        public static decimal? StringToDecimalInvariantCulture(this string str) {
            return StringToDecimal(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert a string to a decimal.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public static decimal? StringToDecimal(this string str, string cultureInfo) {
            return StringToDecimal(str, CultureInfo.CreateSpecificCulture(cultureInfo));
        } 

        #endregion

        #region Convert string to Integer. Long is Int64

        /// <summary>
        /// Convert a string to a integer
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <returns>the corresponding integer value</returns>
        public static int? StringToInt32(this string str) {
            int result;
            if (Int32.TryParse(str, out result)) {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert a string to a long
        /// </summary>
        /// <param name="str">the string to convert</param>
        /// <returns>the corresponding long value</returns>
        public static long? StringToLong(this string str) {
            long result;
            if (Int64.TryParse(str, out result)) {
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
            return DecimalToString(value, CultureInfo.CurrentCulture);
        }

        public static string DecimalToStringInvariantCulture(this decimal value) {
            return DecimalToString(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert Decimals to string with culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string DecimalToString(this decimal value, string cultureInfo) {
            return DecimalToString(value, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        /// <summary>
        /// Decimals to string with culture info.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information CultureInfo.</param>
        /// <returns></returns>
        public static string DecimalToString(this decimal value, CultureInfo cultureInfo) {
            return Convert.ToString(value, cultureInfo);
        } 

        #endregion

        #region Convert Double to string

        /// <summary>
        /// Convert double to string with current Culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value) {
            return DoubleToString(value, CultureInfo.CurrentCulture);
        }

        public static string DoubleToStringInvariantCulture(this double value) {
            return DoubleToString(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert double to string with culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information string.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value, string cultureInfo) {
            return DoubleToString(value, CultureInfo.CreateSpecificCulture(cultureInfo));
        }

        /// <summary>
        /// Convert double to string with culture info.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information CultureInfo.</param>
        /// <returns></returns>
        public static string DoubleToString(this double value, CultureInfo cultureInfo) {
            return Convert.ToString(value, cultureInfo);
        } 

        #endregion

        #region Convert Integer to string

        /// <summary>
        /// Convert integer to string with current Culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Int32ToString(this int value) {
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        } 

        #endregion

        #endregion
    }
}
