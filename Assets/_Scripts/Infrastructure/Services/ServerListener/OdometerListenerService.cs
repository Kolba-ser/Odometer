using Cysharp.Threading.Tasks;
using Infrastructure.Entities.Server;
using System;
using System.Net.WebSockets;
using System.Threading;

namespace Infrastructure.Services.Request
{
    public class OdometerListenerService : IOdometerListenerService
    {
        private readonly IOdometerServerConnetion connection;

        private readonly OdometerStatusRequest statusRequest;
        private readonly CurrentOdometerRequest odometerRequest;

        private bool isStopped;

        public event Action<bool> OnStatusReceived;

        public event Action<float> OnValueRecieved;

        public OdometerListenerService(IOdometerServerConnetion connection)
        {
            this.connection = connection;
            statusRequest = new OdometerStatusRequest(connection);
            odometerRequest = new CurrentOdometerRequest(connection);
        }

        public async UniTask GetStatus()
        {
            OdometerStatus status = await statusRequest.SendAsync();
            CurrentOdometer currentOdometer = await odometerRequest.SendAsync();
            Notify(status, currentOdometer);
        }

        public async void Track()
        {
            byte[] buf = new byte[1056];

            isStopped = false;
            while (connection.Client.State == WebSocketState.Open && !isStopped)
            {
                await connection.Client.ReceiveAsync(buf, CancellationToken.None);
                await GetStatus();
            }
        }

        public void Stop()
        {
            isStopped = true;
        }
        private void Notify(OdometerStatus status, CurrentOdometer currentOdometer)
        {
            OnStatusReceived?.Invoke(status.status);
            if (status.status)
                OnValueRecieved?.Invoke(currentOdometer.odometer);
        }
    }
}