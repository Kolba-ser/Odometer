using Infrastructure.Entities.Server;

namespace Infrastructure.Services.Request
{
    public class CurrentOdometerRequest : OdometerRequestSender<CurrentOdometer>
    {
        private const string OPERATION = "getCurrentOdometer";

        public CurrentOdometerRequest(IOdometerServerConnetion odemeterServer) : base(odemeterServer, OPERATION)
        {
        }
    }
}