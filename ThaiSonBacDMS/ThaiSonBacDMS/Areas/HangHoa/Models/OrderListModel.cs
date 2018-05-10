using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderListModel
    {        
        public string orderID { get; set; }
        public string customerName { get; set; }
        public string invoice_number { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string fromCompletedDate { get; set; }
        public string toCompletedDate { get; set; }
        public string statusName { get; set; }
        public string deliverMethod { get; set; }
        public string driverName { get; set; }
        public bool takeInvoice { get; set; }
        public bool takeBallot { get; set; }
        public byte statusId { get; set; }
        public string shipperName { get; set; }
        public bool reveiceBallot { get; set; }
        public bool reveiceInvoice { get; set; }
        public byte? DeliverMethod_ID { get; set; }
        public string Driver_ID { get; set; }
        public int? Shiper_ID { get; set; }
        public IList<SelectListItem> listDriver { get; set; }
        public IList<SelectListItem> listShipper { get; set; }
        public IList<SelectListItem> listMethod { get; set; }
        public List<OrderListPending> listOrderPending { get; set; }
    }
    public class OrderListPending
    {
        public String spanClass { get; set; }
        public int indexOf { get; set; }
        public string orderID { get; set; }
        public string customerName { get; set; }
        public string salesUserName { get; set; }
        public string invoiceNumber { get; set; }
        public string statusName { get; set; }
        public bool takeInvoice { get; set; }
        public bool takeBallot { get; set; }
        public bool reveiceInvoice { get; set; }
        public string devilerMethod { get; set; }
        public string shipperName { get; set; }
        public string driverName { get; set; }
        public bool reveiceBallot { get; set; }
        public DateTime dateExport { get; set; }
        public DateTime? dateCompleted { get; set; }
        public string note { get; set; }
        public byte? DeliverMethod_ID { get; set; }
        public string Driver_ID { get; set; }
        public int? Shiper_ID { get; set; }
    }
}