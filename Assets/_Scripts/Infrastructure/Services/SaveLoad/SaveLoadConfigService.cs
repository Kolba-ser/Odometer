using Infrastructure.Entities.Config;
using Infrastructure.Services.Progress;
using Shared;
using System.IO;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class SaveLoadConfigService : ISaveLoadConfigService
    {

        private readonly IPersistentConfigService progressService;

        public SaveLoadConfigService(IPersistentConfigService progressService)
        {
            this.progressService = progressService;
        }

        public void Save()
        {
            File.WriteAllText($"Assets/Resources/{AssetsPath.CONFIG_PATH}.txt", progressService.Config.ToJson());
        }

        public Config Load()
        {
            TextAsset json = Resources.Load<TextAsset>(AssetsPath.CONFIG_PATH);
            if (json == null)
                return null;

            return json.text.ToDeserialized<Config>();
        }
    }
}