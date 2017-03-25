using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class Tests
    {
        BabysitterKat.Program.BabySitterFeeCalculator babySitterFeeCalculator = new BabysitterKat.Program.BabySitterFeeCalculator();

        [Fact]
        public void TakesAString()
        {
            string timeString = "5:00PM";
            var result = babySitterFeeCalculator.TakeTimeString(timeString);
            result.ShouldBe("5:00PM");
        }
        [Fact]
        public void MakeStringIntoArray()
        {
            string timeString = "5:00AM";
            var result = babySitterFeeCalculator.MakeCharArray(timeString);
            result[0].ShouldBeOfType<char>();
            result[5].ShouldBeOfType<char>();
            result[0].ShouldBe('5');
        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithPm()
        {
            string timeString = "5:00PM";
            var result = babySitterFeeCalculator.CheckAmOrPm(timeString);
            result.ShouldBe(true);

        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithAm()
        {
            string timeString = "5:00AM";
            var result = babySitterFeeCalculator.CheckAmOrPm(timeString);
            result.ShouldBe(false);

        }
        [Fact]
        public void RemoveAmFromCharArray()
        {
            string timeString = "5:00AM";
            var timeArray = babySitterFeeCalculator.MakeCharArray(timeString);
            var result = babySitterFeeCalculator.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemovePmFromCharArray()
        {
            string timeString = "5:00PM";
            var timeArray = babySitterFeeCalculator.MakeCharArray(timeString);
            var result = babySitterFeeCalculator.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemoveColonFromCharArray()
        {
            string timeString = "5:00";
            var timeArray = babySitterFeeCalculator.MakeCharArray(timeString);
            var result = babySitterFeeCalculator.RemoveColon(timeArray);
            result.ShouldBe("5.00");
        }
        [Fact]
        public void ConvertTimeArrayToDouble()
        {
            string timeString = "5.00";
            var timeArray = babySitterFeeCalculator.MakeCharArray(timeString);
            var result = babySitterFeeCalculator.ConvertTimeArrayToDouble(timeArray);
            result.ShouldBe(5.00);
        }
        [Fact]
        public void MakeMilitaryTimeMid()
        {
            bool isPm = false;
            var result = babySitterFeeCalculator.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(24.00);
        }
        [Fact]
        public void MakeMilitaryTimeNoon()
        {
            bool isPm = true;
            var result = babySitterFeeCalculator.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(12.00);
        }
        [Fact]
        public void MakeMilitaryTimeAm()
        {
            bool isPm = false;
            var result = babySitterFeeCalculator.MakeMilitaryTime(11.00, isPm);
            result.ShouldBe(11.00);
        }
        [Fact]
        public void MakeMilitaryTimePm()
        {
            bool isPm = true;
            var result = babySitterFeeCalculator.MakeMilitaryTime(1.00, isPm);
            result.ShouldBe(13.00);
        }
        [Fact]
        public void ConvertTimeString()
        {
            var result = babySitterFeeCalculator.ConvertTimeString("5:00PM");
            result.ShouldBe(17.00);
        }
        [Fact]
        public void CheckStartTimeAfter5PmWhenTimeIs6Pm()
        {
            var result = babySitterFeeCalculator.CheckStartTime(18.0);
            result.ShouldBe(true);
        }
        [Fact]
        public void CheckStartTime2Am()
        {
            var result = babySitterFeeCalculator.CheckStartTime(2.0);
            result.ShouldBe(true);
        }
    }
}
