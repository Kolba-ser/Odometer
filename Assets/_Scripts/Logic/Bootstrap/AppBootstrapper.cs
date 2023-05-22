using Infrastructure.AppStates;
using Infrastructure.Entities.Server;
using Logic.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Bootstrap
{
    public class AppBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        public static AppBootstrapper instance;

        private Game game;

        private void Awake()
        {
            if(instance != null && this != instance)
            {
                Destroy(gameObject);
                return;
            }

            if (instance != null && SceneManager.GetActiveScene().name != GameScenes.INITIAL_SCENE)
            {
                SceneManager.LoadScene(GameScenes.INITIAL_SCENE);
                return;
            }

            var loadscreen = Instantiate(loadingScreenPrefab);
            Init(loadscreen);
        }

        private void Init(LoadingScreen loadscreen)
        {
            OdometerServer connetion = new OdometerServer();
            game = new Game(this, loadscreen, connetion);
            game.StateMachine.Enter<BootstrapState, IOdometerServerConnetion>(connetion);
            instance = this;

            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            game?.Dispose();
        }
    }
}