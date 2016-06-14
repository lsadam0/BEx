namespace BEx.Exchanges.Bitfinex.WebSocket.Models
{
    //[1,575.22,0.55,575.26,2.1,-1.44,0,575.22,16056.83183407,581.17,569.97]
    /* [
    "<CHANNEL_ID>",
    "<BID>",
    "<BID_SIZE>",
    "<ASK>",
    "<ASK_SIZE>",
    "<DAILY_CHANGE>",
    "<DAILY_CHANGE_PERC>",
    "<LAST_PRICE>",
    "<VOLUME>",
    "<HIGH>",
    "<LOW>"
 ]*/

    public class TickModel
    {
        public float[] Data { get; set; }
    }
}