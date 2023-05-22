using System;

namespace Infrastructure.Services.Request
{
    [Serializable]
    public struct OdometerStatus
    {
        public string operation;
        public bool status;
        public float odometer;
    }
}