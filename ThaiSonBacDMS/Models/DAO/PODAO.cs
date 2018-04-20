using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class PODAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public PODAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public int createPO(PO po)
        {
            db.POes.Add(po);
            if (db.SaveChanges() > 0)
            {
                return po.PO_ID;
            }
            return 0;
        }

        public List<PO> getLstPO()
        {
            return db.POes.ToList();
        }

        public PO getPO(int id)
        {
            return db.POes.Where(x => x.PO_ID == id).SingleOrDefault();
        }

    }
}
