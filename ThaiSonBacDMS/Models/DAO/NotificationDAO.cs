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

        public List<Notification> getByRoleID(int roleId)
        {
            return context.Notifications.Where(x => x.Role_ID == roleId && x.User_ID == null).ToList();
        }
        public void changeStatus(int notiID)
        {
            Notification noti = context.Notifications.SingleOrDefault(x => x.Notif_ID == notiID);
            if(noti.Status == 1)
            {
                noti.Status = 0;
            }
            context.SaveChanges();
        }

        public void addNotification(Notification noti)
        {
            context.Notifications.Add(noti);
            context.SaveChanges();
        }
    }
}
