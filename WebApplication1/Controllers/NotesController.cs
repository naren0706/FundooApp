using FundooManager.IManager;
using FundooModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FundooModel.Notes;    
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        public readonly INotesManger NoteManager;
        public NotesController(INotesManger NoteManager)
        {
            this.NoteManager = NoteManager;
        }
        [HttpPost]
        [Route("Add New Notes")]
        public async Task<ActionResult> AddNotes(Note note)
        {
            try
            {
                var result = await this.NoteManager.AddNotes(note);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Adding Notes Successful", data = note });
                }
                return this.BadRequest(new { Status = false, Message = "Notes empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Edit Notes")]
        public ActionResult EditNotes(Note note)
        {
            try
            {
                var result = this.NoteManager.EditNotes(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Edit Task Successful", data = note });
                }
                return this.BadRequest(new { Status = false, Message = "Notes found empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Delete Notes")]
        public ActionResult DeleteNote(int noteid, int UserId)
        {
            try
            {
                var result = this.NoteManager.DeleteNote(noteid, UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Moved to Trash" });
                }
                return this.BadRequest(new { Status = false, Message = "Data empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get All Notes")]
        public async Task<ActionResult> GetAllNotes(int UserId)
        {
            try
            {
                var result = this.NoteManager.GetAllNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Notes Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get Notes by Id")]
        public async Task<ActionResult> GetNoteById(int userId, int noteId)
        {
            try
            {
                var result = this.NoteManager.GetNoteById(userId, noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Notes Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get All Archeived Notes")]
        public async Task<ActionResult> GetArcheived(int UserId)
        {
            try
            {
                var result = this.NoteManager.GetArcheived(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Added to archeived", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "UserId data found empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get All Pinned Notes")]
        public async Task<ActionResult> GetPinnedTask(int UserId)
        {
            try
            {
                var result = this.NoteManager.GetPinnedTask(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Pinned successfully", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "UserId not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get All Thrashed Notes")]
        public async Task<ActionResult> GetThrashedTask(int UserId)
        {
            try
            {
                var result = this.NoteManager.GetThrashedTask(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Null data Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("Permanent Delete")]
        public async Task<ActionResult> TrashNote(int UserId)
        {
            try
            {
                var result = this.NoteManager.TrashNote(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Found" });
                }
                return this.BadRequest(new { Status = false, Message = "No Notes Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Add to Notes Archeive")]
        public ActionResult ArcheiveNote(int noteid, int UserId)
        {
            try
            {
                var result = this.NoteManager.ArcheiveNote(noteid, UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Archeived Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Empty data found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Add to Pinned Notes ")]
        public ActionResult PinNote(int noteid, int UserId)
        {
            try
            {
                var result = this.NoteManager.PinNote(noteid, UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Pinned Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Empty data found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

      

        [HttpPut]
        [Route("Restore Notes")]
        public ActionResult RestoreNotes(int noteid, int UserId)
        {
            try
            {
                var result = this.NoteManager.RestoreNotes(noteid, UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Images")]
        public ActionResult Image(IFormFile file, int NoteId)
        {
            try
            {
                var result = this.NoteManager.Image(file, NoteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}