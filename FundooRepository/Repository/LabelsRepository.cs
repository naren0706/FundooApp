using FundooModel.Labels;
using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelsRepository : ILabelsRepository
    {
        public readonly UserDbContext context;
        NlogOperation nlog = new NlogOperation();
        public LabelsRepository(UserDbContext context)
        {
            this.context = context;
        }
        public Task<int> AddLabels(label labels)
        {
            this.context.Labels.Add(labels);
            var result = this.context.SaveChangesAsync();
            nlog.LogInfo("Added labels");
            return result;
        }
        public label EditLabel(label labels)
        {
            var data = this.context.Labels.Where(x => x.Id == labels.Id && x.EmailId == labels.EmailId).FirstOrDefault();
            if (data != null)
            {
                data.Id = labels.Id;
                data.LabelName  = labels.LabelName;
                data.EmailId = labels.EmailId;
                nlog.LogInfo("Edited label successful");
                return data;
            }
            nlog.LogWarn("No labels found");
            return null;
        }
        public IEnumerable<label> GetAllLabels(string email)
        {
            var result = this.context.Labels.Where(x => x.EmailId == email).AsEnumerable();
            if (result != null)
            {
                nlog.LogInfo("Got all labels");
                return result;
            }
            nlog.LogError("Label on email not found");
            return null;
        }

        public bool DeleteLabels(string email)
        {
            var result = this.context.Labels.Where(x => x.EmailId == email ).ToList();
            foreach (var data in result)
            {
                nlog.LogInfo("Deleted label successfully");
                this.context.Labels.Remove(data);
            }
            var deleteResult = this.context.SaveChanges();
            if (deleteResult == 0)
            {
                nlog.LogWarn("labels is null");
                return false;
            }
            nlog.LogWarn("labels not found");
            return false;
        }
    }
}
