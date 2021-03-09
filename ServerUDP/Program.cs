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
        static UDPSocket TestSocket;
        static Socket mainSocket;
        static System.Diagnostics.Stopwatch watch;

        static void Main(string[] args)
        {
            watch = System.Diagnostics.Stopwatch.StartNew();

            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8089);
            mainSocket.Bind(endPoint);
            Console.WriteLine("Bind OK");
            mainSocket.Listen(10);
            Console.WriteLine("Listen OK");
            while (true)
            {
                TCPSocket request = new TCPSocket();
                request.socket = mainSocket.Accept();
                request.Send(BitConverter.GetBytes(8083));
                TestSocket = new UDPSocket();
                TestSocket.Receive();
                watch = System.Diagnostics.Stopwatch.StartNew();
                Console.WriteLine("Accept OK");
                handleRequest(request);
            }
        }

        static void handleRequest(TCPSocket InfoSocket)
        {
            Console.WriteLine("Start handle");
            while (true)
            {
                watch.Restart();
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
                            Console.WriteLine("Speed");
                            Speed(InfoSocket);
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
                            Console.WriteLine("Delivery coef");
                            DeliveryCoef(InfoSocket);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Packet loss");
                            PacketLoss(InfoSocket);
                            break;
                        }
                    default:
                        {
                            InfoSocket.socket.Close();
                            TestSocket.socket.Close();
                            Console.WriteLine("Closed");
                            return;
                        }
                }
            }
        }

        static void Ping(TCPSocket InfoSocket)
        {
            TestSocket.Send(BitConverter.GetBytes(0));
        }

        static void Speed(TCPSocket InfoSocket)
        {
            int time = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = int.MaxValue;
            TestSocket.socket.ReceiveTimeout = 1000;

            InfoSocket.Send(BitConverter.GetBytes(0));

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            long bytes = 0;

            while (stopwatch.ElapsedMilliseconds <= time * 1000)
            {
                try
                {
                    bytes += TestSocket.ReceiveAll(time * 1000);
                }
                catch (Exception e)
                {

                }
            }
            stopwatch.Stop();
            Thread.Sleep(100);
            while (TestSocket.socket.Available > 0)
            {
                Console.WriteLine("Available: " + TestSocket.socket.Available.ToString());
                TestSocket.ReceiveAll(TestSocket.socket.Available);
                Thread.Sleep(100);
            }

            TestSocket.socket.ReceiveTimeout = -1;
            InfoSocket.Send(BitConverter.GetBytes(bytes));
            Console.WriteLine("Time: " + time.ToString() + " s");
            Console.WriteLine("Size: " + (bytes / 1024.0 / 1024.0).ToString() + " MB");
            Console.WriteLine("Speed: " + (bytes / 1024.0 / 1024.0 / time).ToString() + " MB/s");
        }

        static void Delay(TCPSocket InfoSocket)
        {
            long clientTicks = BitConverter.ToInt64(TestSocket.Receive(), 0);
            long delayTicks = watch.ElapsedTicks - clientTicks;
            InfoSocket.Send(BitConverter.GetBytes(delayTicks));
            double secTime = delayTicks / (double)System.Diagnostics.Stopwatch.Frequency;
            Console.WriteLine("Delay: " + secTime.ToString() + " s");
        }
        static void DeliveryCoef(TCPSocket InfoSocket)
        {
            int packets = BitConverter.ToInt32(InfoSocket.Receive(), 0);
            int packSize = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = packSize * packets;
            TestSocket.socket.ReceiveTimeout = 1000;

            int bytes = 0;

            while (true)
            {
                try
                {
                    bytes += TestSocket.Receive(packSize * packets).Length;
                }
                catch (Exception e)
                {
                    break;
                }
                if (bytes >= packSize * packets)
                    break;
            }

            double deliveryKoef = bytes / (double)packSize / (double)packets;

            InfoSocket.Send(BitConverter.GetBytes(deliveryKoef));
        }
        static void PacketLoss(TCPSocket InfoSocket)
        {
            int packets = BitConverter.ToInt32(InfoSocket.Receive(), 0);
            int packSize = BitConverter.ToInt32(InfoSocket.Receive(), 0);

            TestSocket.socket.ReceiveBufferSize = packSize * packets;
            TestSocket.socket.ReceiveTimeout = 1000;

            int bytes = 0;

            while (true)
            {
                try
                {
                    bytes += TestSocket.Receive(packSize * packets).Length;
                }
                catch(Exception e)
                {
                    break;
                }
                if (bytes >= packSize * packets)
                    break;
            }

            int packetLoss = packets - bytes / packSize;

            InfoSocket.Send(BitConverter.GetBytes(packetLoss));
            Console.WriteLine("PL: " + packetLoss.ToString() + " packets");
        }

    }
}
