using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Coinbase;
namespace BEx.Tests
{
    [TestFixture]
    public class Scratch
    {
        [Test]
        public  void Socket()
        {
            var conf = BEx.ExchangeEngine.Coinbase.Configuration.Singleton;

            var obs = new ExchangeSocketObserver(conf);
            obs.Begin();
            obs.Subscribe(new TradingPair(Currency.BTC, Currency.USD));


            bool run = true;

            while (run)
            {
                { }
            }
        }
    }
}
