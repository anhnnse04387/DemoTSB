using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class BaoCaoBanHangController : QuanLyBaseController
    {
        public ActionResult Index(BaoCaoBanHangModel model)
        {
            DateTime beginDate = new DateTime(2017, 1, 1);
            DateTime endDate = new DateTime(2017, 2, 1);
            model.errorStr = string.Empty;
            if (string.IsNullOrEmpty(model.beginDate) && string.IsNullOrEmpty(model.beginDate))
            {
                beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = beginDate.AddMonths(1).AddDays(-1);
                model.beginDate = beginDate.ToString("dd/MM/yyyy");
                model.endDate = endDate.ToString("dd/MM/yyyy");
            }
            else
            {
                try
                {
                    beginDate = DateTime.ParseExact(model.beginDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    endDate = DateTime.ParseExact(model.endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch(Exception e)
                {
                    model.errorStr = "Chọn đủ định dạng ngày tháng";
                    System.Diagnostics.Debug.WriteLine(e);
                    return View(model);
                }
            }
            

            //Overview
            var lstOrder = new OrderTotalDAO().getOrderByDateCreated(beginDate, endDate);
            model.totalOrder = lstOrder.Count;
            model.completedOrder = lstOrder.Where(z => z.Status_ID == 7).Count();
            model.pendingOrder = lstOrder.Where(z => z.Status_ID != 7 && z.Status_ID != 8 && z.Status_ID != 9).Count();
            model.cancelOrder = lstOrder.Where(z => z.Status_ID == 8 || z.Status_ID == 9).Count();
            model.sumMoney = (decimal) lstOrder.Sum(z => z.Total_price);

            //get list User Phan Phoi
            List<User> lstSalesUser = new UserDAO().getAllUserByRoleID(3);
            model.listSalesPerformance = new List<RolePerformance>();
            foreach (var item in lstSalesUser)
            {
                var lstOrderByUserID = lstOrder.Where(z => z.User_ID == item.User_ID);
                double sumPrice = lstOrderByUserID.Count()==0 ? 0 : (double) lstOrderByUserID.Sum(z => z.Total_price);
                model.listSalesPerformance.Add(new RolePerformance(item.User_name, lstOrderByUserID.Count(), sumPrice));
            }
            //get list User Ke Toan
            List<User> lstAccUser = new UserDAO().getAllUserByRoleID(5);
            model.listAccountingPerformance = setListRolePerformance(lstAccUser, beginDate, endDate);
            //get list User Hang Hoa
            List<User> lstWarHouseUser = new UserDAO().getAllUserByRoleID(4);
            model.listWarhousePerformance = setListRolePerformance(lstWarHouseUser, beginDate, endDate);

            return View(model);
        }

        private List<RolePerformance> setListRolePerformance(List<User> listUser, DateTime beginDate, DateTime endDate)
        {
            List<RolePerformance> retunrList = new List<RolePerformance>();
            foreach (var item in listUser)
            {
                List<Order_detail_status> lstPerformance = new OrderDetailStatusDAO().
                getStatusByDateAndUserID(beginDate, endDate, item.User_ID);
                var q = lstPerformance.Where(z => z.Status_ID == 3 || z.Status_ID == 4).OrderBy(z => z.Date_change)
                    .GroupBy(z => z.Order_part_ID).Select(z => new {
                        orderName = z.Key,
                        differentTime = z.Count() != 2 ? 0 : (z.Last().Date_change - z.First().Date_change).Value.TotalHours,
                    });
                var totalTime = q.Where(z => z.differentTime != 0).Sum(z => z.differentTime);
                double perform = q.Count() == 0 ? 0 : totalTime / q.Count();
                retunrList.Add(new RolePerformance(item.User_name,q.Count(),perform));
            }
            return retunrList;
        }
    }
}