
using Newtonsoft.Json;


namespace BEx.BitStampSupport
{

    public class BitStampErrorJSON 
    {

        [JsonProperty("error")]
        public string Error { get; set; }

        
        public APIError ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            APIError error = new APIError();

            error.Message = Error;

            return error;
        }
    }

}
