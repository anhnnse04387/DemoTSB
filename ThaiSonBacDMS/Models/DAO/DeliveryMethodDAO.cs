using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class DeliveryMethodDAO
    {

        private ThaiSonBacDMSDbContext context = null;
        public DeliveryMethodDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }

        public List<Delivery_Method> getLstDelivery()
        {
            return context.Delivery_Method.ToList();
        }

        public Delivery_Method getByID(int deliverID)
        {
            return context.Delivery_Method.SingleOrDefault(x => x.Method_ID == deliverID);
        } 

    }
}
