using System.Collections.Generic;
using JackLeitch.RateGate;


namespace BEx.CommandProcessing
{
    /// <summary>
    /// Provides rate limiting by exchange type across all instances and threads.
    /// </summary>
    internal class RateLimiter
    {
        private static readonly Dictionary<ExchangeType, RateGate> Gates = new Dictionary<ExchangeType, RateGate>();

        private static readonly object Locker = new object();

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

        public void Throttle()
        {
            Gates[_sourceExchange].WaitToProceed();
        }
    }
}