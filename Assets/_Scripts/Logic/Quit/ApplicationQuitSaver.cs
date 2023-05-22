using Infrastructure.SaveLoad;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Logic.Quit
{
    public class ApplicationQuitSaver : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            AllServices.Container.GetSingle<ISaveLoadConfigService>().Save();
        }
    }
}
