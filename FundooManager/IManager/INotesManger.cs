using FundooModel.Notes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace FundooManager.IManager
{
    public interface INotesManger
    {
        public Task<int> AddNotes(Note note);
        public Note EditNotes(Note note);
        public IEnumerable<Note> GetAllNotes(string email);
        public bool DeleteNote(int noteid, string email);
        public IEnumerable<Note> GetArcheived(string email);
        public IEnumerable<Note> GetPinnedTask(string email);
        public IEnumerable<Note> GetThrashedTask(string email);
        public bool TrashNote(string email);
        public Note ArcheiveNote(int noteId, string email);
        public Note PinNote(int noteId, string email);
        public bool RestoreNotes(int noteId, string email);
    }
}
