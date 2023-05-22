namespace Infrastructure.AppStates
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}