using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FundooRepo.Repository
{
    public class NotesRepository : INotesRepository
    {
        public readonly UserDbContext context;
        NlogOperation nlog = new NlogOperation();
        public NotesRepository(UserDbContext context )
        {
            this.context = context;
        }
        public Task<int> AddNotes(Note note)
        {
            this.context.Notes.Add(note);
            var result = this.context.SaveChangesAsync();
            nlog.LogInfo("Added Note successfully");
            return result;
        }
        public Note EditNotes(Note note)
        {
            var data = this.context.Notes.Where(x => x.Id == note.Id && x.EmailId == note.EmailId).FirstOrDefault();
            if (data != null)
            {
                data.Id = note.Id;
                data.EmailId = note.EmailId;
                data.Remainder = note.Remainder;
                data.Title = note.Title;
                data.Color = note.Color;
                data.ModifiedDate = note.ModifiedDate;
                data.CreatedDate = note.CreatedDate;
                data.Description = note.Description;
                data.Image = note.Image;
                data.IsArchive = note.IsArchive;
                data.IsPin = note.IsPin;
                data.IsTrash = note.IsTrash;
                this.context.Notes.Update(data);
                this.context.SaveChangesAsync();
                nlog.LogInfo("Edited successfully");
                return note;
            }
            nlog.LogWarn("Note id or Note Emial did not found!");
            return null;
        }
        public IEnumerable<Note> GetAllNotes(string email)
        {
            var result = this.context.Notes.Where(x => x.EmailId == email).AsEnumerable();
            if (result != null)
            {
                nlog.LogInfo("Retrived All Notes successfully");
                return result;
            }
            nlog.LogWarn("Email doesn't match");
            return null;
        }
        public bool DeleteNote(int noteid, string email)
        {
            var result = this.context.Notes.Where(x => x.Id == noteid && x.EmailId == email).FirstOrDefault();
            if (result != null)
            {
                result.IsTrash = true;
                this.context.Notes.Update(result);
                var deleteResult = this.context.SaveChanges();
                if (deleteResult == 1)
                {
                    nlog.LogInfo("Added in trash successfully");
                    return true;
                }
                nlog.LogWarn("Adding to trash unsuccessful");
                return false;
            }
            nlog.LogWarn("Empty value sent");
            return false;
        }
        public IEnumerable<Note> GetThrashedTask(string email)
        {
            var result = this.context.Notes.Where(x => x.EmailId == email && x.IsTrash == true).AsEnumerable();
            if (result != null)
            {
                nlog.LogInfo("trash task found");
                return result;
            }
            nlog.LogWarn("Not trash found");
            return null;
        }
        public bool TrashNote(string email)
        {
            var result = this.context.Notes.Where(x => x.EmailId == email && x.IsTrash == true).ToList();
            foreach (var data in result)
            {
                nlog.LogInfo("Task deleted successfully");
                this.context.Notes.Remove(data);
            }
            var deleteResult = this.context.SaveChanges();
            if (deleteResult == 0)
            {
                nlog.LogWarn(" No task to Delete");
                return false;
            }
            nlog.LogWarn(" No task to Delete");
            return false;
        }
        public Note PinNote(int noteId, string email)
        {
            var result = this.context.Notes.Where(x => x.Id == noteId && x.EmailId == email).FirstOrDefault();
            if (result != null)
            {
                result.IsPin = true;
                this.context.Notes.Update(result);
                this.context.SaveChangesAsync();
                nlog.LogInfo("Task pinned successfully");
                return result;
            }
            nlog.LogWarn("No Pinned note found");
            return null;
        }
        public IEnumerable<Note> GetArcheived(string email)
        {
            var result = this.context.Notes.Where(x => x.EmailId == email && x.IsArchive == true).AsEnumerable();
            if (result != null)
            {
                nlog.LogInfo("Get take that achived successfully");
                return result;
            }
            nlog.LogWarn("No achived notes found");
            return null;
        }
        public Note ArcheiveNote(int noteId, string email)
        {
            var result = this.context.Notes.Where(x => x.Id == noteId && x.EmailId == email).FirstOrDefault();
            if (result != null)
            {
                result.IsArchive = true;
                this.context.Notes.Update(result);
                this.context.SaveChangesAsync();
                nlog.LogInfo("Task added archeive successfully");
                return result;
            }
            nlog.LogWarn("Cannot add archeive Notes");
            return null;
        }
        public IEnumerable<Note> GetPinnedTask(string email)
        {
            var result = this.context.Notes.Where(x => x.EmailId == email && x.IsPin == true).AsEnumerable();
            if (result != null)
            {
                nlog.LogInfo("Get all task pinned successfully");
                return result;
            }
            nlog.LogWarn("No pinned task found");
            return null;
        }
        public bool RestoreNotes(int noteId,string email)
        {
            var result = this.context.Notes.Where(x => x.Id == noteId && x.EmailId == email).FirstOrDefault();
            if (result != null)
            {
                result.IsTrash = false;
                this.context.Notes.Update(result);
                var restoreResult = this.context.SaveChanges();
                if (restoreResult != 0)
                {
                    nlog.LogInfo("Restored Notes successful");
                    return true;
                }
            }
            nlog.LogWarn("Restored not found ");
            return false;
        }
    }
}
