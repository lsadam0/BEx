// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine
{
    public class ExchangeParameter
    {
        public ExchangeParameter(
            ParameterMethod parameterMethod,
            string name,
            StandardParameter standard)
        {
            ParameterMethod = parameterMethod;
            ExchangeParameterName = name;
            DefaultValue = null;
            StandardParameterIdentifier = standard;
            IsLowercase = false;
        }

        public ExchangeParameter(
            ParameterMethod parameterMethod,
            string name,
            StandardParameter standard,
            string defaultValue)
        {
            ParameterMethod = parameterMethod;
            ExchangeParameterName = name;
            DefaultValue = defaultValue;
            StandardParameterIdentifier = standard;
            IsLowercase = false;
        }

        /// <summary>
        ///     If no value is explicitly supplied, this value will be delivered to the exchange
        /// </summary>
        public string DefaultValue { get; private set; }

        /// <summary>
        ///     Exchange specific parameter name
        /// </summary>
        public string ExchangeParameterName { get; private set; }

        /// <summary>
        ///     If true, the value of the parameter will be converted to lowercase before delivery
        /// </summary>
        public bool IsLowercase { get; internal set; }

        /// <summary>
        ///     Parameter Delivery Method
        /// </summary>
        public ParameterMethod ParameterMethod { get; private set; }

        /// <summary>
        ///     BEx Common Parameter Identifier
        /// </summary>
        public StandardParameter StandardParameterIdentifier { get; private set; }
    }
}