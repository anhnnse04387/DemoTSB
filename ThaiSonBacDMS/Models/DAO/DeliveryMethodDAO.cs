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

    }
}
