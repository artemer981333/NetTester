using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class TCPSocket
    {
        public Socket socket;

        public TCPSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public TCPSocket(string IP, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connect(IP, port);
        }
        public void Connect(string IP, int port)
        {
            socket.Connect(IPAddress.Parse(IP), port);
        }
        public byte[] Receive()
        {
            byte[] len = new byte[sizeof(int)];
            socket.Receive(len, sizeof(int), SocketFlags.None);
            return Receive(BitConverter.ToInt32(len, 0));
        }
        public byte[] Receive(int size)
        {
            byte[] ret = new byte[size];
            socket.Receive(ret, size, SocketFlags.None);
            return ret;
        }
        public void Send(byte[] data, int size)
        {
            socket.Send(BitConverter.GetBytes(size), sizeof(int), SocketFlags.None);
            socket.Send(data, size, SocketFlags.None);
        }
        public void Send(byte[] data)
        {
            socket.Send(BitConverter.GetBytes(data.Length), sizeof(int), SocketFlags.None);
            socket.Send(data, data.Length, SocketFlags.None);
        }
    }
}
