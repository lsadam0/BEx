// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using BEx.ExchangeEngine.API;
namespace BEx
{
    public sealed class Confirmation : BExResult
    {
        internal Confirmation(DateTime exchangeTimestamp, ExchangeType sourceExchange)
            : base(exchangeTimestamp, sourceExchange)
        {
        }

        public bool IsConfirmed { get; internal set; }
    }
}