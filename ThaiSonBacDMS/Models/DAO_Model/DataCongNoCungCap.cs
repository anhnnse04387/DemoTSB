using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class DataCongNoCungCap
    {
        public DataCongNoCungCap()
        {

        }
        public DataCongNoCungCap(int productID, string productName, 
            string categoryName, int totalQuantity, decimal totalPrice)
        {
            this.productID = productID;
            this.productName = productName;
            this.categoryName = categoryName;
            this.totalQuantity = totalQuantity;
            this.totalPrice = totalPrice;
        }
        public int productID { get; set; }
        public string productName { get; set; }
        public string categoryName { set; get; }
        public int totalQuantity { set; get; }
        public decimal totalPrice { set; get; }
    }
}
