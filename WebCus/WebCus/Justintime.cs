using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCus
{
    public class Justintime
    {
        public string BLType { get; set; }
        public string BLNo { get; set; }
        public string Pieces { get; set; }
        public string GW { get; set; }
        public string Conts { get; set; }
        public string Cbm { get; set; }

        public histList[] Histories { get; set; }
    }
    public class ListHistories
    {
        public IEnumerable<histList> data { get; set; }
    }
    public class histList
    {
        public string BlStatus { set; get; }
        public string FlighNo { set; get; }
        public string Conts { set; get; }
        public string Gw { set; get; }
        public string Pieces { get; set; }
        public string Measurement { set; get; }
        public string Airport { set; get; }
        public string TrackDate { set; get; }
        public string Actual { set; get; }
        public string Vessel { set; get; }
        public string Voyage { set; get; }
        public string Location { set; get; }
    }
}