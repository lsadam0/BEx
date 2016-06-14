using System;
using BEx.Exceptions;
using BEx.ExchangeEngine;
using BEx.Exchanges.Gdax.WebSocket.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BEx.Exchanges.Gdax.WebSocket
{
    internal class GdaxParser : IMessageParser
    {
        private const string ChangeIdentifier = "change";
        private const string DoneIdentifier = "done";
        private const string ErrorIdentifier = "error";
        private const string HeartBeatIdentifier = "heartbeat";
        private const string IdentifierProperty = "type";
        private const string LimitProperty = "limit";
        private const string MatchIdentifier = "match";
        private const string NewSizeProperty = "new_size";
        private const string OpenIdentifier = "open";
        private const string OrderTypeProperty = "order_type";
        private const string ReceivedIdentifier = "received";
        private readonly Type _changeFundsType = typeof(ChangeFundsModel);
        private readonly Type _changeSizeType = typeof(ChangeSizeModel);
        private readonly Type _doneType = typeof(DoneModel);
        private readonly Type _errorType = typeof(ErrorModel);
        private readonly Type _heartbeatType = typeof(HeartBeatModel);
        private readonly Type _matchType = typeof(MatchModel);
        private readonly Type _openType = typeof(OpenModel);
        private readonly Type _receivedLimitOrderType = typeof(ReceivedLimitOrderModel);
        private readonly Type _receivedMarketOrderType = typeof(ReceivedMarketOrderModel);

        public object Parse(string message)
        {
            var messageObject = JObject.Parse(message);

            var identifier = ExtractIdentifierToken(messageObject);

            Type deserializationTarget = null;

            switch (identifier)
            {
                case ReceivedIdentifier:
                    deserializationTarget = WhichReceivedType(messageObject);
                    break;

                case OpenIdentifier:
                    deserializationTarget = _openType;
                    break;

                case DoneIdentifier:
                    deserializationTarget = _doneType;
                    break;

                case MatchIdentifier:
                    deserializationTarget = _matchType;
                    break;

                case ChangeIdentifier:
                    deserializationTarget = WhichChangeType(messageObject);
                    break;

                case HeartBeatIdentifier:
                    deserializationTarget = _heartbeatType;
                    break;

                case ErrorIdentifier:
                    deserializationTarget = _errorType;
                    break;

                default:
                    throw new InvalidOperationException(
                        string.Format(
                            ErrorMessages.SocketMessageIdentificationFailed,
                            message));
            }

            return JsonConvert.DeserializeObject(
                message,
                deserializationTarget);
        }

        private string ExtractIdentifierToken(JObject messageObject)
        {
            var token = messageObject[IdentifierProperty];

            if (token is JValue)
            {
                return (token as JValue).Value.ToString();
            }

            return string.Empty;
        }

        private Type WhichChangeType(JObject message)
        {
            if (message[NewSizeProperty] != null)
            {
                return _changeSizeType;
            }
            return _changeFundsType;
        }

        private Type WhichReceivedType(JObject message)
        {
            var orderType = (message[OrderTypeProperty] as JValue).Value.ToString();

            if (orderType == LimitProperty)
            {
                return _receivedLimitOrderType;
            }
            return _receivedMarketOrderType;
        }
    }
}