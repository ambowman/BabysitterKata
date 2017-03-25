﻿using Shouldly;
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
            var result = babySitterFeeCalculator.RemoveAm(timeArray);
            result.ShouldBe("5:00");
        }
    }
}
