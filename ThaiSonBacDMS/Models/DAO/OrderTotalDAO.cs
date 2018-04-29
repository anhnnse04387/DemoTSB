using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models.DAO
{
    public class OrderTotalDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public OrderTotalDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public int checkOrder(String id)
        {
            return db.Order_total.Where(x => x.Order_ID.Equals(id)).Count();
        }

        public String createOrder(Order_total order)
        {
            db.Order_total.Add(order);
            if (db.SaveChanges() > 0)
            {
                return order.Order_ID;
            }
            return null;
        }

        public void deleteContent(String orderId)
        {
            foreach (Order_items i in db.Order_items.Where(x => x.Order_ID.Equals(orderId)).ToList())
            {
                db.Order_items.Remove(i);
            }
            foreach (Order_part p in db.Order_part.Where(x => x.Order_ID.Equals(orderId)).ToList())
            {
                db.Order_part.Remove(p);
            }
            db.SaveChanges();
        }

        public void updateOrder(Order_total order)
        {
            var record = db.Order_total.Where(x => x.Order_ID.Equals(order.Order_ID)).SingleOrDefault();
            record.Address_delivery = order.Address_delivery;
            record.Address_invoice_issuance = order.Address_invoice_issuance;
            record.Tax_code = order.Tax_code;
            record.Customer_ID = order.Customer_ID;
            record.Rate = order.Rate;
            record.User_ID = order.User_ID;
            record.Sub_total = order.Sub_total;
            record.VAT = order.VAT;
            record.Total_price = order.Total_price;
            record.Order_discount = order.Order_discount;
            record.Status_ID = order.Status_ID;
            db.SaveChanges();
        }

        public int getNextValueID()
        {
            return db.Order_total.ToList().Count + 1;
        }

        public Order_total getOrder(String orderId)
        {
            return db.Order_total.Where(x => x.Order_ID.Equals(orderId)).SingleOrDefault();
        }

        public void checkOut(String orderId, int userId, int deliveryQtt)
        {
            (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).ToList().ForEach(x => x.Status_ID = 2);
            (from op in db.Order_part where op.Order_ID.Equals(orderId) select op).ToList().ForEach(x => x.Status_ID = 2);
            if (deliveryQtt > 1)
            {
                for (int i = 0; i < deliveryQtt; i++)
                {
                    db.Order_detail_status.Add(new Order_detail_status
                    {
                        Status_ID = 2,
                        User_ID = userId,
                        Date_change = DateTime.Now,
                        Order_ID = orderId,
                        Order_part_ID = deliveryQtt > 1 ? orderId + "-" + (i + 1) : ""
                    });
                }
            }
            db.Order_detail_status.Add(new Order_detail_status
            {
                Status_ID = 2,
                User_ID = userId,
                Date_change = DateTime.Now,
                Order_ID = orderId
            });
            db.SaveChanges();
        }

        public void checkOutOnTime(String orderId, int userId)
        {
            var user = db.Users.Where(x => x.User_ID == userId).SingleOrDefault();
            db.Notifications.Add(new Notification
            {
                Notif_date = DateTime.Now,
                Content = "Nhân viên " + db.Role_detail.Where(x => x.Role_ID == user.Role_ID).SingleOrDefault().Role_name.ToLower()
                + " " + user.User_name + " đã chốt đơn với mã " + orderId,
                Link = "/ChiTietPhieu/Index?" + orderId,
                Role_ID = 4,
                Status = 1
            });
            db.Notifications.Add(new Notification
            {
                Notif_date = DateTime.Now,
                Content = "Nhân viên " + db.Role_detail.Where(x => x.Role_ID == user.Role_ID).SingleOrDefault().Role_name.ToLower()
                + " " + user.User_name + " đã chốt đơn với mã " + orderId,
                Link = "/ChiTietPhieu/Index?" + orderId,
                Role_ID = 5,
                Status = 1
            });
            if (orderId.Contains("-"))
            {
                (from o in db.Order_part where o.Order_part_ID.Equals(orderId) select o).SingleOrDefault().Status_ID = 3;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 3,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault().Status_ID = 3;
                (from o in db.Order_part where o.Order_part_ID.Equals(orderId) select o).SingleOrDefault().Status_ID = 3;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 3,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void keToan_checkOut(String orderId, int userId, decimal? vat, String invoiceNumber)
        {
            byte? status = 4;
            if (db.Order_detail_status.Where(x => (x.Order_part_ID.Equals(orderId) || x.Order_ID.Equals(orderId)) && x.Status_ID == 11).ToList().Count > 0)
            {
                status = 6;
            }
            if (orderId.Contains("-"))
            {
                var part = (from o in db.Order_part where o.Order_part_ID.Equals(orderId) select o).SingleOrDefault();
                part.Status_ID = status;
                part.VAT = vat;
                part.Invoice_number = invoiceNumber;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                var order = (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault();
                order.Status_ID = status;
                order.VAT = vat;
                var part = (from o in db.Order_part where o.Order_part_ID.Equals(orderId) select o).SingleOrDefault();
                part.Status_ID = status;
                part.VAT = vat;
                part.Invoice_number = invoiceNumber;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void kho_checkOut(String orderId, int userId, bool takeInvoice, bool takeBallot)
        {
            var p = db.Order_part.Where(x => x.Order_part_ID.Equals(orderId)).SingleOrDefault();
            byte? status = 5;
            if (db.Order_detail_status.Where(x => (x.Order_part_ID.Equals(orderId) || x.Order_ID.Equals(orderId)) && x.Status_ID == 4).ToList().Count == 0)
            {
                status = 10;
            }
            if (takeInvoice)
            {
                p.Date_take_invoice = DateTime.Now;
            }
            if (takeBallot)
            {
                p.Date_take_ballot = DateTime.Now;
            }
            p.Status_ID = status;
            if (orderId.Contains("-"))
            {
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault().Status_ID = status;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void delivery_checkOut(String orderId, int userId, byte? DeliverMethod_ID, string Driver_ID, int Shiper_ID, bool receiveInvoice, bool receiveBallot)
        {
            var p = db.Order_part.Where(x => x.Order_part_ID.Equals(orderId)).SingleOrDefault();
            byte? status = 6;
            if (db.Order_detail_status.Where(x => (x.Order_part_ID.Equals(orderId) || x.Order_ID.Equals(orderId)) && x.Status_ID == 4).ToList().Count == 0)
            {
                status = 11;
            }
            if (receiveInvoice)
            {
                p.Date_reveice_invoice = DateTime.Now;
            }
            if (receiveBallot)
            {
                p.Date_reveice_ballot = DateTime.Now;
            }
            p.Status_ID = status;
            p.Shiper_ID = Shiper_ID;
            p.DeliverMethod_ID = DeliverMethod_ID;
            p.Driver_ID = Driver_ID;
            if (orderId.Contains("-"))
            {
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault().Status_ID = status;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = status,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void kho_updateOrder(String orderId, bool takeInvoice, bool takeBallot, bool receiveInvoice, bool receiveBallot)
        {
            var p = db.Order_part.Where(x => x.Order_part_ID.Equals(orderId)).SingleOrDefault();
            if (receiveInvoice)
            {
                p.Date_reveice_invoice = DateTime.Now;
            }
            if (receiveBallot)
            {
                p.Date_reveice_ballot = DateTime.Now;
            }
            if (takeInvoice)
            {
                p.Date_take_invoice = DateTime.Now;
            }
            if (takeBallot)
            {
                p.Date_take_ballot = DateTime.Now;
            }
            db.SaveChanges();
        }

        public void cancelOrder(String orderId, String reason, int userId, int deliveryQtt)
        {
            if (orderId.Contains("-"))
            {
                var p = (from op in db.Order_part where op.Order_part_ID.Equals(orderId) select op).SingleOrDefault();
                p.Status_ID = 8;
                p.Note = reason;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 8,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                var user = db.Users.Where(x => x.User_ID == userId).SingleOrDefault();
                var order = (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault();
                order.Status_ID = 8;
                order.Note = reason;
                var part = (from op in db.Order_part where op.Order_ID.Equals(orderId) select op).ToList();
                foreach (Order_part p in part)
                {
                    p.Status_ID = 8;
                    p.Note = reason;
                }
                for (int i = 0; i < deliveryQtt; i++)
                {
                    db.Order_detail_status.Add(new Order_detail_status
                    {
                        Status_ID = 8,
                        User_ID = userId,
                        Date_change = DateTime.Now,
                        Order_ID = orderId,
                        Order_part_ID = deliveryQtt > 1 ? orderId + "-" + (i + 1) : ""
                    });
                }
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 8,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void exchangeOrder(String orderId, String reason, int userId)
        {
            if (orderId.Contains("-"))
            {
                var p = (from op in db.Order_part where op.Order_part_ID.Equals(orderId) select op).SingleOrDefault();
                p.Status_ID = 9;
                p.Note = reason;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 9,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
            }
            else
            {
                var order = (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault();
                order.Status_ID = 9;
                order.Note = reason;
                var p = (from op in db.Order_part where op.Order_part_ID.Equals(orderId) select op).SingleOrDefault();
                p.Status_ID = 9;
                p.Note = reason;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 9,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public void completeOrder(String orderId, int userId)
        {
            if (orderId.Contains("-"))
            {
                db.Notifications.Add(new Notification
                {
                    Notif_date = DateTime.Now,
                    Content = "Nhân viên hàng hóa " + db.Users.Where(x => x.User_ID == userId).SingleOrDefault().User_name + " đã chốt đơn với mã " + orderId,
                    Link = "/ChiTietPhieu/Index?" + orderId,
                    User_ID = db.Order_detail_status.Where(x => x.Order_part_ID.Equals(orderId) && (x.Status_ID == 1 || x.Status_ID == 2)).FirstOrDefault().User_ID,
                    Status = 1
                });
                var p = (from op in db.Order_part where op.Order_part_ID.Equals(orderId) select op).SingleOrDefault();
                p.Status_ID = 7;
                p.Date_completed = DateTime.Now;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 7,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId.Substring(0, orderId.IndexOf("-")),
                    Order_part_ID = orderId
                });
                var count = 0;
                var order = db.Order_total.Where(x => x.Order_ID.Equals(orderId.Substring(0, orderId.IndexOf("-")))).SingleOrDefault();
                foreach (Order_part i in order.Order_part)
                {
                    if(i.Status_ID == 7)
                    {
                        count++;
                    }
                }
                if(count == order.Order_part.Count)
                {
                    order.Status_ID = 7;
                    order.Date_completed = DateTime.Now;
                    db.Order_detail_status.Add(new Order_detail_status
                    {
                        Status_ID = 7,
                        User_ID = userId,
                        Date_change = DateTime.Now,
                        Order_ID = orderId
                    });
                }
            }
            else
            {
                db.Notifications.Add(new Notification
                {
                    Notif_date = DateTime.Now,
                    Content = "Nhân viên hàng hóa " + db.Users.Where(x => x.User_ID == userId).SingleOrDefault().User_name + " đã hoàn thành đơn với mã " + orderId,
                    Link = "/ChiTietPhieu/Index?" + orderId,
                    User_ID = db.Order_detail_status.Where(x => x.Order_ID.Equals(orderId) && x.Status_ID == 1).FirstOrDefault().User_ID,
                    Status = 1
                });
                var order = (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault();
                order.Status_ID = 7;
                order.Date_completed = DateTime.Now;
                var p = (from op in db.Order_part where op.Order_part_ID.Equals(orderId) select op).SingleOrDefault();
                p.Status_ID = 7;
                p.Date_completed = DateTime.Now;
                db.Order_detail_status.Add(new Order_detail_status
                {
                    Status_ID = 7,
                    User_ID = userId,
                    Date_change = DateTime.Now,
                    Order_ID = orderId
                });
            }
            db.SaveChanges();
        }

        public List<Order_total> getLstOrder()
        {
            return db.Order_total.ToList();
        }

        public List<Order_total> getOrderByDateCreated(DateTime dateBegin, DateTime dateEnd)
        {
            return db.Order_total.Where(s => s.Date_created >= dateBegin && s.Date_created <= dateEnd).OrderBy(x => x.Date_created).ToList();
        }

        public List<Order_total> showNewestOrder(int user_id)
        {
            var query = (from od in db.Order_total
                         join cus in db.Customers on od.Customer_ID equals cus.Customer_ID
                         join sta in db.Status on od.Status_ID equals sta.Status_ID
                         join u in db.Users on od.User_ID equals u.User_ID
                         orderby od.Date_created
                         select new
                         {
                             orderID = od.Order_ID
                         }).Take(10);
            List<Order_total> listOrder = new List<Order_total>();
            if (query == null)
            {
                return new List<Order_total>();
            }
            else
            {
                foreach (var item in query)
                {
                    Order_total order = new Order_total();
                    order = new OrderTotalDAO().getOrder(item.orderID);
                    listOrder.Add(order);
                }
                return listOrder;
            }
        }

        public List<int> getListYear()
        {
            List<int> listYear = new List<int>();

            var query = (from ot in db.Order_total
                         select new
                         {
                             date_created = ot.Date_created.Year
                         }).Distinct().OrderBy(d => d);
            if (query != null)
            {
                foreach (var item in query)
                {
                    listYear.Add((int)item.date_created);
                }
            }
            return listYear;
        }
        public List<Order_total> getListByCustomerID(int cusID)
        {
            return db.Order_total.Where(x => x.Customer_ID == cusID).ToList();
        }

        public decimal getDataKhachHangByID(DateTime beginDate, DateTime endDate, int customerID)
        {
            var query = from ot in db.Order_total
                        where ot.Date_created >= beginDate && ot.Date_created <= endDate
                        && ot.Customer_ID == customerID
                        select ot;
            return query.Sum(x => x.Total_price) == null ? 0 : (decimal)query.Sum(x => x.Total_price);
        }

        public List<Order_total> getOrderByCustomerID(DateTime dateBegin, DateTime dateEnd, int customerID)
        {
            return db.Order_total.Where(s => s.Date_created >= dateBegin && s.Date_created <= dateEnd
            && s.Customer_ID == customerID).ToList();
        }

        //thuongtx
        public DateTime? getDate(string orderId)
        {
            return db.Order_total.SingleOrDefault(x => x.Order_ID.Equals(orderId)).Date_completed;
        }
    }
}
