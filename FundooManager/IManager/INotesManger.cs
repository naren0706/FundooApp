using FundooModel.Notes;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<Note> GetAllNotes(int UserId);
        public bool DeleteNote(int noteid, int UserId);
        public IEnumerable<Note> GetArcheived(int UserId);
        public IEnumerable<Note> GetPinnedTask(int UserId);
        public IEnumerable<Note> GetThrashedTask(int UserId);
        public bool TrashNote(int UserId);
        public Note ArcheiveNote(int noteId, int UserId);
        public Note PinNote(int noteId, int UserId);
        public bool RestoreNotes(int noteId, int UserId);
        public string Image(IFormFile file, int noteId);
        public Note GetNoteById(int userId, int noteId);

    }
}
