using Infrastructure.Entities.Config;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadConfigService : IService
    {
        public void Save();

        public Config Load();
    }
}