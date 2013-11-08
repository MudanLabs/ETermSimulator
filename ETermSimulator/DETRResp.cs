using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETermSimulator
{
    public class DETRResp:BaseResp
    {
        public IEnumerator<AirlineTicketData> AirlineTicketDatas { get; set; }
    }
    public class AirlineTicketData
    {
        public string TicketCode { get; set; }
        public string PassengerName { get; set; }
        public string PassengerIDCard { get; set; }
        public decimal Fare { get; set; }
        public decimal TaxAirport { get; set; }
        public decimal TaxFuel { get; set; }
        public string ReceiptNo { get; set; }
        public string PnrCode { get; set; }
        public AirticketFlightSailData AirticketFlightSailData { get; set; }
    }
    public class AirticketFlightSailData
    {
        public string FlightCode { get; set; }
        public DateTime DepDate { get; set; }
        public string DepAirportCode { get; set; }
        public string ArrAirportCode { get; set; }
        public string Cabin { get; set; }
        public string FlightSailState { get; set; }
    }
}
