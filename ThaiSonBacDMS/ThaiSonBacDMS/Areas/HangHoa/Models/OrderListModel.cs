using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderListModel
    {
        public string orderID { get; set; }
        public string customerName { get; set; }
        public string invoice_number { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string statusName { get; set; } 
        public List<OrderListPending> listOrderPending { get; set; }
    }
    public class OrderListPending
    {
        public int indexOf { get; set; }
        public string orderID { get; set; }
        public string customerName { get; set; }
        public string invoiceNumber { get; set; }
        public string statusName { get; set; }
        public bool takeInvoice { get; set; }
        public bool takeBallot { get; set; }
        public DateTime dateExport { get; set; }
        public string note { get; set; }
    }
}