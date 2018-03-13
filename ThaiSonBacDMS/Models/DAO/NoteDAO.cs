using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class NoteDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public NoteDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public Note getNotebyAccount(string account_id)
        {
            return context.Notes.SingleOrDefault(s => s.Account_ID == account_id);
        }
        public void editNotebyAccount(string account_id, string content)
        {
            var rowEdit = (from n in context.Notes
                           where n.Account_ID == account_id
                           select n).SingleOrDefault();
            rowEdit.Contents = content;
            context.SaveChanges();
        }
    }
}
