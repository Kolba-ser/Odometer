using Infrastructure.Entities.Config;
using Infrastructure.SaveLoad;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.AppStates
{
    public class LoadProgressState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IPersistentConfigService progressService;
        private readonly ISaveLoadConfigService saveLoadService;

        public LoadProgressState(StateMachine gameStateMachine, IPersistentConfigService progressService, ISaveLoadConfigService saveLoadService)
        {
            this.stateMachine = gameStateMachine;
            this.progressService = progressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            stateMachine.Enter<LoadLevelState, string>(GameScenes.GAME_SCENE);
        }

        public void Exit()
        {
        }

        private Config LoadProgressOrInitNew()
        {
            Config loaded = saveLoadService.Load();
            Config progress = loaded ?? NewProgress();
            progressService.SetProgress(progress);

            return progress;
        }

        private Config NewProgress()
        {
            Debug.Log("NewConfig");

            Config newProgress = new Config()
            {
                IpAddres = "185.246.65.199",
                Port = 9090
            };

            return newProgress;
        }
    }
}