using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx.Request
{
    public enum ExchangeParameterType
    {
        Address,
        Post
    }

    public class ExchangeParameter
    {
        public ExchangeParameter(ExchangeParameterType paramType, string name, string defaultValue = null)
        {
            ParamType = paramType;
            Name = name;
            DefaultValue = defaultValue;
        }

        public string DefaultValue
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ExchangeParameterType ParamType
        {
            get;
            set;
        }
    }
}