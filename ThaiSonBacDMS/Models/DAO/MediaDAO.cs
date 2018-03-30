using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class MediaDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public MediaDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public int mediaCount()
        {
            return context.Media.Count();
        }
    }
}
