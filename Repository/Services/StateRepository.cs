using ContextDB;
using Microsoft.EntityFrameworkCore;
using tfg.Repository.Base.RepositoryBase;
using tfg.Repository.Services.IStateRepository;

namespace tfg.Repository.StateRepository
{
    public class StateRepository : RepositoryBase<State>, IStateRepository 
    { 
        public StateRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<State> GetAllStates()
        { 
            return FindAll()
                .OrderBy(s => s.Name)
                .ToList(); 
        }

        public State GetStateById(int stateId)
        {
            return FindByCondition(u => u.Id.Equals(stateId))
                .FirstOrDefault();
        }

        public void CreateState(State state)
        {
            Create(state);
        }

        public void UpdateState(State state)
        {
            Update(state);
        }

        public void DeleteState(State state)
        {
            Delete(state);
        }
    }
}
