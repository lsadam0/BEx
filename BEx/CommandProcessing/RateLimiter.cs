using JackLeitch.RateGate;
using System.Collections.Generic;

namespace BEx.CommandProcessing
{
    /// <summary>
    /// Provides rate limiting by exchange type across all instances and threads.
    /// </summary>
    internal class RateLimiter
    {
        private static Dictionary<ExchangeType, RateGate> gates = new Dictionary<ExchangeType, RateGate>();
        private static object locker = new object();

        private ExchangeType _sourceExchange;

        public RateLimiter(ExchangeType sourceExchange)
        {
            if (!gates.ContainsKey(sourceExchange))
            {
                lock (locker)
                {
                    if (!gates.ContainsKey(sourceExchange))
                    {
                        gates.Add(sourceExchange, new RateGate(600, new System.TimeSpan(0, 10, 0)));
                    }
                }
            }

            _sourceExchange = sourceExchange;
        }

        public void Throttle()
        {
            gates[_sourceExchange].WaitToProceed();
        }
    }
}