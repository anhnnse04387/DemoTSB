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
        public Medium getMediaByID(int mediaID)
        {
            return context.Media.SingleOrDefault(x => x.Media_ID == mediaID);
        }
        public int insertMedia(string m_name, string m_location, int upload_by)
        {
            Medium m = new Medium();
            m.Media_name = m_name;
            m.Location = m_location;
            m.Upload_by = upload_by;
            m.Date_upload = DateTime.Now;
            context.Media.Add(m);
            context.SaveChanges();
            return m.Media_ID;
        }

        public int insertProductMedia(int productID, int mediaID)
        {
            try
            {
                Product_media pm = new Product_media();
                pm.Product_ID = productID;
                pm.Media_ID = mediaID;
                context.Product_media.Add(pm);
                context.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return 0;
            } 
        }
        public List<int?> getMediaId(int productId)
        {
            List<int?> lstProductId = new List<int?>();
            var query = from me in context.Product_media
                        where me.Product_ID == productId
                        select me;
            if(query.Count() != 0)
            {
                foreach(var item in query)
                {
                    lstProductId.Add(item.Media_ID);
                }
            }
            return lstProductId;
        }
        public string getProductMedia(int mediaId)
        {
            return context.Media.SingleOrDefault(x => x.Media_ID == mediaId).Location;

        }
    }
}
