using FundooModel.Notes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace FundooRepository.IRepository
{
    public interface INotesRepository
    {
        public Task<int> AddNotes(Note note);
        public Note EditNotes(Note note);
        public IEnumerable<Note> GetAllNotes(int id);
        public bool DeleteNote(int noteid);
        public IEnumerable<Note> GetArcheived(int id);
        public IEnumerable<Note> GetPinnedNote(int id);
        public IEnumerable<Note> GetThrashedNote(int id);
        public bool TrashNote(int id);
        public Note ArcheiveNote(int noteId, int id);
        public Note PinNote(int noteId, int id);
        public bool RestoreNotes(int noteId, int id);
        public string Image(IFormFile file, int noteId);
        public Note GetNoteById(int userId, int noteId);
    }
}
