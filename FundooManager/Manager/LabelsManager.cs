﻿using FundooManager.IManager;
using FundooModel.Labels;
using FundooModel.Notes;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelsManager : ILabelsManager
    {
        public readonly ILabelsRepository LabelRepository;
        public LabelsManager(ILabelsRepository LabelRepository)
        {
            this.LabelRepository = LabelRepository;
        }
        public Task<int> AddLabels(label labels)
        {
            var result = this.LabelRepository.AddLabels(labels);
            return result;
        }

        public bool DeleteLabels(int UserId)
        {
            var result = this.LabelRepository.DeleteLabels(UserId);
            return result;
        }

        public label EditLabel(label labels)
        {
            var result = this.LabelRepository.EditLabel(labels);
            return result;
        }

        public IEnumerable<label> GetAllLabels(int UserId)
        {
            var result = this.LabelRepository.GetAllLabels(UserId);
            return result;
        }
    }
}
