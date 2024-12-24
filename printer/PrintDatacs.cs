using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace printer
{
    public class PrintData
    {

        public string PartNumber {  get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string CustomerCode { get; set; }
        public string QuantityBox { get; set; }
        public string PoNo { get; set; }
        public string Inspector { get; set; }
        public string ProductionDate { get; set; }
        public string AccountNumber { get; set; }
        public string Product { get; set; }

        public string QRcode { get; set; }

        public List<string> ListVincode { get; set; }


    }


}
