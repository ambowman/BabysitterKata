﻿using System;
using System.Linq;

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

            while (!end.Equals("YES"))
            {
                Console.WriteLine("Welcome to the babysitter fee calculator!");
                Console.WriteLine("Please enter the times in this format: XX:XXPM Midnight is 12:00AM");
                Console.WriteLine("Please enter a start time for the babysitting service:");
                startTime = Console.ReadLine();
                doubleStartTime = babySitterFeeCalculator.CheckAndAssignStartTime(startTime);
                Console.WriteLine("Please enter an end time for the babysitting service:");
                endTime = Console.ReadLine();
                doubleEndTime = babySitterFeeCalculator.CheckAndAssignEndTime(endTime, doubleStartTime);
                Console.WriteLine("What time is the child's bedtime:");
                bedTime = Console.ReadLine();
                doubleBedTime = babySitterFeeCalculator.ConvertTimeString(bedTime);

                totalFee = babySitterFeeCalculator.CalculateRate(doubleStartTime, doubleBedTime, doubleEndTime);
                Console.WriteLine("Total fee = $" + totalFee);
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
                if (!isPm && doubleTimeDec >= 12.0)
                    return doubleTimeDec + 12.0;
                if (isPm && doubleTimeDec < 12.0)
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
                if (time.Equals("12:00PM"))
                {
                    return true;
                }
                return false;
            }
            public double CheckAndAssignStartTime(string startTime)
            {
                double doubleStartTime = 0.0;
                doubleStartTime = ConvertTimeString(startTime);

                while (!CheckStartTime(doubleStartTime) || CheckIsNoon(startTime))
                {
                    Console.WriteLine("The start time you entered was incorrect. Please enter a start time for the babysitting service:");
                    startTime = Console.ReadLine();
                    doubleStartTime = ConvertTimeString(startTime);
                }
                return doubleStartTime;
            }

            public double CheckAndAssignEndTime(string endTime, double doubleStartTime)
            {
                double doubleEndTime = 0.0;
                doubleEndTime = ConvertTimeString(endTime);
                while (!CheckEndTime(doubleEndTime, doubleStartTime) || CheckIsNoon(endTime))
                {
                    Console.WriteLine("The end time you entered was incorrect. Please enter an end time for the babysitting service:");
                    endTime = Console.ReadLine();
                    doubleEndTime = ConvertTimeString(endTime);
                }
                return doubleEndTime;
            }

            public double ConvertRawTimeDoubleToFractionalHours(double doubleTime)
            {
                double floorTime = 0.0;
                double fracTime = 0.0;
                floorTime = Math.Floor(doubleTime);
                fracTime = doubleTime - floorTime;

                doubleTime = Math.Round(((fracTime / .60) + floorTime), 2);
                return doubleTime;
            }

            public double CalculateDiffBetweenTwoTimes(double firstTimeDouble, double SecondTimeDouble)
            {
                double diff = 0;
                return diff = (firstTimeDouble - SecondTimeDouble) - Math.Floor(firstTimeDouble - SecondTimeDouble);

            }

            public int CalculateRateStartAndEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int fee = 0;
                double doubleFee = 0.0;

                if (doubleStartTime < doubleEndTime)
                {
                    doubleFee = doubleEndTime - doubleStartTime;
                    fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 16;
                }
                else if (doubleStartTime > doubleEndTime)
                {
                    doubleFee = doubleEndTime - (doubleStartTime - 24);
                    fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 16;
                }

                return fee;
            }

            public int CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int fee = 0;
                double doubleFee = 0.0;
                doubleFee = doubleEndTime - doubleStartTime;
                fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 8;

                return fee;
            }

            public int CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int fee = 0;
                double doubleFee = 0.0;

                if (CalculateDiffBetweenTwoTimes(doubleStartTime, doubleEndTime) == 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(doubleFee) * 8;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(Math.Ceiling(doubleFee)) * 16;
                    return fee;
                }
                else if (CalculateDiffBetweenTwoTimes(24, doubleStartTime) > 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 8;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(doubleFee) * 16;
                    return fee;
                }
                else if ((doubleEndTime) - Math.Floor(doubleEndTime) > 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(doubleFee) * 8;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(Math.Ceiling(doubleFee)) * 16;
                    return fee;
                }
                else


                    return fee;
            }

            public int CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(double doubleStartTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int fee = 0;
                double doubleFee = 0.0;
                doubleFee = doubleEndTime - doubleStartTime;
                fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 12;
                return fee;
            }

            public int CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int fee = 0;
                double doubleFee = 0.0;

                if (CalculateDiffBetweenTwoTimes(doubleStartTime, doubleEndTime) == 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(doubleFee) * 12;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(doubleFee) * 16;
                    return fee;
                }
                else if (CalculateDiffBetweenTwoTimes(24, doubleStartTime) > 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(Math.Ceiling(doubleFee)) * 12;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(doubleFee) * 16;
                    return fee;
                }
                else if ((doubleEndTime) - Math.Floor(doubleEndTime) > 0)
                {
                    doubleFee = 24 - doubleStartTime;
                    fee = Convert.ToInt32(doubleFee) * 12;
                    doubleFee = doubleEndTime;
                    fee = fee + Convert.ToInt32(Math.Ceiling(doubleFee)) * 16;
                    return fee;

                }
                else
                    return fee;
            }
            public int CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleBedTime = ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
                int feeStartToBed = 0;
                int feeBedToEnd = 0;
                int totalFee = 0;
                double doubleStartToBedFee = 0.0;
                double doubleBedToEndFee = 0.0;

                if ((CalculateDiffBetweenTwoTimes(doubleBedTime, doubleStartTime) > 0)
                    && (CalculateDiffBetweenTwoTimes(doubleEndTime, doubleBedTime) > 0))
                {
                    doubleStartToBedFee = doubleBedTime - doubleStartTime;
                    feeStartToBed = Convert.ToInt32(Math.Ceiling(doubleStartToBedFee)) * 12;
                    doubleBedToEndFee = doubleEndTime - doubleBedTime;
                    feeBedToEnd = Convert.ToInt32(Math.Floor(doubleBedToEndFee)) * 8;
                    totalFee = feeBedToEnd + feeStartToBed;

                    return totalFee;
                }
                else
                {
                    if (CalculateDiffBetweenTwoTimes(doubleBedTime, doubleStartTime) == 0)
                    {
                        doubleStartToBedFee = doubleBedTime - doubleStartTime;
                        feeStartToBed = Convert.ToInt32(doubleStartToBedFee) * 12;
                    }
                    else if (CalculateDiffBetweenTwoTimes(doubleBedTime, doubleStartTime) > 0)
                    {
                        doubleStartToBedFee = doubleBedTime - doubleStartTime;
                        feeStartToBed = Convert.ToInt32(Math.Ceiling(doubleStartToBedFee)) * 12;
                    }

                    if (CalculateDiffBetweenTwoTimes(doubleEndTime, doubleBedTime) == 0)
                    {
                        doubleBedToEndFee = doubleEndTime - doubleBedTime;
                        feeBedToEnd = Convert.ToInt32(doubleBedToEndFee) * 8;
                    }
                    else if (CalculateDiffBetweenTwoTimes(doubleEndTime, doubleBedTime) > 0)
                    {
                        doubleBedToEndFee = doubleEndTime - doubleBedTime;
                        feeBedToEnd = Convert.ToInt32(Math.Ceiling(doubleBedToEndFee)) * 8;
                    }

                    totalFee = feeBedToEnd + feeStartToBed;

                    return totalFee;
                }
            }
            public double CalculateDiffToDeterminePartOfHour(double doubleTime)
            {
                double diff = 0;
                return diff = doubleTime - Math.Floor(doubleTime);
            }
            public int CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {
                doubleStartTime = ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                doubleBedTime = ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
                doubleEndTime = ConvertRawTimeDoubleToFractionalHours(doubleEndTime);

                int totalFee = 0;
                double doubleStartToBedFee = 0.0;
                double doubleBedToMidFee = 0.0;
                double doubleMidToEndFee = 0.0;
                double doubleTotalFee = 0.0;

                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0)
                {
                    if (CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                    {
                        doubleStartToBedFee = Math.Floor(doubleBedTime - doubleStartTime) * 12;
                        doubleBedToMidFee = (24 - doubleBedTime) * 8;
                        doubleMidToEndFee = Math.Ceiling(doubleEndTime) * 16;
                    }
                    else if (CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                    {
                        doubleStartToBedFee = (Math.Floor(doubleBedTime) - Math.Floor(doubleStartTime)) * 12;
                        doubleBedToMidFee = Math.Floor(24 - doubleBedTime) * 8;
                        doubleMidToEndFee = Math.Ceiling(doubleEndTime) * 16;
                    }
                }
                else if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0)
                {
                    if (CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                    {
                        doubleStartToBedFee = (doubleBedTime - doubleStartTime) * 12;
                        doubleBedToMidFee = (24 - doubleBedTime) * 8;
                        doubleMidToEndFee = doubleEndTime * 16;
                    }
                    else if (CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                    {
                        doubleStartToBedFee = (Math.Ceiling(doubleBedTime - doubleStartTime)) * 12;
                        doubleBedToMidFee = Math.Floor(24 - doubleBedTime) * 8;
                        doubleMidToEndFee = doubleEndTime * 16;
                    }
                }
                else if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0)
                {
                    if (CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                    {
                        doubleStartToBedFee = Math.Ceiling(doubleBedTime - doubleStartTime) * 12;
                        doubleBedToMidFee = (24 - doubleBedTime) * 8;
                        doubleMidToEndFee = doubleEndTime * 16;
                    }
                    else if (CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                    {
                        doubleStartToBedFee = (Math.Floor(doubleBedTime) - Math.Floor(doubleStartTime)) * 12;
                        doubleBedToMidFee = Math.Ceiling(24 - doubleBedTime) * 8;
                        doubleMidToEndFee = doubleEndTime * 16;
                    }
                }
                else if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0)
                {
                    if (CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                    {
                        doubleStartToBedFee = (doubleBedTime - doubleStartTime) * 12;
                        doubleBedToMidFee = (24 - doubleBedTime) * 8;
                        doubleMidToEndFee = Math.Ceiling(doubleEndTime) * 16;
                    }
                    else if (CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                    {
                        doubleStartToBedFee = (Math.Ceiling(doubleBedTime - doubleStartTime)) * 12;
                        doubleBedToMidFee = Math.Floor(24 - doubleBedTime) * 8;
                        doubleMidToEndFee = Math.Ceiling(doubleEndTime) * 16;
                    }
                }

                doubleTotalFee = doubleStartToBedFee + doubleBedToMidFee + doubleMidToEndFee;
                totalFee = Convert.ToInt32(doubleTotalFee);

                return totalFee;
            }
            public int CalculateRate(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {

                int fee = 0;

                if (doubleEndTime <= 4.0 && (doubleStartTime < 4.0 || doubleStartTime > 24))
                {
                    fee = CalculateRateStartAndEndAfterMid(doubleStartTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime >= doubleBedTime && doubleStartTime < doubleEndTime && doubleEndTime > 17.0 && doubleStartTime < 24 && doubleBedTime > 4.00)
                {
                    fee = CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(doubleStartTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime >= doubleBedTime && doubleStartTime <= 24 && doubleEndTime <= 4.0 && doubleBedTime > 4.00)
                {
                    fee = CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(doubleStartTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime < doubleEndTime && doubleEndTime <= 24.0 && doubleStartTime < 24.0 && doubleStartTime > 4.0 && (doubleBedTime >= doubleEndTime || doubleBedTime <= 4.0))
                {
                    fee = CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(doubleStartTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime <= 24 && doubleEndTime <= 4.0 && doubleBedTime <= 4.00)
                {
                    fee = CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(doubleStartTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime < doubleBedTime && doubleBedTime < doubleEndTime && doubleEndTime <= 24.0)
                {
                    fee = CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(doubleStartTime, doubleBedTime, doubleEndTime);
                    return fee;
                }
                else if (doubleStartTime < doubleBedTime && doubleBedTime <= 24 && doubleEndTime <= 4.0 && doubleStartTime >= 17.0)
                {
                    fee = CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(doubleStartTime, doubleBedTime, doubleEndTime);
                    return fee;
                }
                else
                    return fee;
            }
        }
    }
}
