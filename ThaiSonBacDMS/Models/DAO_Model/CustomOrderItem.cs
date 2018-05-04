using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class CustomOrderItem
    {
        public int id { get; set; }
        public String orderId { get; set; }
        public int? qtt { get; set; }
        public String date { get; set; }
    }
}
