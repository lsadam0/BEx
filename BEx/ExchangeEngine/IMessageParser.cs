namespace BEx.ExchangeEngine
{
    internal interface IMessageParser
    {
        object Parse(string message);
    }
}