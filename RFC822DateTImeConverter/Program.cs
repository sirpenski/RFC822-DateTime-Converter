using System;
using System.Collections.Generic;
using Sirpenski.AspNetCore.Utilities;

namespace RFC822StringToDateTImeConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> TestData = new List<string>()
            {
                "Mon, 11 Mar 2019 01:57:00 EST",
                "11 Mar 2019 01:57:23 EST",
                "Mon, 11 Mar 2019 01:57:00 -0500",
                 "Mon, 11 Mar 2019 01:57 A",
                 "11 Mar 2019 01:57 A",
                "11 Mar 2019 01 A",
                "Mon, 11 Mar 2019 01:57 N",
                "11 Mar 2019 01:57 N", 
                "11 Mar 2019 01 N",
                "mar 2019 01:57 -0500",
                "Mon, 11 Mar 2019 01 EST",
                "Mon, 11 Mar 2019 01:00 EST",
                "Mon, 11 Mob 2019 01:00 EST",
                "abc, 11 Mar 2019 01:57:00 -0500",
                "Mon,  11   Mar  2019   01:57:00   -0500"

            };


            bool loopControl = true;

            while (loopControl)
            {
                Console.WriteLine("");
                Console.WriteLine("Select Test To Run\n");
                Console.WriteLine("Press 1 For Manual Input, Press 2 For Preset Tests, Press 3 For TryParse With DateTime object, Esc to Exit");
    
                Console.Write("What Do You Wish To Do? ");

                ConsoleKeyInfo k = Console.ReadKey(true);

                if (k.Key != ConsoleKey.Escape && k.KeyChar != 'x' && k.KeyChar != 'X')
                {

                    if (k.KeyChar == '1')
                    {
                        Console.WriteLine();
                        ManualInput();
                    }
                    else if (k.KeyChar == '2')
                    {
                        Console.WriteLine();
                        ParseRfc822Test(TestData);
                    }

                    else if (k.KeyChar == '3')
                    {
                        Console.WriteLine("");
                        DateTimeObjectTryParseTests(TestData);
                    }
                }
                else 
                {
                    loopControl = false;

                }

            }
        }



        /// <summary>
        /// Manually enter in dates
        /// </summary>
        static void ManualInput()
        {
            bool bContinue = true;

            while(bContinue)
            {
                Console.WriteLine("");
                Console.Write("Enter RFC822 Date: ");

                string s = Console.ReadLine();
                ParseRfc822(s);


                Console.WriteLine("");
                Console.Write("Do You Wish To Do Another (y/n) ");
                ConsoleKeyInfo k = Console.ReadKey();

                string c = k.Key.ToString().ToLower();

                if (string.Compare(c, "y") != 0)
                {
                    bContinue = false;
                }

                else
                {
                    Console.WriteLine("");
                }


            }

        }



        // ********************************************************
        // DateTIme Parse trys
        // ********************************************************
        static void DateTimeObjectTryParseTests(List<string>testData)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < testData.Count; i++)
            {
                DateTimeObjectTryParseTest(testData[i]);
                Console.WriteLine("");
            }

        }


        // *********************************************************
        // try parsing with Microsoft datetime object
        // *********************************************************
        static void DateTimeObjectTryParseTest(string s)
        {


            bool DateTimeParseResult = false;
            string DateTimeParseResultMessage = "EXCEPTION ERROR";
            DateTime tmp = new DateTime();
            try
            {
                tmp = DateTime.Parse(s);
                DateTimeParseResult = true;
                DateTimeParseResultMessage = "SUCCESS";
            }
            catch (Exception) { }

            Console.WriteLine("INPUT RFC822:               " + s);

            if (DateTimeParseResult)
            {
                Console.WriteLine("RESULT DATETIME.PARSE:      " + tmp.ToString("ddd, dd MMM yyyy hh:mm:ss zzz"));
            }
            else
            {
                Console.WriteLine("RESULT DATETIME.PARSE:      " + DateTimeParseResultMessage);
            }


            DateTimeParseResult = false;
            DateTimeParseResultMessage = "EXCEPTION ERROR";
            try
            {
                tmp = DateTime.ParseExact(s, "ddd, dd MMM yyyy hh:mm:ss zzz", null);
                DateTimeParseResult = true;
                DateTimeParseResultMessage = "SUCCESS";
            }
            catch (Exception) { }

            if (DateTimeParseResult)
            {
                Console.WriteLine("RESULT DATETIME.PARSEEXACT: " + tmp.ToString("ddd, dd MMM yyyy hh:mm:ss zzz"));
            }
            else
            {
                Console.WriteLine("RESULT DATETIME.PARSEEXACT: " + DateTimeParseResultMessage);
            }



        }



        // *************************************************************
        // Preset Tests
        // *************************************************************
        static void ParseRfc822Test(List<string>testData)
        {
            Console.WriteLine("");
            Console.WriteLine("");

            for (int i = 0; i < testData.Count; i++)
            {
                ParseRfc822(testData[i]);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            }


        }


        /// <summary>
        /// Parses the string.  Dumps results to console window
        /// </summary>
        /// <param name="s"></param>
        static void ParseRfc822(string s)
        {

            PFSRfc822DateTimeConverter converter = new PFSRfc822DateTimeConverter();


            Console.WriteLine(FixWidth("INPUT RFC822:") + s);

            // try and parse.
            if (converter.TryParse(s, out DateTime tmpDt))
            {

                    Console.WriteLine(FixWidth("RESULT RFC822:") + converter.ResultRfc822);
                    Console.WriteLine(FixWidth("RESULT TIMEZONE OFFSET:") + converter.UtcOffset.ToString("c"));
                    Console.WriteLine(FixWidth("RESULT UTC DATETIME:") + converter.ResultUtc.ToString("yyyy-MM-dd HH:mm:ss zzz"));
                    Console.WriteLine(FixWidth("RESULT LOCAL DATETIME:") + tmpDt.ToString("yyyy-MM-dd HH:mm:ss zzz"));
                    Console.WriteLine(FixWidth("RESULT DST IN USE:") + (converter.TimeZoneUsesDaylightSavingsTime ? "YES" : "NO"));
                    Console.WriteLine(FixWidth("RESULT MESSAGE:") + "VALID FORMAT");
                    
                    WriteDateParts(converter);
  
            }

            // error encountered, bail
            else
            {
                Console.WriteLine(FixWidth("RESULT MESSAGE:") + "INVALID FORMAT");

                WriteDateParts(converter);
            }

        }

        /// <summary>
        /// Common routine just to dump the dateparts array
        /// </summary>
        /// <param name="conv"></param>
        private static void WriteDateParts(PFSRfc822DateTimeConverter conv)
        {
            Console.WriteLine("");
            for (int i = 0; i < conv.DATEPART_KEYS.Count; i++)
            {
                string datePartValue = conv.GetDatePartValue(conv.DATEPART_KEYS[i]);
                Console.WriteLine(FixWidth("DATEPART " + conv.DATEPART_KEYS[i] + ":") + datePartValue);
            }

        }


        // fixes the width for console display
        private static string FixWidth(string caption)
        {
             return caption.PadRight(36);
        }

    }
}
