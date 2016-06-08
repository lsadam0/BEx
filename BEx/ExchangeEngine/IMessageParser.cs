using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace BEx.ExchangeEngine
{
    internal interface IMessageParser
    {
        void Parse(string message);
    }

    internal class GdaxParser : IMessageParser
    {
        private int _sequence = 0;
        private const string _identifierProperty = "type";
        private const string _tickIdentifier = "done";

        private void CheckSequence(JObject messageObject)
        {
            lock (this)
            {
                var sequence = messageObject["sequence"];

                if (sequence == null)
                {
                    Trace.TraceError("Unable to determine message sequence");
                }
                else if (sequence is JValue)
                {
                    int tempSequence = -1;

                    if (Int32.TryParse((sequence as JValue).Value.ToString(), out tempSequence))
                    {
                        if (_sequence > 0)
                        {
                            if (tempSequence <= _sequence)
                            {
                                Trace.TraceError("Message Sequence Out Of Order - Last: {0} - Received: {1}", _sequence,
                                    tempSequence);
                            }
                            else if (Math.Abs(_sequence - tempSequence) > 1)
                            {
                                int gap = Math.Abs(_sequence - tempSequence);

                                Trace.TraceError(
                                    "Message Sequence Gap Larger than 1 - Gap Size: {0} - Last: {1} - Received: {2}",
                                    gap, _sequence, tempSequence);
                            }
                        }

                        _sequence = tempSequence;
                    }
                    else
                    {
                        Trace.TraceError("Message Sequence Parse Failed - Received: {0}",
                            (sequence as JValue).Value.ToString());
                    }

                }
            }
        }

        public void Parse(string message)
        {
            JObject messageObject = JObject.Parse(message);

            CheckSequence(messageObject);

            var identifier = ExtractIdentifierToken(messageObject);

            if (identifier == _tickIdentifier)
            {
                CreateTick(messageObject);
            }
        }

        private void CreateTick(JObject messageObject)
        {
            // {"type":"done","order_type":"limit","side":"sell","sequence":1106535249,"order_id":"a60365e5-b0ce-4819-932c-094ff3b9958b","reason":"canceled","product_id":"BTC-USD","time":"2016-06-08T00:38:21.649122Z","price":"580.58","remaining_size":"1.81"}

            var reason = messageObject["reason"];

            if (reason is JValue)
            {
                if (
                    (reason as JValue).Value.ToString() == "filled")
                {
                    /*Snapshot information about the last trade (tick), best bid/ask and 24h volume.*/
                    /*
                     {
                         "type": "done",
                         "order_type": "limit",
                         "side": "sell",
                         "sequence": 1106594706,
                         "order_id": "3812dde7-c818-414a-975e-fcc45e1135ce",
                         "reason": "filled",
                         "product_id": "BTC-USD",
                         "time": "2016-06-08T01:02:02.289479Z",
                         "price": "579.73",
                         "remaining_size": "0"
                       }
                     */
                }
            }
        }

        private string ExtractIdentifierToken(JObject messageObject)
        {
            var token = messageObject[_identifierProperty];

            if (token is JValue)
            {
                return (token as JValue).Value.ToString();
            }

            return string.Empty;
        }
    }
}