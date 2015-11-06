// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using JackLeitch.RateGate;
using System.Collections.Generic;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Provides rate limiting by exchange type across all instances and threads.
    /// </summary>
    internal class RateLimiter
    {
        /// <summary>
        /// Collection of RateGate objects, indexed by exchange, to be shared across
        /// all instances of all Exchange types
        /// </summary>
        private static readonly Dictionary<ExchangeType, RateGate> Gates = new Dictionary<ExchangeType, RateGate>();

        /// <summary>
        /// Thread Sync
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// Exchange requesting throttling
        /// </summary>
        private readonly ExchangeType _sourceExchange;

        public RateLimiter(ExchangeType sourceExchange)
        {
            if (!Gates.ContainsKey(sourceExchange))
            {
                lock (Locker)
                {
                    if (!Gates.ContainsKey(sourceExchange))
                    {
                        Gates.Add(sourceExchange, new RateGate(600, new System.TimeSpan(0, 10, 0)));
                    }
                }
            }

            _sourceExchange = sourceExchange;
        }

        /// <summary>
        /// Retrieve and invoke the shared RateGate for this Exchange
        /// </summary>
        public void Throttle()
        {
            Gates[_sourceExchange].WaitToProceed();
        }
    }
}