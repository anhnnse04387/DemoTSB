using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderDeliveryModel
    {
        public String dateReceiveBallot { get; set; }
        public String dateReceiveInvoice { get; set; }
        public String orderId { get; set; }
        public String customerName { get; set; }
        public String deliveryAddress { get; set; }
        public String invoiceNumber { get; set; }
        public String invoiceAddress { get; set; }
        public String taxCode { get; set; }
        public String status { get; set; }
        public IList<SelectListItem> deliveryMethod { get; set; }
        public byte? DeliverMethod_ID { get; set; }
        public string Driver_ID { get; set; }
        public string Shiper_ID { get; set; }
        public IList<SelectListItem> shipper { get; set; }
        public IList<SelectListItem> driver { get; set; }

    }
}