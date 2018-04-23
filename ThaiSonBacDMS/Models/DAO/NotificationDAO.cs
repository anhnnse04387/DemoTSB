using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class NotificationDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public NotificationDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public List<Notification> getByUserID(int user_id)
        {
            return context.Notifications.Where(x => x.User_ID == user_id).ToList();
        }
    }
}
