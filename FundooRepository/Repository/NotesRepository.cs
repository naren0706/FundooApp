using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace FundooRepo.Repository
{
    public class NotesRepository : INotesRepository
    {
        public readonly UserDbContext context;
        private readonly IDistributedCache distributedCache;
        NlogOperation log = new NlogOperation();
        public NotesRepository(UserDbContext context, IDistributedCache distributedCache)
        {
            this.context = context;
            this.distributedCache = distributedCache;
        }

        public NotesRepository()
        {
        }

        public Task<int> AddNotes(Note note)
        {
            try
            {
                this.context.Notes.Add(note);
                var result = this.context.SaveChangesAsync();
                log.LogInfo("Notes Addeed Successfully");
                return result;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }
        public Note EditNotes(Note note)
        {
            try
            {
                var data = this.context.Notes.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (data != null)
                {
                    data.Id = note.Id;
                    data.Remainder = note.Remainder;
                    data.Title = note.Title;
                    data.Color = note.Color;
                    data.ModifiedDate = note.ModifiedDate;
                    data.Description = note.Description;
                    data.Image = note.Image;
                    data.IsArchive = note.IsArchive;
                    data.IsPin = note.IsPin;
                    data.IsTrash = note.IsTrash;
                    this.context.Notes.Update(data);
                    this.context.SaveChangesAsync();
                    log.LogInfo("Note Updated Sucessfully");
                    return data;
                }
                log.LogWarn("Your Id or Email Id is Incorrect");
                return null;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }
        public IEnumerable<Note> GetAllNotes(int id)
        {
            var result = this.context.Notes.Where(x => x.Id == id).AsEnumerable();
            if (result != null)
            {
                this.PutListToCache(id);
                return result;
            }
            var data = this.GetListFromCache("noteList");
            return null;
            WriteToJson(result.ToList());
        }

        private void WriteToJson(List<Note> notes)
        {
            string fileName = @"E:\BridgeGateProblems\MVC\fundoo4\FundooApp\FundooRepository\Notes.json";
            string result = JsonConvert.SerializeObject(notes, Formatting.Indented);
            File.WriteAllText(fileName, result);
        }

        public Note GetNoteById(int userId, int noteId)
        {
            var data = this.GetListFromCache("noteList");
            if (data != null)
            {
                this.PutListToCache(noteId);
                return data.Where(x => x.Id == userId && x.NoteId == noteId).FirstOrDefault();
            }
            else
            {
                var result = this.context.Notes.Where(x => x.Id == userId && x.NoteId == noteId).FirstOrDefault();
                return result;
            }
        }
        public bool DeleteNote(int noteid)
        {
            try
            {
                var result = this.context.Notes.Where(x => x.NoteId == noteid).FirstOrDefault();
                if (result != null)
                {
                    result.IsTrash = true;
                    this.context.Notes.Update(result);
                    var deleteResult = this.context.SaveChanges();
                    if (deleteResult == 1)
                    {
                        log.LogInfo("Your Note is Added to the Thrash");
                        return true;
                    }
                    log.LogWarn("Your Note is Not Added to the Thrash");
                    return false;
                }
                log.LogWarn("Given Mail id or Note Id is Wrong");
                return false;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return false;
            }
        }
        public IEnumerable<Note> GetThrashedNote(int UserID)
        {
            var data = this.GetListFromCache("noteList");
            if (data != null)
            {
                this.PutListToCache(UserID);
                return data.Where(x => x.Id == UserID && x.IsTrash==true).AsEnumerable();
            }
            else
            {
                var result = this.context.Notes.Where(x => x.Id == UserID && x.IsTrash == true).AsEnumerable();
                return result;
            }
        }
        //delete the notes in trash
        public bool TrashNote(int id)
        {
            try
            {
                var result = this.context.Notes.Where(x => x.Id == id && x.IsTrash == true).ToList();
                foreach (var data in result)
                {
                    this.context.Notes.Remove(data);
                }
                var deleteResult = this.context.SaveChanges();
                if (deleteResult == 0)
                {
                    log.LogInfo("Your Thrashed Note are Deleted Permanetly");
                    return false;
                }
                log.LogWarn("There is No Thrash Note in the Given Mail Id");
                return false;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return false;
            }
        }
        public Note PinNote(int noteId, int id)
        {
            try
            {
                var result = this.context.Notes.Where(x => x.NoteId == noteId && x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.IsPin = true;
                    this.context.Notes.Update(result);
                    this.context.SaveChangesAsync();
                    log.LogInfo("Your Note is Pinned Successfully");
                    return result;
                }
                log.LogWarn("Given Mail Id or Note Id is Incorrect");
                return null;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }
        public IEnumerable<Note> GetArcheived(int UserID)
        {
            var data = this.GetListFromCache("noteList");
            if (data != null)
            {
                this.PutListToCache(UserID);
                return data.Where(x => x.Id == UserID && x.IsArchive == true).AsEnumerable();
            }
            else
            {
                var result = this.context.Notes.Where(x => x.Id == UserID && x.IsArchive == true).AsEnumerable();
                return result;
            }
        }
        public Note ArcheiveNote(int noteId, int id)
        {
            try
            {
                var result = this.context.Notes.Where(x => x.NoteId == noteId && x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.IsArchive = true;
                    this.context.Notes.Update(result);
                    this.context.SaveChangesAsync();
                    log.LogInfo("Your Note is Archeived Successfully");
                    return result;
                }
                log.LogWarn("Given Mail id or Note Id is Incorrect");
                return null;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }
        public IEnumerable<Note> GetPinnedNote(int UserID)
        {
            var data = this.GetListFromCache("noteList");
            if (data != null)
            {
                this.PutListToCache(UserID);
                return data.Where(x => x.Id == UserID && x.IsPin == true).AsEnumerable();
            }
            else
            {
                var result = this.context.Notes.Where(x => x.Id == UserID && x.IsPin == true).AsEnumerable();
                return result;
            }
        }
        public bool RestoreNotes(int noteId, int id)
        {
            try
            {
                var result = this.context.Notes.Where(x => x.NoteId == noteId && x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.IsTrash = false;
                    this.context.Notes.Update(result);
                    this.context.SaveChanges();
                    log.LogInfo("Your Note is Restored Successfully");
                    return true;
                }
                log.LogWarn("Given Mail Id and Note Id is Incorrect");
                return false;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return false;
            }
        }
        public string Image(IFormFile file, int noteId)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account("dzel62mup", "691995444473279", "PFGqItzTvOzXSLeOT8n3VyhENno");
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                var data = this.context.Notes.Where(t => t.NoteId == noteId).FirstOrDefault();
                data.Image = uploadResult.Uri.ToString();
                var result = this.context.SaveChanges();
                return data.Image;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void PutListToCache(int userid)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
            var enlist = this.context.Notes.Where(x => x.Id == userid);
            var jsonstring = JsonConvert.SerializeObject(enlist);
            distributedCache.SetString("noteList", jsonstring, options);
        }
        public List<Note> GetListFromCache(string key)
        {
            var CacheString = this.distributedCache.GetString(key);
            return JsonConvert.DeserializeObject<IEnumerable<Note>>(CacheString).ToList();
        }
    }
}
