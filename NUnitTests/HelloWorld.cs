using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BEx.NUnitTests
{

    [TestFixture]
    public class NUnitTests
    {

        [Test]
        public void SumOfTwoNumbers()
        {
            Assert.AreEqual(10, 5 + 5);
        }



        [Test]
        public void AreTheValuesTheSame()
        {

            Assert.AreSame(10, 5 + 6);

        }

    }


}
