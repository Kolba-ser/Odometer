using Infrastructure.Entities.Config;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Progress
{
    public interface IPersistentConfigService : IService
    {
        public Config Config { get; }

        public void SetProgress(Config playerProgress);
    }
}