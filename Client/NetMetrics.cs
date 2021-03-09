using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    abstract class NetMetrics
    {
        protected bool connected;

        public abstract string Protocol { get; }

        public bool Connected { get => connected; }

        public abstract void Connect(string IP, int port);
        public abstract void Close();
        public abstract double Ping();
        public abstract double Speed(int time);
        public abstract double Delay();
        public abstract double DeliveryCoef(int packets, int packSize);
        public abstract int PacketLoss(int packets, int packSize);
    }
}
