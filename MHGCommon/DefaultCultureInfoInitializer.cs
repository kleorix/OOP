using System;
using System.Globalization;
using System.Threading;

namespace MHGCommon {

    /// <summary>
    /// Initialize culture 
    /// How to use
    /// </summary>
    public static class DefaultCultureInfoInitializer {
        private const string DEFAULT_CULTURE = "en-US";
        private const string DEFAULT_DECIMAL_SYMBOL = ".";
        private const string DEFAULT_GROUP_SYMBOL = ",";

        /// <summary>
        /// Applies the en-US culture.
        /// </summary>
        public static void ApplyCulture() {
            var cultureInfo = CultureInfo.CreateSpecificCulture(GetDefaultCulture());
            var defaultDecimalSymbol = GetDefaultDecimalSymbol();
            var defaultGroupSymbol = GetDefaultGroupSymbol();

            ApplyCulture(cultureInfo, defaultDecimalSymbol, defaultGroupSymbol);
        }

        public static void ApplyCulture(CultureInfo cultureInfo) {
            if (cultureInfo == null) {
                cultureInfo = CultureInfo.CreateSpecificCulture(GetDefaultCulture());
            }
            ApplyCulture(cultureInfo, cultureInfo.NumberFormat.NumberDecimalSeparator, cultureInfo.NumberFormat.NumberGroupSeparator);
        }

        public static void ApplyCulture(string culture) {
            var cultureInfo = CultureInfo.CreateSpecificCulture(culture);
            ApplyCulture(cultureInfo, cultureInfo.NumberFormat.NumberDecimalSeparator, cultureInfo.NumberFormat.NumberGroupSeparator);
        }

        /// <summary>
        /// Applies the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="decimalSymbol">The decimal symbol.</param>
        /// <param name="groupSymbol">The group symbol.</param>
        /// <exception cref="System.Exception">Culture is null of decimal symbol is empty</exception>
        public static void ApplyCulture(CultureInfo culture, string decimalSymbol, string groupSymbol) {
            if (culture == null) {
                culture = CultureInfo.CreateSpecificCulture(GetDefaultCulture());
            }
            if (string.IsNullOrEmpty(decimalSymbol)) {
                decimalSymbol = GetDefaultDecimalSymbol();
            }

            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = decimalSymbol;
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = decimalSymbol;
            Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = decimalSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.CurrencyDecimalSeparator = decimalSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.NumberDecimalSeparator = decimalSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.PercentDecimalSeparator = decimalSymbol;

            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = groupSymbol;
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = groupSymbol;
            Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = groupSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.CurrencyGroupSeparator = groupSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.NumberGroupSeparator = groupSymbol;
            CultureInfo.DefaultThreadCurrentCulture.NumberFormat.PercentGroupSeparator = groupSymbol;
        }

        private static string GetDefaultCulture() {
            return DEFAULT_CULTURE;
        }

        private static string GetDefaultDecimalSymbol() {
            return DEFAULT_DECIMAL_SYMBOL;
        }

        private static string GetDefaultGroupSymbol() {
            return DEFAULT_GROUP_SYMBOL;
        }
    }
}
