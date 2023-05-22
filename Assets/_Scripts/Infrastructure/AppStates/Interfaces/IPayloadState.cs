namespace Infrastructure.AppStates
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}