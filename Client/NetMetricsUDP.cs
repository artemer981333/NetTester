using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class NetMetricsUDP : NetMetrics
    {
        public UDPSocket TestSocket;
        public TCPSocket InfoSocket;
        public System.Diagnostics.Stopwatch timer;

        public NetMetricsUDP()
        {
            InfoSocket = new TCPSocket();
            TestSocket = new UDPSocket();
            timer = new System.Diagnostics.Stopwatch();
        }
        public override string Protocol => "UDP";

        public override void Connect(string IP, int portTCP)
        {
            Close();
            InfoSocket = new TCPSocket(IP, portTCP);
            TestSocket = new UDPSocket(new IPEndPoint(IPAddress.Parse(IP), BitConverter.ToInt32(InfoSocket.Receive(), 0)));
            TestSocket.Send(BitConverter.GetBytes(0));
            connected = true;
        }

        public override void Close()
        {
            if (InfoSocket.socket.Connected)
                InfoSocket.Send(BitConverter.GetBytes(1948));
            Thread.Sleep(5);
            InfoSocket.socket.Close();
            TestSocket.socket.Close();
            connected = false;
        }
        public override double Ping()
        {
            InfoSocket.Send(BitConverter.GetBytes(0));
            timer.Restart();
            TestSocket.Receive();
            timer.Stop();
            return timer.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000;
        }

        public override double Speed(int time)
        {
            InfoSocket.Send(BitConverter.GetBytes(1));
            InfoSocket.Send(BitConverter.GetBytes(time));
            TestSocket.socket.SendBufferSize = int.MaxValue;
            InfoSocket.Receive();
            byte[] buffer = new byte[65000];
            long send = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < time * 1000 && send < 1024 * 1024 * 100 * time)
                send += TestSocket.Send(buffer);
            stopwatch.Stop();
            return BitConverter.ToInt64(InfoSocket.Receive(), 0) / 1024.0 / 1024.0 / time;
        }

        public override double Delay()
        {
            InfoSocket.Send(BitConverter.GetBytes(2));
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TestSocket.Send(BitConverter.GetBytes(stopwatch.ElapsedTicks));
            return BitConverter.ToInt64(InfoSocket.Receive(), 0) / (double)System.Diagnostics.Stopwatch.Frequency * 1000;
        }
        public override double DeliveryCoef(int packets, int packSize)
        {
            TestSocket.socket.SendBufferSize = packSize;
            TestSocket.socket.SendTimeout = -1;
            InfoSocket.Send(BitConverter.GetBytes(3));
            InfoSocket.Send(BitConverter.GetBytes(packets));
            InfoSocket.Send(BitConverter.GetBytes(packSize));
            byte[] buffer = new byte[packSize];
            for (int i = 0; i < packets; i++)
                TestSocket.Send(buffer);

            return BitConverter.ToDouble(InfoSocket.Receive(), 0);
        }
        public override int PacketLoss(int packets, int packSize)
        {
            TestSocket.socket.SendBufferSize = packSize;
            TestSocket.socket.SendTimeout = -1;
            InfoSocket.Send(BitConverter.GetBytes(4));
            InfoSocket.Send(BitConverter.GetBytes(packets));
            InfoSocket.Send(BitConverter.GetBytes(packSize));
            byte[] buffer = new byte[packSize];
            for (int i = 0; i < packets; i++)
                TestSocket.Send(buffer);

            return BitConverter.ToInt32(InfoSocket.Receive(), 0);
        }

    }
}
