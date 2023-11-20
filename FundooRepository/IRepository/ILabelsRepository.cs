using FundooModel.Labels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.IRepository
{
    public interface ILabelsRepository
    {
        public Task<int> AddLabels(label labels);
        public label EditLabel(label labels);
        public IEnumerable<label> GetAllLabels(int UserId);
        public bool DeleteLabels(int UserId);
    }
}
