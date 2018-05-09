using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class Sub_CategoryDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public Sub_CategoryDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public List<Sub_category> getAllSubCate()
        {
           return db.Sub_category.Where(x=>x.Status == 1).ToList();
        }
        public List<Autocomplete> getSubCateByCateId(string cateId)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            var query = from subCate in db.Sub_category
                        where subCate.Status == 1
                        select subCate;
            if (!string.IsNullOrEmpty(cateId))
            {
                query = query.Where(x => x.Category_ID.ToString().Equals(cateId));
            }
            if(query.Count() > 0)
            {
                foreach(var item in query)
                {
                    Autocomplete obj = new Autocomplete();
                    obj.key = item.Sub_category_name;
                    obj.strValue = item.Sub_category_ID;
                    lst.Add(obj);
                }
            }
            return lst;
        }

    }
}
