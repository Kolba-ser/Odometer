using Infrastructure.Entities.Config;

namespace Infrastructure.Services.Progress
{
    public class PersistentConfigService : IPersistentConfigService
    {
        private Config progress;

        private bool isProgressSet;

        public Config Config => progress;

        public void SetProgress(Config playerProgress)
        {
            if (!isProgressSet)
            {
                progress = playerProgress;
                isProgressSet = true;
            }
        }
    }
}