using BEx.ExchangeEngine.Gdax;

namespace BEx
{
    public sealed class Gdax : Exchange
    {
        public Gdax()
            : base(Configuration.Singleton,
                CommandFactory.Singleton)
        {
        }

        public Gdax(string apiKey, string secret, string passphrase)
            : base(Configuration.Singleton,
                CommandFactory.Singleton,
                new Authenticator(apiKey, secret, passphrase))
        {
        }
    }
}