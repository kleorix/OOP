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
            return dateObj.DateToString(DATE_PATTERN);
        }

        /// <summary>
        /// Dates to string with current culture.
        /// </summary>
        /// <param name="dateObj">The date obj.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string DateToString(this DateTime dateObj, string format) {
            return dateObj.DateToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Dates to string. <para />
        /// The example displays the following output:<para />
        ///    d Format Specifier      de-DE Culture                               01.10.2008 <para />
        ///    d Format Specifier      en-US Culture                                10/1/2008 <para />
        ///    d Format Specifier      es-ES Culture                               01/10/2008 <para />
        ///    d Format Specifier      fr-FR Culture                               01/10/2008 <para />
        ///                                                                                   <para />
        ///    D Format Specifier      de-DE Culture                Mittwoch, 1. Oktober 2008 <para />
        ///    D Format Specifier      en-US Culture              Wednesday, October 01, 2008 <para />
        ///    D Format Specifier      es-ES Culture         miércoles, 01 de octubre de 2008 <para />
        ///    D Format Specifier      fr-FR Culture                  mercredi 1 octobre 2008 <para />
        ///                                                                                   <para />
        ///    f Format Specifier      de-DE Culture          Mittwoch, 1. Oktober 2008 17:04 <para />
        ///    f Format Specifier      en-US Culture      Wednesday, October 01, 2008 5:04 PM <para />
        ///    f Format Specifier      es-ES Culture   miércoles, 01 de octubre de 2008 17:04 <para />
        ///    f Format Specifier      fr-FR Culture            mercredi 1 octobre 2008 17:04 <para />
        ///                                                                                   <para />
        ///    F Format Specifier      de-DE Culture       Mittwoch, 1. Oktober 2008 17:04:32 <para />
        ///    F Format Specifier      en-US Culture   Wednesday, October 01, 2008 5:04:32 PM <para />
        ///    F Format Specifier      es-ES Culture miércoles, 01 de octubre de 2008 17:04:3 <para />
        ///    F Format Specifier      fr-FR Culture         mercredi 1 octobre 2008 17:04:32 <para />
        ///                                                                                   <para />
        ///    g Format Specifier      de-DE Culture                         01.10.2008 17:04 <para />
        ///    g Format Specifier      en-US Culture                        10/1/2008 5:04 PM <para />
        ///    g Format Specifier      es-ES Culture                         01/10/2008 17:04 <para />
        ///    g Format Specifier      fr-FR Culture                         01/10/2008 17:04 <para />
        ///                                                                                   <para />
        ///    G Format Specifier      de-DE Culture                      01.10.2008 17:04:32 <para />
        ///    G Format Specifier      en-US Culture                     10/1/2008 5:04:32 PM <para />
        ///    G Format Specifier      es-ES Culture                      01/10/2008 17:04:32 <para />
        ///    G Format Specifier      fr-FR Culture                      01/10/2008 17:04:32 <para />
        ///                                                                                   <para />
        ///    m Format Specifier      de-DE Culture                               01 Oktober <para />
        ///    m Format Specifier      en-US Culture                               October 01 <para />
        ///    m Format Specifier      es-ES Culture                               01 octubre <para />
        ///    m Format Specifier      fr-FR Culture                                1 octobre <para />
        ///                                                                                   <para />
        ///    o Format Specifier      de-DE Culture              2008-10-01T17:04:32.0000000 <para />
        ///    o Format Specifier      en-US Culture              2008-10-01T17:04:32.0000000 <para />
        ///    o Format Specifier      es-ES Culture              2008-10-01T17:04:32.0000000 <para />
        ///    o Format Specifier      fr-FR Culture              2008-10-01T17:04:32.0000000 <para />
        ///                                                                                   <para />
        ///    r Format Specifier      de-DE Culture            Wed, 01 Oct 2008 17:04:32 GMT <para />
        ///    r Format Specifier      en-US Culture            Wed, 01 Oct 2008 17:04:32 GMT <para />
        ///    r Format Specifier      es-ES Culture            Wed, 01 Oct 2008 17:04:32 GMT <para />
        ///    r Format Specifier      fr-FR Culture            Wed, 01 Oct 2008 17:04:32 GMT <para />
        ///                                                                                   <para />
        ///    s Format Specifier      de-DE Culture                      2008-10-01T17:04:32 <para />
        ///    s Format Specifier      en-US Culture                      2008-10-01T17:04:32 <para />
        ///    s Format Specifier      es-ES Culture                      2008-10-01T17:04:32 <para />
        ///    s Format Specifier      fr-FR Culture                      2008-10-01T17:04:32 <para />
        ///                                                                                   <para />
        ///    t Format Specifier      de-DE Culture                                    17:04 <para />
        ///    t Format Specifier      en-US Culture                                  5:04 PM <para />
        ///    t Format Specifier      es-ES Culture                                    17:04 <para />
        ///    t Format Specifier      fr-FR Culture                                    17:04 <para />
        ///                                                                                   <para />
        ///    T Format Specifier      de-DE Culture                                 17:04:32 <para />
        ///    T Format Specifier      en-US Culture                               5:04:32 PM <para />
        ///    T Format Specifier      es-ES Culture                                 17:04:32 <para />
        ///    T Format Specifier      fr-FR Culture                                 17:04:32 <para />
        ///                                                                                   <para />
        ///    u Format Specifier      de-DE Culture                     2008-10-01 17:04:32Z <para />
        ///    u Format Specifier      en-US Culture                     2008-10-01 17:04:32Z <para />
        ///    u Format Specifier      es-ES Culture                     2008-10-01 17:04:32Z <para />
        ///    u Format Specifier      fr-FR Culture                     2008-10-01 17:04:32Z <para />
        ///                                                                                   <para />
        ///    U Format Specifier      de-DE Culture     Donnerstag, 2. Oktober 2008 00:04:32 <para />
        ///    U Format Specifier      en-US Culture   Thursday, October 02, 2008 12:04:32 AM <para />
        ///    U Format Specifier      es-ES Culture    jueves, 02 de octubre de 2008 0:04:32 <para />
        ///    U Format Specifier      fr-FR Culture            jeudi 2 octobre 2008 00:04:32 <para />
        ///                                                                                   <para />
        ///    Y Format Specifier      de-DE Culture                             Oktober 2008 <para />
        ///    Y Format Specifier      en-US Culture                            October, 2008 <para />
        ///    Y Format Specifier      es-ES Culture                          octubre de 2008 <para />
        ///    Y Format Specifier      fr-FR Culture                             octobre 2008 <para />
        /// </summary>
        /// <param name="dateObj">The date obj.</param>
        /// <param name="format"></param>
        /// <param name="cultureInfo">The Culture Info.</param>
        /// <returns></returns>
        public static string DateToString(this DateTime dateObj, string format, IFormatProvider cultureInfo) {
            return dateObj.ToString(format, cultureInfo);
        }

        public static string DateToStringEnUS(this DateTime dateObj, string format) {
            return dateObj.ToString(format, CultureInfo.CreateSpecificCulture(StringHelper.US_CULTURE));
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
            return StringToDate(dateObj, DATE_PATTERN);
        }

        public static DateTime? StringToDate(string dateObj, string format) {
            return StringToDate(dateObj, format, DateTimeFormatInfo.InvariantInfo);
        }

        public static DateTime? StringToDate(string date, string format, IFormatProvider provider) {
            DateTime result;
            if (DateTime.TryParseExact(date.Trim(), format, provider, DateTimeStyles.None, out result)) {
                return result;
            }
            return null;
        } 

        #endregion
    }
}
