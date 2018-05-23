using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderTotalModel
    {
        public String spanClass { get; set; }
        public String dateComplete { get; set; }
        public String dateWarehouse { get; set; }
        public String dateExport { get; set; }
        public String dateReceiveBallot { get; set; }
        public String dateReceiveInvoice { get; set; }
        public String orderId { get; set; }
        public String customerName { get; set; }
        public String deliveryAddress { get; set; }
        public String invoiceNumber { get; set; }
        public String invoiceAddress { get; set; }
        public String taxCode { get; set; }
        public String status { get; set; }
        public int count { get; set; }
        public byte? statusId { get; set; }
        public IList<SelectListItem> deliveryMethod { get; set; }
        public byte? DeliverMethod_ID { get; set; }
        public string Driver_ID { get; set; }
        public int? Shiper_ID { get; set; }
        public IList<SelectListItem> shipper { get; set; }
        public IList<SelectListItem> driver { get; set; }
        public String dateTakeInvoice { get; set; }
        public String dateTakeBallot { get; set; }
        public int? rate { get; set; }
        public List<OrderItemModel> readItems { get; set; }

    }
}