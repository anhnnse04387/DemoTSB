using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class POItemDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public POItemDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public int createPOItem(PO_Items items)
        {
            db.PO_Items.Add(items);
            if (db.SaveChanges() > 0)
            {
                return items.ID;
            }
            return 0;
        }

    }
}
