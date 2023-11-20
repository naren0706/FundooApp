using FundooManager.IManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FundooModel.Notes;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        public readonly ICollaboratorManager collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
        }
        [HttpPost]
        [Route("AddCollaborator")]
        public async Task<ActionResult> AddCollaborator(Collaborator collaborator)
        {
            try
            {
                var result = await this.collaboratorManager.AddCollaborator(collaborator);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Label Added Successfully", Data = collaborator });
                }
                return this.BadRequest(new { Status = false, Message = "Adding label Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteCollaborator")]
        public ActionResult DeleteCollab(int noteId, int userId)
        {
            try
            {
                var result = this.collaboratorManager.DeleteCollab(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborator Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Collaborator Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetAllCollobbrator")]
        public async Task<ActionResult> GetAllCollabNotes(int userId)
        {
            try
            {
                var result = this.collaboratorManager.GetAllCollabNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Collaborators Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No collaborators Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllNotesColloborator")]
        public async Task<ActionResult> GetAllNotesColllab(int userId)
        {
            try
            {
                var result = this.collaboratorManager.GetAllNotesColllab(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Collaborators Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No collaborators Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }

        }
    }
}
