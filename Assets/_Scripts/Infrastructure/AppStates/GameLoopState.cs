namespace Infrastructure.AppStates
{
    public class GameLoopState : IState
    {
        private readonly StateMachine gameStateMachine;

        public GameLoopState(StateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}