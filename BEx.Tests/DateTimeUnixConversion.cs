using BEx.ExchangeEngine.Utilities;
using NUnit.Framework;
using System;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Time.Conversion")]
    public class DateTimeUnixConversion
    {
        [Test]
        public void UnixTime_CircularConversion_Success()
        {
            DateTime testDate = DateTime.UtcNow;

            double unixTime = testDate.ToUnixTime();

            DateTime converted = unixTime.ToDateTimeUTC();

            Assert.That(converted.Second == testDate.Second);
            Assert.That(converted.Minute == testDate.Minute);
            Assert.That(converted.Hour == testDate.Hour);
            Assert.That(converted.Date == testDate.Date);
        }

        [Test]
        public void UnixTime_UTCResponse()
        {
            DateTime response = new DateTime(2015, 3, 27, 0, 4, 9, DateTimeKind.Utc);

            double unixTime = response.ToUnixTime();

            DateTime final = unixTime.ToDateTimeUTC();

            Assert.IsTrue(final == response);
        }
    }
}