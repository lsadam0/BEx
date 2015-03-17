namespace BEx.CommandProcessing
{
    public enum ExchangeParameterType
    {
        Address,
        Post
    }

    public enum StandardParameterType
    {
        None,
        Amount,
        Id,
        Price,
        Base,
        Counter,
        Currency,
        CurrencyFullName,
        Pair,
        UnixTimeStamp,
        TimeStamp
    }

    public class ExchangeParameter
    {
        public ExchangeParameter(ExchangeParameterType paramType,
                                    string name,
                                    StandardParameterType standardType,
                                    string defaultValue = null)
        {
            ParamType = paramType;
            ExchangeParameterName = name;
            DefaultValue = defaultValue;
            StandardParameterIdentifier = standardType;
            IsLowerCase = false;
        }

        public bool IsLowerCase
        {
            get;
            set;
        }

        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// BEx standard parameter name
        /// </summary>
        public StandardParameterType StandardParameterIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Exchange specific parameter name
        /// </summary>
        public string ExchangeParameterName
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