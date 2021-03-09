using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    class Program
    {
        static Socket mainSocket;
        static System.Diagnostics.Stopwatch watch;

        static void Main(string[] args)
        {
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8080);
            mainSocket.Bind(endPoint);
            Console.WriteLine("Bind OK");
            mainSocket.Listen(10);
            Console.WriteLine("Listen OK");
            while(true)
            {
                TCPSocket request = new TCPSocket();
                request.socket = mainSocket.Accept();
                watch = System.Diagnostics.Stopwatch.StartNew();
                Console.WriteLine("Accept OK");
                handleRequest(request);
            }
        }

        static void handleRequest(TCPSocket InfoSocket)
        {
            Console.WriteLine("Start handle");
            Socket testSocketConnecter = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8087);
            InfoSocket.Send(BitConverter.GetBytes(8087), sizeof(int));
            testSocketConnecter.Bind(endPoint);
            testSocketConnecter.Listen(10);
            TCPSocket TestSocket = new TCPSocket();
            InfoSocket.Send(BitConverter.GetBytes(0), sizeof(int));
            while (true)
            {
                int queryType = BitConverter.ToInt32(InfoSocket.Receive(), 0);
                switch (queryType)
                {
                    case 0:
                        {
                            Console.WriteLine("Ping");
                            Ping(InfoSocket);
                            break;
                        }
                    case 1:
                        {
                            TestSocket.socket = testSocketConnecter.Accept(); 
                            Console.WriteLine("Speed");
                            Speed(InfoSocket, TestSocket);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Delay");
                            Delay(InfoSocket);
                            break;
                        }
                    case 3:
                        {
                            TestSocket.socket = testSocketConnecter.Accept();
                            Console.WriteLine("Delivery coef");
                            DeliveryCoef(InfoSocket, TestSocket);
                            break;
                        }
                    case 4:
                        {
                            TestSocket.socket = testSocketConnecter.Accept();
                            Console.WriteLine("Packet loss");
                            PacketLoss(InfoSocket, TestSocket);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Closed");
                            return;
                        }
                }
            }
        }

        static void Ping(TCPSocket socket)
        {
            socket.socket.Send(new byte[sizeof(int)], sizeof(int), SocketFlags.None);
        }

        static void Speed(TCPSocket InfoSocket, TCPSocket TestSocket)
        {
            int time = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = int.MaxValue;
            TestSocket.socket.ReceiveTimeout = 1000;

            InfoSocket.Send(BitConverter.GetBytes(0), sizeof(int));

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            long bytes = 0;

            while (stopwatch.ElapsedMilliseconds <= time * 1000)
            {
                try
                {
                    bytes += TestSocket.Receive().Length;
                }
                catch(Exception e)
                {

                }
            }
            stopwatch.Stop();
            //Thread.Sleep(200);
            //while (InfoSocket.socket.Available > 0)
            //{
            //    Console.WriteLine("While: " + InfoSocket.socket.Available.ToString());
            //    InfoSocket.Receive(InfoSocket.socket.Available);
            //    Thread.Sleep(200);
            //}

            InfoSocket.Send(BitConverter.GetBytes(bytes), sizeof(long));
            Console.WriteLine("Time: " + time.ToString() + " s");
            Console.WriteLine("Size: " + (bytes / 1024.0 / 1024.0).ToString() + " MB");
            Console.WriteLine("Speed: " + (bytes / 1024.0 / 1024.0 / time).ToString() + " MB/s");
        }
        static void Delay(TCPSocket socket)
        {
            long ticks = BitConverter.ToInt32(socket.Receive(), 0);
            long delayTicks = watch.ElapsedTicks - ticks;
            socket.Send(BitConverter.GetBytes(delayTicks), sizeof(long));
            double secTime = delayTicks / (double)System.Diagnostics.Stopwatch.Frequency;
            Console.WriteLine("Delay: " + secTime.ToString() + " s");
        }
        static void DeliveryCoef(TCPSocket InfoSocket, TCPSocket TestSocket)
        {
            int packets = BitConverter.ToInt32(InfoSocket.Receive(), 0);
            int packSize = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = packSize * packets;
            TestSocket.socket.ReceiveTimeout = -1;

            byte[] recv = new byte[packSize];
            int bytes = 0;

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            while (true)
            {
                bytes += TestSocket.socket.Receive(recv, packSize, SocketFlags.None);
                if (timer.ElapsedMilliseconds >= 5000)
                    break;
                if (bytes >= packSize * packets)
                    break;
                timer.Restart();
            }

            double deliveryKoef = bytes / (double)packSize / (double)packets;

            InfoSocket.Send(BitConverter.GetBytes(deliveryKoef), sizeof(double));
        }
        static void PacketLoss(TCPSocket InfoSocket, TCPSocket TestSocket)
        {
            int packets = BitConverter.ToInt32(InfoSocket.Receive(), 0);
            int packSize = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = packSize * packets;
            TestSocket.socket.ReceiveTimeout = -1;

            byte[] recv = new byte[packSize];
            int bytes = 0;

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            while (true)
            {
                bytes += TestSocket.socket.Receive(recv, packSize, SocketFlags.None);
                if (timer.ElapsedMilliseconds >= 5000)
                    break;
                if (bytes >= packSize * packets)
                    break;
                timer.Restart();
            }

            int packetLoose = packets - bytes / packSize;

            InfoSocket.Send(BitConverter.GetBytes(packetLoose), sizeof(int));
        }
    }
}
