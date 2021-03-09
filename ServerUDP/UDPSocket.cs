using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class UDPSocket
    {
        public Socket socket;
        public IPEndPoint udpEndPoint;
        public EndPoint senderEndPoint;

        public UDPSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpEndPoint = new IPEndPoint(IPAddress.Any, 8083);
            senderEndPoint = new IPEndPoint(0, 0);
            socket.Bind(udpEndPoint);
        }
        public byte[] Receive(int bufferSize = 1000)
        {
            byte[] buffer = new byte[bufferSize];
            int size = 0;
            size += socket.ReceiveFrom(buffer, ref senderEndPoint);
            byte[] ret = new byte[size];
            Array.Copy(buffer, ret, size);
            return ret;
        }
        public int ReceiveAll(double milliseconds)
        {
            Stopwatch stopwatch = new Stopwatch();
            byte[] buffer = new byte[650000];
            int size = 0;
            stopwatch.Start();
            while(socket.Available > 0 && stopwatch.ElapsedMilliseconds < milliseconds)
                size += socket.ReceiveFrom(buffer, ref senderEndPoint);
            return size;
        }
        public int Send(byte[] data)
        {
            return socket.SendTo(data, senderEndPoint);
        }
    }
}
