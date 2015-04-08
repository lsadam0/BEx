using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BEx.Exceptions;

namespace BEx.ExchangeEngine.Bitfinex
{
    internal class BitfinexErrorInterpreter : ExchangeErrorInterpreter
    {
        private static BitfinexErrorInterpreter _instance;

        private BitfinexErrorInterpreter(IList<ExceptionIdentifier> identifiers)
            : base(identifiers)
        {
        }

        public static BitfinexErrorInterpreter GetInterpreter()
        {
            if (_instance == null)
                _instance = new BitfinexErrorInterpreter(GetIdentifiers());

            return _instance;
        }

        private static IList<ExceptionIdentifier> GetIdentifiers()
        {
            IList<ExceptionIdentifier> identifiers = new List<ExceptionIdentifier>();

            identifiers.Add(new ExceptionIdentifier(
                                            new Regex("header was not sent.$"),
                                            typeof(ExchangeAuthorizationException)));
            //"{\"message\":\"Could not find a key matching the given X-BFX-APIKEY.\"}"

            identifiers.Add(new ExceptionIdentifier(
                                            new Regex("^Could not find a key matching the given X-BFX-APIKEY.$"),
                                            typeof(ExchangeAuthorizationException)));

            //Invalid X-BFX-SIGNATURE.

            identifiers.Add(new ExceptionIdentifier(
                                            new Regex("^Invalid X-BFX-SIGNATURE.$"),
                                            typeof(ExchangeAuthorizationException)));

            //Invalid order: not enough balance

            identifiers.Add(new ExceptionIdentifier(
                                            new Regex("^Invalid order: not enough balance$"),
                                            typeof(LimitOrderRejectedException)));

            return identifiers;

        }
    }
}
