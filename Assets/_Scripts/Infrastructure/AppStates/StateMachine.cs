using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.Progress;
using Logic.Loading;
using System;
using System.Collections.Generic;

namespace Infrastructure.AppStates
{
    public class StateMachine : IDisposable
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public StateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.GetSingle<IPersistentConfigService>(), services.GetSingle<ISaveLoadConfigService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        public TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        public void Dispose()
        {
            _states = null;
            _activeState = null;
        }
    }
}