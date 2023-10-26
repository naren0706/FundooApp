using FundooManager.IManager;
using FundooModel.Notes;
using FundooRepository.IRepository;
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
        public IEnumerable<Note> GetAllNotes(string email)
        {
            var result = this.NotesRepository.GetAllNotes(email);
            return result;
        }
        public bool DeleteNote(int noteid, string email)
        {
            var result = this.NotesRepository.DeleteNote(noteid, email);
            return result;
        }
        public IEnumerable<Note> GetArcheived(string email)
        {
            var result = this.NotesRepository.GetArcheived(email);
            return result;
        }
        public IEnumerable<Note> GetPinnedTask(string email)
        {
            var result = this.NotesRepository.GetPinnedTask(email);
            return result;
        }
        public IEnumerable<Note> GetThrashedTask(string email)
        {
            var result = this.NotesRepository.GetThrashedTask(email);
            return result;
        }
        public bool TrashNote(string email)
        {
            var result = this.NotesRepository.TrashNote(email);
            return result;
        }
        public Note ArcheiveNote(int noteId, string email)
        {
            var result = this.NotesRepository.ArcheiveNote(noteId, email);
            return result;
        }
        public Note PinNote(int noteId, string email)
        {
            var result = this.NotesRepository.PinNote(noteId, email);
            return result;
        }
    
        public bool RestoreNotes(int noteId, string email)
        {
            var result = this.NotesRepository.RestoreNotes(noteId, email);
            return result;
        }
    }
}