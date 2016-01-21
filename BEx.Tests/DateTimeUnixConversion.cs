using System;
using BEx.ExchangeEngine.Utilities;
using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Time.Conversion")]
    public class DateTimeUnixConversion
    {
        [Test]
        public void UnixTime_CircularConversion_Success()
        {
            var testDate = DateTime.UtcNow;

            var unixTime = testDate.ToUnixTime();

            var converted = unixTime.ToDateTimeUTC();

            Assert.That(converted.Second == testDate.Second);
            Assert.That(converted.Minute == testDate.Minute);
            Assert.That(converted.Hour == testDate.Hour);
            Assert.That(converted.Date == testDate.Date);
        }

        [Test]
        public void UnixTime_UTCResponse()
        {
            var response = new DateTime(2015, 3, 27, 0, 4, 9, DateTimeKind.Utc);

            var unixTime = response.ToUnixTime();

            var final = unixTime.ToDateTimeUTC();

            Assert.IsTrue(final == response);
        }
    }
}