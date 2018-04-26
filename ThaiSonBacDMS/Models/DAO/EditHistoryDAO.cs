using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class EditHistoryDAO
    {

        private ThaiSonBacDMSDbContext db = null;
        public EditHistoryDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public void createHistory(Edit_history e)
        {
            db.Edit_history.Add(e);
            db.SaveChanges();
        }

        public byte getEditCode(int? productId)
        {
            var history = db.Edit_history.Where(x => x.Product_ID == productId).OrderByDescending(x => x.Date_change).FirstOrDefault();
            if(history == null)
            {
                return 0;
            }
            return history.Edit_code;
        }

    }
}
