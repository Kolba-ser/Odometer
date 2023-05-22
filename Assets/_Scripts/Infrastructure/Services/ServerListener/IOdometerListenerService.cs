using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using System;

namespace Infrastructure.Services.Request
{
    public interface IOdometerListenerService : IService
    {
        public event Action<bool> OnStatusReceived;

        public event Action<float> OnValueRecieved;

        UniTask GetStatus();
        void Stop();
        void Track();
    }
}