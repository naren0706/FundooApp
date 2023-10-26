using FundooModel.Labels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.IManager
{
    public interface ILabelsManager
    {
        public Task<int> AddLabels(label labels);
        public label EditLabel(label labels);
        public IEnumerable<label> GetAllLabels(string email);
        public bool DeleteLabels(string email);
    }
}
