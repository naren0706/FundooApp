using FundooModel.Notes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.IManager
{
    public interface ICollaboratorManager
    {
        public Task<int> AddCollaborator(Collaborator collaborator);
        public bool DeleteCollab(int noteId, int userId);
        public IEnumerable<Collaborator> GetAllCollabNotes(int userId);
        public IEnumerable<NotesCollab> GetAllNotesColllab(int userId);
    }
}
