using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine;
using RestSharp;

namespace BEx.UnitTests.MockTests.MockObjects
{
    internal class MockRequestDispatcher : IRequestDispatcher
    {

        //     IRestResponse Dispatch(IRestRequest request, IExchangeCommand referenceCommand);
        public IRestResponse Dispatch(IRestRequest request, IExchangeCommand referenceCommand)
        {
            return null;
        }
    }
}
