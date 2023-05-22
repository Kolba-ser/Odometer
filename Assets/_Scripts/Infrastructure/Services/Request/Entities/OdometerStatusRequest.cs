using Infrastructure.Entities.Server;

namespace Infrastructure.Services.Request
{
    public class OdometerStatusRequest : OdometerRequestSender<OdometerStatus>
    {
        private const string OPERATION = "getRandomStatus";

        public OdometerStatusRequest(IOdometerServerConnetion odemeterServer) : base(odemeterServer, OPERATION)
        {
        }
    }
}