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
        public Note getNotebyAccount(int account_id)
        {
            var note = context.Notes.SingleOrDefault(s => s.Account_ID == account_id);
            if (note == null)
            {
                var newNote = new Note();
                newNote.Account_ID = account_id;
                newNote.Date_Created = DateTime.Now;
                newNote.Contents = string.Empty;
                context.Notes.Add(newNote);
                context.SaveChanges();
                return context.Notes.SingleOrDefault(s => s.Account_ID == account_id);
            }
            else
            {
                return context.Notes.SingleOrDefault(s => s.Account_ID == account_id);
            }
        }
        public void editNotebyAccount(int account_id, string content)
        {
            var rowEdit = (from n in context.Notes
                           where n.Account_ID == account_id
                           select n).SingleOrDefault();
            rowEdit.Contents = content;
            rowEdit.Date_Created = DateTime.Now;
            context.SaveChanges();
        }
    }
}
