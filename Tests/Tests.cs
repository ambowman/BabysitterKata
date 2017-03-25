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
        [Fact]
        public void CheckStartTimeWhenTimeIs3Pm()
        {
            var result = babySitterFeeCalculator.CheckStartTime(15.0);
            result.ShouldBe(false);
        }
        public void CheckStartTimeNotBetween5PmAnd4Am()
        {
            var result = babySitterFeeCalculator.CheckStartTime(6.0);
            result.ShouldBe(false);
        }
        [Fact]
        public void CheckEndTimeNoLaterThan4Am()
        {
            var result = babySitterFeeCalculator.CheckEndTime(3.0, 17.0);
            result.ShouldBe(true);
        }
        [Fact]
        public void CheckEndTimeLaterThan4Am()
        {
            var result = babySitterFeeCalculator.CheckEndTime(5.0, 17.0);
            result.ShouldBe(false);
        }
        [Fact]
        public void CheckEndTimeNoLaterThan4AmAndNotEqualStartTime()
        {
            var result = babySitterFeeCalculator.CheckEndTime(5.0, 17.0);
            result.ShouldBe(false);
        }
        [Fact]
        public void CheckIsNoonWhenNoonIsTrue()
        {
            var result = babySitterFeeCalculator.CheckIsNoon("12:00PM");
            result.ShouldBe(true);
        }
        [Fact]
        public void CheckIsNoonWhenNotNoon()
        {
            var result = babySitterFeeCalculator.CheckIsNoon("1:00PM");
            result.ShouldBe(false);
        }
        [Fact]
        public void ConvertRawTimeDoubleToFractionalHoursGiven4point3Becomes4point5()
        {
            var result = babySitterFeeCalculator.ConvertRawTimeDoubleToFractionalHours(4.3);
            result.ShouldBe(4.5);
        }
        [Fact]
        public void ConvertRawTimeDoubleToFractionalHoursGiven4Remians4()
        {
            var result = babySitterFeeCalculator.ConvertRawTimeDoubleToFractionalHours(4.0);
            result.ShouldBe(4.0);
        }
        [Fact]
        public void ConvertRawTimeDoubleToFractionalHoursGiven12Point45Returns12Point75()
        {
            var result = babySitterFeeCalculator.ConvertRawTimeDoubleToFractionalHours(12.45);
            result.ShouldBe(12.75);
        }
        [Fact]
        public void StartAndEndAfterMidnightStart1AmEnd2AmBed1Am()
        {
            var result = babySitterFeeCalculator.CalculateRateStartAndEndAfterMid(1.00, 2.00);
            result.ShouldBe(16);
        }
        [Fact]
        public void StartAndEndAfterMidnightStart1AmEnd4AmBed10Pm()
        {
            var result = babySitterFeeCalculator.CalculateRateStartAndEndAfterMid(1.00, 4.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void BedBeforeOrEqualStartBothBeforeMidEndBeforeMidStart5PmBed3PmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(17.00, 23.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid5PmBed3PmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(17.00, 1.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid()
        {
            var result = babySitterFeeCalculator.CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(17.00, 23.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void StartBeforeMidBedtimeEqualsOrAfterEndAndEndAfterMid()
        {
            var result = babySitterFeeCalculator.CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(17.00, 1.00);
            result.ShouldBe(100);
        }
        [Fact]
        public void StartBeforeBedBothBeforeMidEndBeforeMid()
        {
            var result = babySitterFeeCalculator.CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(17.00, 20.00, 23.00);
            result.ShouldBe(60);
        }
        [Fact]
        public void StartBeforeBedBothBeforeMidEndAfterMid()
        {
            var result = babySitterFeeCalculator.CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(17.00, 20.00, 1.00);
            result.ShouldBe(84);
        }
        [Fact]
        public void CalculateRateStart1AmBed10PmEnd2Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(1.00, 22.00, 2.00);
            result.ShouldBe(16);
        }
        [Fact]
        public void CalculateRateStart2AmBed1AmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(2.00, 1.00, 3.00);
            result.ShouldBe(16);
        }
        [Fact]
        public void CalculateRateStart1AmBed1AmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(1.00, 1.00, 3.00);
            result.ShouldBe(32);
        }
        [Fact]
        public void CalculateRateStart1AmBed2AmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(1.00, 2.00, 3.00);
            result.ShouldBe(32);
        }
        [Fact]
        public void CalculateRateStart1AmBed3AmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(1.00, 3.00, 3.00);
            result.ShouldBe(32);
        }
        [Fact]
        public void CalculateRateStart5PmBed3PmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 15.00, 23.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void CalculateRateStart5PmBed5PmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 17.00, 23.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void CalculateRateStart5PmBed3PmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 15.00, 1.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void CalculateRateStart5PmBed5PmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 17.00, 1.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void CalculateRateStart5PmBed11PmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 23.00, 23.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void CalculateRateStart5PmBed1AmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 1.00, 23.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void CalculateRateStart5PmBed1AmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 1.00, 1.00);
            result.ShouldBe(100);
        }
        [Fact]
        public void CalculateRateStart10PmBed1AmEnd2Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(22.00, 1.00, 2.00);
            result.ShouldBe(56);
        }
        [Fact]
        public void CalculateRateStart5PmBed2AmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 2.00, 1.00);
            result.ShouldBe(100);
        }
        [Fact]
        public void CalculateRateStart5PmBed6PmEnd11Pm()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 18.00, 23.00);
            result.ShouldBe(52);
        }
        [Fact]
        public void CalculateRateStart5PmBed6PmEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 18.00, 1.00);
            result.ShouldBe(76);
        }
        [Fact]
        public void CalculateRateStartMidBed10PmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(24.00, 22.00, 3.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void CalculateRateStartMidBed1AmEnd3Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(24.00, 1.00, 3.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void CalculateRateStart5PMBed10PmEndMid()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 22.00, 24.00);
            result.ShouldBe(76);
        }
        [Fact]
        public void CalculateRateStart5PMBed1AmEndMid()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 1.00, 24.00);
            result.ShouldBe(84);
        }
        [Fact]
        public void CalculateRateStart10PMBedMidEnd1Am()
        {
            var result = babySitterFeeCalculator.CalculateRate(22.00, 24.00, 1.00);
            result.ShouldBe(40);
        }
        [Fact]
        public void CalculateRateStart10PMBedMidEndMid()
        {
            var result = babySitterFeeCalculator.CalculateRate(22.00, 24.00, 24.00);
            result.ShouldBe(24);
        }
        [Fact]
        public void CalculateRateStart5PMBed5PmEndMid()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 17.00, 24.00);
            result.ShouldBe(56);
        }
        [Fact]
        public void CalculateRateStart5ThirtyPMBed3PmEnd11PM()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.30, 15.00, 23.00);
            result.ShouldBe(48);
        }
        [Fact]
        public void CalculateRateStart12ThirtyAMBed3PmEnd4AM()
        {
            var result = babySitterFeeCalculator.CalculateRate(24.30, 15.00, 4.00);
            result.ShouldBe(64);
        }
        [Fact]
        public void CalculateRateStart5ThirtyPMBed3PmEnd1AM()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.30, 15.00, 1.00);
            result.ShouldBe(72);
        }
        [Fact]
        public void CalculateRateStart5ThirtyPMBed3PmEnd1ThirtyAM()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.30, 15.00, 1.30);
            result.ShouldBe(80);
        }
        [Fact]
        public void CalculateRateStart5PMBed3PmEnd1ThirtyAM()
        {
            var result = babySitterFeeCalculator.CalculateRate(17.00, 15.00, 1.30);
            result.ShouldBe(88);
        }

    } 
}
