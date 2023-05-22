using Cysharp.Threading.Tasks;
using Infrastructure.Entities.Config;
using Infrastructure.Entities.Server;
using Infrastructure.Services;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Providers.Assets;
using Infrastructure.Services.Request;
using Logic.Loading;
using Logic.UI.Hud;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.AppStates
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly StateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingScreen loadingScreen;

        private AllServices allServices;
        private Config config;

        public LoadLevelState(StateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
            this.allServices = allServices;
        }

        public void Enter(string sceneName)
        {
            sceneLoader.Load(sceneName, OnLoaded);
            loadingScreen.Show();
        }

        private async Task InitializeGameWorld()
        {
            await CreateHUD();
            
            var odometerListenerService = allServices.GetSingle<IOdometerListenerService>();
            await odometerListenerService.GetStatus();
            odometerListenerService.Track();
        }

        private async Task CreateHUD()
        {
            var prefab = await allServices.GetSingle<IAssetsProvider>().LoadAsync<HudComponents>(AssetsPath.HUD_PATH);
            var components = GameObject.Instantiate(prefab);
            components.Construct(allServices);
            
        }

        public void Exit()
        {
            loadingScreen.Hide();
        }

        private async void OnLoaded()
        {
            config = allServices.GetSingle<IPersistentConfigService>().Config;
            await Connect();
            RegisterConnectionListener();
            await InitializeGameWorld();

            gameStateMachine.Enter<GameLoopState>();
        }

        private async UniTask Connect()
        {
            string url = $"ws://{config.IpAddres}:{config.Port}/ws";
            await allServices.GetSingle<IOdometerServerConnetion>().Connect(url);
        }

        private void RegisterConnectionListener() => 
            allServices.RegisterSingle<IOdometerListenerService>(new OdometerListenerService(allServices.GetSingle<IOdometerServerConnetion>()));
    }
}