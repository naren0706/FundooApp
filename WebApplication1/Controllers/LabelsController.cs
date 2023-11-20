using FundooManager.IManager;
using FundooModel.Notes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FundooModel.Labels;
using Microsoft.AspNetCore.Authorization;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelsController : ControllerBase
    {
        public readonly ILabelsManager labelsManager;
        public LabelsController(ILabelsManager labelsManager)
        {
            this.labelsManager = labelsManager;
        }
        [HttpPost]
        [Route("Add New Label")]
        public async Task<ActionResult> AddLabe(label labels)
        {
            try
            {
                var result = await this.labelsManager.AddLabels(labels);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Adding Label Successful", data = labels });
                }
                return this.BadRequest(new { Status = false, Message = "Label empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Edit Existing Labels")]
        public ActionResult EditLabel(label labels)
        {
            try
            {
                var result = this.labelsManager.EditLabel(labels);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Edit Label Successful", data = labels });
                }
                return this.BadRequest(new { Status = false, Message = "Label found empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Delete Label")]
        public ActionResult DeleteLabels( int UserId)
        {
            try
            {
                var result = this.labelsManager.DeleteLabels(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Deleted label" });
                }
                return this.BadRequest(new { Status = false, Message = "label not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get All Label")]
        public async Task<ActionResult> GetAllLabels(int UserId)
        {
            try
            {
                var result = this.labelsManager.GetAllLabels(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "labels displayed", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No labels Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
