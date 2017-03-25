﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKat
{
    public class Program
    {
        static void Main(string[] args)
        {
            BabySitterFeeCalculator babySitterFeeCalculator = new BabySitterFeeCalculator();
            string startTime = "";
            string endTime = "";
            string bedTime = "";

            string end = "no";

            double doubleStartTime = 0.0;
            double doubleEndTime = 0.0;
            double doubleBedTime = 0.0;

            int totalFee = 0;

            while (!end.Equals("yes"))
            {
                Console.WriteLine("Welcome to the babysitter fee calculator!");
                Console.WriteLine("Please enter the times in this format: XX:XXPM Midnight is 12:00AM");
                Console.WriteLine("Please enter a start time for the babysitting service:");
                startTime = Console.ReadLine();
                Console.WriteLine("Please enter an end time for the babysitting service:");
                endTime = Console.ReadLine();
                Console.WriteLine("What time is the child's bedtime:");
                bedTime = Console.ReadLine();
                Console.WriteLine("Are you done calculating babysitting fees? Enter YES/NO");
                end = Console.ReadLine();
                end = end.ToUpper();
            }

        }
        public class BabySitterFeeCalculator
        {
            public string TakeTimeString(string timeString)
            {
                return timeString;
            }
            public char[] MakeCharArray(string time)
            {
                char[] timeArray = new char[time.Length]; 
                return timeArray = time.ToCharArray();
            }
            public bool CheckAmOrPm(string time)
            {
                if (time.EndsWith("PM"))
                {
                    return true;
                }
                return false;
            }
            public char[] RemoveAmPm(char[] timeArray)
            {
                timeArray = timeArray.Where(val => val != 'A').ToArray();
                timeArray = timeArray.Where(val => val != 'P').ToArray();
                timeArray = timeArray.Where(val => val != 'M').ToArray();
                return timeArray;
            }
            public char[] RemoveColon(char[] timeArray)
            {
                int i = 0;
                for (; i < timeArray.Length; ++i)
                {
                    if (timeArray[i] == ':')
                        timeArray[i] = '.';
                }
                return timeArray;
            }
            public double ConvertTimeArrayToDouble(char[] timeArray)
            {
                double timeDouble = 0.0;
                string time = new string(timeArray);
                timeDouble = Double.Parse(time);
                return timeDouble;
            }
            public double MakeMilitaryTime(double doubleTimeDec, bool isPm)
            {
                if (!isPm && doubleTimeDec == 12.0)
                    return doubleTimeDec + 12.0;
                if(isPm && doubleTimeDec< 12.0)
                    return doubleTimeDec + 12.0;
                return doubleTimeDec;
            }
            public double ConvertTimeString(string time)
            {
                double convertedDoubleTime = 0.0;
                time = time.ToUpper();
                bool isPm = CheckAmOrPm(time);

                char[] timeArray = MakeCharArray(time);
                timeArray = RemoveAmPm(timeArray);
                timeArray = RemoveColon(timeArray);
                convertedDoubleTime = ConvertTimeArrayToDouble(timeArray);
                convertedDoubleTime = MakeMilitaryTime(convertedDoubleTime, isPm);
                return convertedDoubleTime;
            }
            public bool CheckStartTime(double doubleStartTime)
            {
                if (doubleStartTime >= 17.00 || doubleStartTime < 4.00)
                    return true;
                return false;
            }
            public bool CheckEndTime(double doubleEndTime, double doubleStartTime)
            {
                if ((doubleEndTime <= 4.00 || doubleEndTime > 17.00) && doubleEndTime != doubleStartTime)
                    return true;
                return false;
            }
            public bool CheckIsNoon(string time)
            {

                return false;
            }

        }
    }
}
