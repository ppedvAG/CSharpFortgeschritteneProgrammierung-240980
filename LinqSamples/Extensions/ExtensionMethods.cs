using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSamples.Extensions
{
    // muss eine static Klasse sein
    // Es ist eine gute Idee Klassen mit Extensions in einen eigenen Namespace zu legen
    public static class ExtensionMethods
    {
        // Extensions methods muessen statisch sein und der erste Parameter benutzt das this Keyword um den Typ zu "erweitern"
        public static int Quersumme(this int number)
        {
            return number.ToString().Sum(e => (int)char.GetNumericValue(e));
        }
    }
}
