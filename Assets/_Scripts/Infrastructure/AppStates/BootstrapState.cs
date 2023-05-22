using Cysharp.Threading.Tasks;
using Infrastructure.Entities.Server;
using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Providers.Assets;
using Infrastructure.Services.Request;
using UnityEngine;

namespace Infrastructure.AppStates
{
    public class BootstrapState : IPayloadState<IOdometerServerConnetion>
    {
        private readonly StateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices allServices;
        private AssetsProvider assetsProvider;

        public BootstrapState(StateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.allServices = allServices;

            RegisterServices();
        }

        public void Enter(IOdometerServerConnetion connetion)
        {
            allServices.RegisterSingle<IOdometerServerConnetion>(connetion);
            sceneLoader.Load(GameScenes.INITIAL_SCENE, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            assetsProvider = new AssetsProvider();
            allServices.RegisterSingle<IAssetsProvider>(assetsProvider);

            PersistentConfigService progressService = new PersistentConfigService();
            allServices.RegisterSingle<IPersistentConfigService>(progressService);
            allServices.RegisterSingle<ISaveLoadConfigService>(new SaveLoadConfigService(progressService));

        }



    }
}