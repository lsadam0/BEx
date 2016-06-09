using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BEx.ExchangeEngine.Gdax.WebSocket;
using BEx.ExchangeEngine.Gdax.WebSocket.JSON;

namespace BEx.Tests.GdaxTests.WebSocket
{
    [TestFixture]
    [Category("Gdax.WebSocket.MessageIdentification")]
    public class MessageParserCanIdentify
    {
        private void Execute(string message, Type expected)
        {
            Assert.That(!string.IsNullOrEmpty(message));

            var parser = new GdaxParser();

            var computedType = parser.Identify(message);

            Assert.IsInstanceOf(expected, computedType);
        }

        [Test]
        public void Gdax_WebSocket_Parse_Heartbeat()
        {
           this.Execute(
               "{ \"type\": \"heartbeat\", \"on\": true }",
               typeof(HeartBeat));
        }

        [Test]
        public void Gdax_WebSocket_Parse_ChangeFunds()
        {
            this.Execute(
                "{ \"type\": \"change\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"sequence\": 80, \"order_id\": \"ac928c66-ca53-498f-9c13-a110027a60e8\", \"product_id\": \"BTC-USD\", \"new_funds\": \"5.23512\", \"old_funds\": \"12.234412\", \"price\": \"400.23\", \"side\": \"sell\" }",
                typeof(ChangeFUnds));
        }

        [Test]
        public void Gdax_WebSocket_Parse_ChangeSize()
        {
            this.Execute(
                "{ \"type\": \"change\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"sequence\": 80, \"order_id\": \"ac928c66-ca53-498f-9c13-a110027a60e8\", \"product_id\": \"BTC-USD\", \"new_size\": \"5.23512\", \"old_size\": \"12.234412\", \"price\": \"400.23\", \"side\": \"sell\"\r\n}",
                typeof(ChangeSize));
        }

        [Test]
        public void Gdax_WebSocket_Parse_Done()
        {
            this.Execute(
                "{ \"type\": \"done\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"product_id\": \"BTC-USD\", \"sequence\": 10, \"price\": \"200.2\", \"order_id\": \"d50ec984-77a8-460a-b958-66f114b0de9b\", \"reason\": \"filled\", // canceled \"side\": \"sell\", \"order_type\": \"limit\", // market \"remaining_size\": \"0.2\"\r\n}",
                typeof(Done));
        }

        [Test]
        public void Gdax_WebSocket_Parse_Error()
        {
            this.Execute(
                "{ \"type\": \"error\", \"message\": \"error message\"\r\n}",
                typeof(Error));
        }

        [Test]
        public void Gdax_WebSocket_Parse_Match()
        {
            this.Execute(
                "{ \"type\": \"match\", \"trade_id\": 10, \"sequence\": 50, \"maker_order_id\": \"ac928c66-ca53-498f-9c13-a110027a60e8\", \"taker_order_id\": \"132fb6ae-456b-4654-b4e0-d681ac05cea1\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"product_id\": \"BTC-USD\", \"size\": \"5.23512\", \"price\": \"400.23\", \"side\": \"sell\"\r\n}",
                typeof(Match));
        }


        [Test]
        public void Gdax_WebSocket_Parse_Open()
        {
            this.Execute(
                "{ \"type\": \"open\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"product_id\": \"BTC-USD\", \"sequence\": 10, \"order_id\": \"d50ec984-77a8-460a-b958-66f114b0de9b\", \"price\": \"200.2\", \"remaining_size\": \"1.00\", \"side\": \"sell\"\r\n}",
                typeof(Open));
        }

        [Test]
        public void Gdax_WebSocket_Parse_ReceivedLimitOrder()
        {
            this.Execute(
                "{ \"type\": \"received\", \"time\": \"2014-11-07T08:19:27.028459Z\", \"product_id\": \"BTC-USD\", \"sequence\": 10, \"order_id\": \"d50ec984-77a8-460a-b958-66f114b0de9b\", \"size\": \"1.34\", \"price\": \"502.1\", \"side\": \"buy\", \"order_type\": \"limit\"\r\n}",
                typeof(ReceivedLimitOrder));
        }

        [Test]
        public void Gdax_WebSocket_Parse_ReceivedMarketOrder()
        {
            this.Execute(
                "{ \"type\": \"received\", \"time\": \"2014-11-09T08:19:27.028459Z\", \"product_id\": \"BTC-USD\", \"sequence\": 12, \"order_id\": \"dddec984-77a8-460a-b958-66f114b0de9b\", \"funds\": \"3000.234\", \"side\": \"buy\", \"order_type\": \"market\"\r\n}",
                typeof(ReceivedMarketOrder));
        }
        
    }
}
