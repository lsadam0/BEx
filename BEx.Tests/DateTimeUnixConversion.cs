using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BEx.ExchangeEngine.Utilities;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Time.Conversion")]
    public class DateTimeUnixConversion
    {
        [Test]
        public void UnixTime_CircularConversion_Success()
        {
            DateTime testDate = DateTime.Now;

            double unixTime = UnixTime.DateTimeToUnixTimestamp(testDate);

            DateTime converted = UnixTime.UnixTimeStampToDateTime(unixTime);

            Assert.That(converted == testDate);

        }
    }
}
