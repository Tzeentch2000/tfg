namespace tfg.Repository.Services.IStateRepository
{
    public interface IStateRepository
    {
        IEnumerable<State> GetAllStates();
        State GetStateById(int id);
        void CreateState(State model);
        void UpdateState(State model);
        void DeleteState(State model);
    }
}