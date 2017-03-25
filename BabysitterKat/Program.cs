using System;
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

        }
    }
}
