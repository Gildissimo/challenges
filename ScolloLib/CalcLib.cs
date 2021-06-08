using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace ScolloLib
{
    public static class CalcLib
    {
        /// <summary>
        /// Basic adder based on csv string int numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int Add(String numbers)
        {
            System.Diagnostics.Trace.TraceInformation("call  int Add(String numbers)");
            try
            {
               
                int total = 0;
                
                //check if a custom delimiter is given
                String delimiterFormat = @"([/][/][\[])(.+)([\]][/][/])(.+)";
                var delimiterMatch = Regex.Match(numbers, delimiterFormat);

                String doubleDelimiterFormat = @"([/][/][\[])(.+)([\]])([\[])(.+)([\]][/][/])(.+)";
                var doubleDelimiterMatch = Regex.Match(numbers, doubleDelimiterFormat);

                if (doubleDelimiterMatch.Success)
                {
                    var delimiter = doubleDelimiterMatch.Groups[2].Value;
                    var delimiter2 = doubleDelimiterMatch.Groups[5].Value;
                    var numberString = doubleDelimiterMatch.Groups[7].Value;
                    System.Diagnostics.Trace.TraceInformation("delimiter found:" + delimiter);
                    System.Diagnostics.Trace.TraceInformation("delimiter2 found:" + delimiter2);
                    System.Diagnostics.Trace.TraceInformation("numbers found:" + numberString);

                    return SumArrayString((new List<String>() { delimiter, delimiter2 }).ToArray(), numberString);
                }
                else if (delimiterMatch.Success)
                {
                    var delimiter = delimiterMatch.Groups[2].Value;
                    var numberString = delimiterMatch.Groups[4].Value;
                    System.Diagnostics.Trace.TraceInformation("delimiter found:"+ delimiter);
                    System.Diagnostics.Trace.TraceInformation("numbers found:" + numberString);

                    return SumArrayString((new List<String>() { delimiter }).ToArray(), numberString);
                }
                else {
                    return SumArrayString((new List<String>() { "," }).ToArray(), numbers);
                }

                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int SumArrayString(string[] delimiters, string numbersString)
        {
            int total = 0;
            string regexDelimiter = "";
            foreach (var delimiter in delimiters)
            {
                if(regexDelimiter!="")
                {
                    regexDelimiter += "|";
                }
                foreach (var dchar in delimiter)
                {
                        regexDelimiter += (@"\" + dchar.ToString());
                }
               
            }
            

            System.Diagnostics.Trace.TraceInformation("regexDelimiter found:" + regexDelimiter);

            var numberArray = Regex.Split(numbersString, regexDelimiter);
            foreach (var number in numberArray)
            {
                String stringNumberTrimmed = number.Trim();
                System.Diagnostics.Trace.TraceInformation("|" + stringNumberTrimmed + "|");
                int parsedNumber = 0;
                if (stringNumberTrimmed != "")
                {
                    var parsed = int.TryParse(stringNumberTrimmed, out parsedNumber);
                    if (parsed)
                    {
                        if (parsedNumber > 0)
                        {
                            if (parsedNumber < 1000)
                            {
                                total += parsedNumber;
                            }
                        }
                        else
                        {
                            throw new NegativeException(stringNumberTrimmed);
                        }
                    }
                    else
                    {
                        throw new ParseException(stringNumberTrimmed);
                    }
                }

            }

            return total;
        }
    }

    public class NegativeException : Exception
    {
        public NegativeException()
        {

        }

        public NegativeException(string negativenumber)
            : base(String.Format("negatives not allowed: {0}", negativenumber))
        {

        }
    }

    public class ParseException : Exception
    {
        public ParseException()
        {

        }

        public ParseException(string number)
            : base(String.Format("Unable to parse number: {0}", number))
        {

        }
    }
}
