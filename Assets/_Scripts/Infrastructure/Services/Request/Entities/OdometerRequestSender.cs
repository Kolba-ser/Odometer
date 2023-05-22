using Cysharp.Threading.Tasks;
using Infrastructure.Entities.Server;
using Shared;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace Infrastructure.Services.Request
{
    public abstract class OdometerRequestSender<T> : IRequest<T>
    {
        private readonly IOdometerServerConnetion odemeterServer;
        private readonly string operation;

        protected OdometerRequestSender(IOdometerServerConnetion odemeterServer, string operation)
        {
            this.odemeterServer = odemeterServer;
            this.operation = operation;
        }

        [Serializable]
        private struct Request
        {
            public string operation;
        }

        public async UniTask<T> SendAsync()
        {
            var request = new Request { operation = operation };
            var jsonRequest = request.ToJson();

            await odemeterServer.Client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonRequest)),
                                           WebSocketMessageType.Text, true, CancellationToken.None);

            var buffer = new ArraySegment<byte>(new byte[8192]);
            WebSocketReceiveResult result = null;
            try
            {
                result = await odemeterServer.Client.ReceiveAsync(buffer, CancellationToken.None);
            }
            catch (WebSocketException)
            {
                return default(T);
            }

            var responseJson = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, result.Count);

            if (responseJson == null || responseJson == string.Empty)
                return default(T);

            return responseJson.ToDeserialized<T>();
        }
    }
}