using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OfficeDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public OfficeDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<Office> getListOffice()
        {
            return db.Offices.ToList();
        }
        public List<Autocomplete> getListBySelectedValue(string selectedValue)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            var query = from office in db.Offices
                        where office.Role_ID.ToString().Equals(selectedValue)
                        select new
                        {
                            office
                        };
            if (query.Count() != 0)
            {
                foreach(var item in query)
                {
                    Autocomplete au = new Autocomplete();
                    au.key = item.office.Office_name;
                    au.value = item.office.Office_ID;
                    lst.Add(au);
                }
            }
            return lst;
        }
    }
   
}
