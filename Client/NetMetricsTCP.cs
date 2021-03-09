using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    class NetMetricsTCP : NetMetrics
    {
        private TCPSocket InfoSocket;
        private TCPSocket TestSocket;
        private System.Diagnostics.Stopwatch timer, watch;
        private string IP;
        int port;

        public NetMetricsTCP()
        {
            InfoSocket = new TCPSocket();
        }
        public override string Protocol => "TCP";
        public override void Connect(string IP, int port)
        {
            Close();
            this.IP = IP;
            InfoSocket = new TCPSocket(IP, port);
            this.port = BitConverter.ToInt32(InfoSocket.Receive(), 0);
            InfoSocket.Receive();
            watch = System.Diagnostics.Stopwatch.StartNew();
            connected = true;
        }

        public override void Close()
        {
            if (InfoSocket.socket.Connected)
            {
                InfoSocket.Send(BitConverter.GetBytes(1948), sizeof(int));
                InfoSocket.socket.Close();
            }
            connected = false;
        }
        public override double Ping()
        {
            InfoSocket.Send(BitConverter.GetBytes(0), sizeof(int));
            timer = System.Diagnostics.Stopwatch.StartNew();
            InfoSocket.Receive(sizeof(int));
            timer.Stop();
            return timer.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000;
        }

        public override double Speed(int time)
        {
            InfoSocket.Send(BitConverter.GetBytes(1));
            TestSocket = new TCPSocket(IP, port);
            InfoSocket.Send(BitConverter.GetBytes(time));
            byte[] buffer = new byte[1024*100];
            long send = 0;
            InfoSocket.Receive();
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < time * 1000 && send <= 1024 * 1024 * 100 * time)
            {
                TestSocket.Send(buffer);
                send += buffer.Length;
            }
            stopwatch.Stop();
            return BitConverter.ToInt64(InfoSocket.Receive(), 0) / 1024.0 / 1024.0 / time;
        }

        public override double Delay()
        {
            InfoSocket.Send(BitConverter.GetBytes(2));
            InfoSocket.Send(BitConverter.GetBytes(watch.ElapsedTicks), sizeof(long));
            return BitConverter.ToInt64(InfoSocket.Receive(), 0) / (double)System.Diagnostics.Stopwatch.Frequency * 1000;
        }
        public override double DeliveryCoef(int packets, int packSize)
        {
            InfoSocket.socket.SendBufferSize = packSize * packets;
            InfoSocket.socket.SendTimeout = -1;
            InfoSocket.Send(BitConverter.GetBytes(3));
            TestSocket = new TCPSocket(IP, port);
            InfoSocket.Send(BitConverter.GetBytes(packets), sizeof(int));
            InfoSocket.Send(BitConverter.GetBytes(packSize), sizeof(int));
            byte[] buffer = new byte[packSize];
            for (int i = 0; i < packets; i++)
                TestSocket.socket.Send(buffer, packSize, SocketFlags.None);

            return BitConverter.ToDouble(InfoSocket.Receive(), 0);
        }
        public override int PacketLoss(int packets, int packSize)
        {
            InfoSocket.socket.SendBufferSize = packSize * packets;
            InfoSocket.socket.SendTimeout = -1;
            InfoSocket.Send(BitConverter.GetBytes(4));
            TestSocket = new TCPSocket(IP, port);
            InfoSocket.Send(BitConverter.GetBytes(packets), sizeof(int));
            InfoSocket.Send(BitConverter.GetBytes(packSize), sizeof(int));
            byte[] buffer = new byte[packSize];
            for (int i = 0; i < packets; i++)
                TestSocket.socket.Send(buffer, packSize, SocketFlags.None);

            return BitConverter.ToInt32(InfoSocket.Receive(), 0);
        }

    }
}
