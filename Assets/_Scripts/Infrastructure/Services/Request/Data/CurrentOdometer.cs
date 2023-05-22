using System;

namespace Infrastructure.Services.Request
{
    [Serializable]
    public struct CurrentOdometer
    {
        public string operation;
        public float odometer;
    }
}