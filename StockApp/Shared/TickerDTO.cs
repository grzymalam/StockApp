using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Shared
{
    public class TickerDTO
    {
        public ulong Volume { get; set; }
        public float VolumeAvg { get; set; }
        public float Open { get; set; }
        public float Close { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public long Date { get; set; }
        public int TransactionCount { get; set; }
        
    }
}
