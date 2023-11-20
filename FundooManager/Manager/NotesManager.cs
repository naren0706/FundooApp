using FundooManager.IManager;
using FundooModel.Notes;
using FundooRepository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FundooManager.Manager
{
    public class NotesManager : INotesManger
    {
        public readonly INotesRepository NotesRepository;
        public NotesManager(INotesRepository NotesRepository)
        {
            this.NotesRepository = NotesRepository;
        }
        public Task<int> AddNotes(Note note)
        {
            var result = this.NotesRepository.AddNotes(note);
            return result;
        }
        public Note EditNotes(Note note)
        {
            var result = this.NotesRepository.EditNotes(note);
            return result;
        }
        public IEnumerable<Note> GetAllNotes(int UserId)
        {
            var result = this.NotesRepository.GetAllNotes(UserId);
            return result;
        }
        public bool DeleteNote(int noteid, int UserId)
        {
            var result = this.NotesRepository.DeleteNote(noteid);
            return result;
        }
        public IEnumerable<Note> GetArcheived(int UserId)
        {
            var result = this.NotesRepository.GetArcheived(UserId);
            return result;
        }
        public IEnumerable<Note> GetPinnedTask(int UserId)
        {
            var result = this.NotesRepository.GetPinnedNote(UserId);
            return result;
        }
        public IEnumerable<Note> GetThrashedTask(int UserId)
        {
            var result = this.NotesRepository.GetThrashedNote(UserId);
            return result;
        }
        public bool TrashNote(int UserId)
        {
            var result = this.NotesRepository.TrashNote(UserId);
            return result;
        }
        public Note ArcheiveNote(int noteId, int UserId)
        {
            var result = this.NotesRepository.ArcheiveNote(noteId, UserId);
            return result;
        }
        public Note PinNote(int noteId, int UserId)
        {
            var result = this.NotesRepository.PinNote(noteId, UserId);
            return result;
        }
    
        public bool RestoreNotes(int noteId, int UserId)
        {
            var result = this.NotesRepository.RestoreNotes(noteId, UserId);
            return result;
        }

        public string Image(IFormFile file, int noteId)
        {
            var result = this.NotesRepository.Image(file, noteId);
            return result;
        }

        public Note GetNoteById(int userId, int noteId)
        {
            var result = this.NotesRepository.GetNoteById(userId, noteId);
            return result;
        }
    }
}