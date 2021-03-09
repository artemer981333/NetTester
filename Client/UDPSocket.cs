using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class UDPSocket
    {
        public Socket socket;
        public IPEndPoint udpEndPoint;
        public EndPoint senderEndPoint;

        public UDPSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpEndPoint = new IPEndPoint(IPAddress.Any, 8082);
            socket.Bind(udpEndPoint);
        }
        public UDPSocket(EndPoint senderEndPoint)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpEndPoint = new IPEndPoint(IPAddress.Any, 8082);
            socket.Bind(udpEndPoint);
            this.senderEndPoint = senderEndPoint;
        }
        public byte[] Receive(int bufferSize = 1000)
        {
            byte[] buffer = new byte[bufferSize];
            int size = 0;
            do
            {
                size += socket.ReceiveFrom(buffer, ref senderEndPoint);
            } while (socket.Available > 0);
            byte[] ret = new byte[size];
            Array.Copy(buffer, ret, size);
            return ret;
        }
        public int Send(byte[] data)
        {
            return socket.SendTo(data, senderEndPoint);
        }
    }
}
