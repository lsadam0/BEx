using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BEx;
using Newtonsoft.Json;

namespace BEx.UnitTests
{
    [TestFixture]
    public class Scratch
    {

        string targetJson = "{\"request\": \"/v1/order/new\",\"nonce\": \"635641370147070282\",\"symbol\": \"BTCUSD\",\"amount\": \"0.01\",\"price\": \"1000\",\"exchange\": \"bitfinex\",\"type\": \"exchange limit\",\"side\": \"sell\",\"X-BFX-APIKEY\": \"ieQecmvNQKC7wFcFnVtaVFIRhst3BKiMp31XMBaSS4n\"}";

  
        internal class CancelOrderPayload
        {
            [JsonProperty("order_id")]
            public int OrderId
            {
                get;
                set;
            }
        }

        [Test]
        public void ScratchPad()
        {

      

            AuthToken token = ExchangeFactory.GetBitfinexAuthToken();
            Bitfinex bfx = new Bitfinex(token.ApiKey, token.Secret);

            
            //  var balance = bfx.GetAccountBalance();

            //var currentTick = bfx.GetTick();

            var open = bfx.GetOpenOrders();

            var sellOrder = open.SellOrders.Values.First();

           var res = bfx.CancelOrder(sellOrder);

            /*
           var availableUsd = balance.BalanceByCurrency[Currency.USD].AvailableToTrade;
           var availableBtc = balance.BalanceByCurrency[Currency.BTC].AvailableToTrade;


           var buyOrder = bfx.CreateBuyLimitOrder(1, 205);

           var open = bfx.GetOpenOrders();
            */

            /*
           if (availableBtc > .01m)
           {
               var sellOrder = bfx.CreateSellLimitOrder(.01m, 1000);

               { }


               var openOrders = bfx.GetOpenOrders();

               Order toCancel = openOrders.SellOrders.Values.First();

               /*
                   var confirm = bfx.CancelOrder(toCancel);

                   confirm = bfx.CancelOrder(toCancel);
                   // bfx.CancelOrder(sellOrder);

                   // openOrders = bfx.GetOpenOrders();
               }
               */
           
        }

      
    }
}
