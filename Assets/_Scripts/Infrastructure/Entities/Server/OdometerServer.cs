using Cysharp.Threading.Tasks;
using System;
using System.Net.WebSockets;
using System.Threading;

namespace Infrastructure.Entities.Server
{
    public class OdometerServer : IOdometerServerConnetion
    {
        private ClientWebSocket client;

        public ClientWebSocket Client => client;

        public async UniTask Connect(string url)
        {
            Dispose();
            client = new ClientWebSocket();

            await client.ConnectAsync(new Uri(url), CancellationToken.None);
        }

        public void Dispose()
        {
            if (client != null && client.State == WebSocketState.Open)
            {
                client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                client = null;
            }
            
        }
    }
}