using Infrastructure.AppStates;
using Infrastructure.Bootstrap;
using Infrastructure.Entities.Server;
using Infrastructure.Services;
using Logic.Loading;
using System;

namespace Infrastructure
{
    public class Game : IDisposable
    {
        public StateMachine StateMachine;
        public IOdometerServerConnetion connetion;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen, IOdometerServerConnetion connetion)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);
            loadingScreen.SetSceneLoader(sceneLoader);
            StateMachine = new StateMachine(sceneLoader, loadingScreen, AllServices.Container);
            this.connetion = connetion;
        }

        public void Dispose()
        {
            AllServices.Container.Dispose();
            StateMachine.Dispose();
            connetion.Dispose();
            StateMachine = null;
        }
    }
}