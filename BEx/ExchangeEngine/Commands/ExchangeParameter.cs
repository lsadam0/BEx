namespace BEx.ExchangeEngine
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
        UnixTimestamp,
        Timestamp
    }

    public class ExchangeParameter
    {
        public ExchangeParameter(
                    ExchangeParameterType parameterType,
                    string name,
                    StandardParameterType standardType)
        {
            ParameterType = parameterType;
            ExchangeParameterName = name;
            DefaultValue = null;
            StandardParameterIdentifier = standardType;
            IsLowercase = false;
        }

        public ExchangeParameter(
                    ExchangeParameterType parameterType,
                    string name,
                    StandardParameterType standardType,
                    string defaultValue)
        {
            ParameterType = parameterType;
            ExchangeParameterName = name;
            DefaultValue = defaultValue;
            StandardParameterIdentifier = standardType;
            IsLowercase = false;
        }

        public string DefaultValue
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

        public bool IsLowercase
        {
            get;
            set;
        }

        public ExchangeParameterType ParameterType
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
    }
}