using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BEx.Exceptions;

namespace BEx.ExchangeEngine.BitStamp
{
    internal class BitStampErrorInterpreter : ExchangeErrorInterpreter
    {
        private static BitStampErrorInterpreter _instance;

        private BitStampErrorInterpreter(IList<ExceptionIdentifier> identifiers)
            : base(identifiers)
        {
        }

        public static BitStampErrorInterpreter GetInterpreter()
        {
            if (_instance == null)
                _instance = new BitStampErrorInterpreter(GetIdentifiers());

            return _instance;
        }

        private static IList<ExceptionIdentifier> GetIdentifiers()
        {
            IList<ExceptionIdentifier> identifiers = new List<ExceptionIdentifier>();

            identifiers.Add(new ExceptionIdentifier(
                                        new Regex("API key not found$"),
                                        typeof(ExchangeAuthorizationException)));

            identifiers.Add(new ExceptionIdentifier(
                                        new Regex("Invalid signature"),
                                        typeof(ExchangeAuthorizationException)));

            identifiers.Add(new ExceptionIdentifier(
                                            new Regex("header was not sent.$"),
                                            typeof(ExchangeAuthorizationException)));

            //You need $1000.0 to open that order. You have only $7.94 available. Check your account balance for details.
            identifiers.Add(new ExceptionIdentifier(
                                        new Regex("Check your account balance for details.$"),
                                        typeof(LimitOrderRejectedException)));

            return identifiers;

        }
    }
}
