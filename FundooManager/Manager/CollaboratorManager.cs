using FundooManager.IManager;
using FundooModel.Notes;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        public readonly ICollaboratorRepository CollabRepository;
        public CollaboratorManager(ICollaboratorRepository CollabRepository)
        {
            this.CollabRepository = CollabRepository;
        }

        public Task<int> AddCollaborator(Collaborator collaborator)
        {
            var result = this.CollabRepository.AddCollaborator(collaborator);
            return result;
        }

        public bool DeleteCollab(int noteId, int userId)
        {
            var result = this.CollabRepository.DeleteCollab(noteId, userId);
            return result;
        }

        public IEnumerable<Collaborator> GetAllCollabNotes(int userId)
        {
            var result = this.CollabRepository.GetAllCollabNotes(userId);
            return result;
        }

        public IEnumerable<NotesCollab> GetAllNotesColllab(int userId)
        {
            var result = this.CollabRepository.GetAllNotesColllab(userId);
            return result;
        }
    }
}
