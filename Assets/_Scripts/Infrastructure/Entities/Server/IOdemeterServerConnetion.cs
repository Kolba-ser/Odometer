using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using System;
using System.Net.WebSockets;

namespace Infrastructure.Entities.Server
{
    public interface IOdometerServerConnetion: IService, IDisposable
    {
        public ClientWebSocket Client
        {
            get;
        }

        public UniTask Connect(string url);
    }
}