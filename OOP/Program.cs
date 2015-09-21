using System;
using System.Globalization;
using MHGCommon;

namespace OOP {
    class Program {
        static void Main(string[] args) {
            var date = DateTime.Today;
            Console.WriteLine(date.DateToString());
            Console.WriteLine(date.DateToString(DateHelper.DATE_3MONTH_PATTERN));
            Console.WriteLine(date.DateToString("D",CultureInfo.CreateSpecificCulture(StringHelper.GERMAN_CULTURE)));
        }
    }
}
