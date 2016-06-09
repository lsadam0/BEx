using System;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using BEx.ExchangeEngine.Gdax.WebSocket.JSON;

namespace BEx.ExchangeEngine.Gdax.WebSocket
{
    internal class GdaxParser : IMessageParser
    {
        private const string _identifierProperty = "type";
        private const string _tickIdentifier = "done";
        private int _sequence = 0;
       // private readonly IList<MessageIdentifier> _identifiers;

        private IList<MessageIdentifier> GetIdentifiers()
        {
            return null;
        }

        public Type Identify(string message)
        {
            return message.GetType();
        }

        public void Parse(string message)
        {
            JObject messageObject = JObject.Parse(message);

            CheckSequence(messageObject);

            var identifier = ExtractIdentifierToken(messageObject);

            if (identifier == _tickIdentifier)
            {
                //  CreateTick(messageObject);
            }
        }

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

    class MessageIdentifier
    {
        public MessageIdentifier(Type deserializationType, Func<JObject, bool> matchDelegate)
        {
            this.DeserializationType = deserializationType;
            this.IsMatch = matchDelegate;
        }

        public Type DeserializationType
        {
            get;
        }

        public Func<JObject, bool> IsMatch
        { get; }
    }
}
