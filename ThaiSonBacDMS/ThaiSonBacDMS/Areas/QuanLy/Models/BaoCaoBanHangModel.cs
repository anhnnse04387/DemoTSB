using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class BaoCaoBanHangModel
    {
        public List<RolePerformance> listSalesPerformance { get; set; }
        public List<RolePerformance> listAccountingPerformance { get; set; }
        public List<RolePerformance> listWarhousePerformance { get; set; }
        public int totalOrder { get; set; }
        public int completedOrder { get; set; }
        public int pendingOrder { get; set; }
        public int cancelOrder { get; set; }
        public decimal sumMoney { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string errorStr { get; set; }
    }

    public class RolePerformance
    {
        public RolePerformance(string userName, int numberOrder, double performance)
        {
            this.userName = userName;
            this.performance = performance;
            this.numberOrder = numberOrder;
        }
        public string userName { get; set; }
        public int numberOrder { get; set; }
        public double performance { get; set; }
    }

}