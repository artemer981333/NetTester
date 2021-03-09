using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Client
{
    class NetStatistics
    {
        public enum ProtocolType
        {
            TCP,
            UDP
        }
        public enum MetricType
        {
            Ping,
            Speed,
            Delay,
            DeliveryCoefficient,
            PacketLoss
        }

        private List<double> pingCollection, speedCollection, delayCollection, deliveryCoefCollection, packetLooseCollection, timeCollection;
        private NetMetrics metrics;
        private int speedTime;
        private int dcPackSize, dcPackets;
        private int plPackSize, plPackets;
        private Stopwatch timer;
        private Chart graph;
        private TextBox resultTextbox;
        private ProtocolType protocolType;

        public List<double> PingCollection { get => pingCollection; }
        public List<double> SpeedCollection { get => speedCollection; }
        public List<double> DelayCollection { get => delayCollection; }
        public List<double> DeliveryCoefCollection { get => deliveryCoefCollection; }
        public List<double> PacketLooseCollection { get => packetLooseCollection; }
        public List<double> TimeCollection { get => timeCollection; }
        public int SpeedTime { get => speedTime; set => speedTime = value; }
        public int DCPackSize { get => dcPackSize; set => dcPackSize = value; }
        public int DCPackets { get => dcPackets; set => dcPackets = value; }
        public int PlPackSize { get => plPackSize; set => plPackSize = value; }
        public int PlPackets { get => plPackets; set => plPackets = value; }
        public Chart Graph { get => graph; set => graph = value; }
        public TextBox ResultTextbox { get => resultTextbox; set => resultTextbox = value; }
        public bool Connected { get => metrics.Connected; }
        internal ProtocolType Protocol { get => protocolType; }

        public NetStatistics(ProtocolType protocol)
        {
            switch (protocol)
            {
                case ProtocolType.TCP:
                    {
                        metrics = new NetMetricsTCP();
                        break;
                    }
                case ProtocolType.UDP:
                    {
                        metrics = new NetMetricsUDP();
                        break;
                    }
                default:
                    break;
            }
            timer = new Stopwatch();
            graph = null;
        }
        public void Connect(string IP, int port)
        {
            metrics.Connect(IP, port);
        }
        public void Close()
        {
            metrics.Close();
        }
        public void StartTest(MetricType type, int N)
        {
            switch (type)
            {
                case MetricType.Ping:
                    {
                        timer.Restart();
                        PingCollection.Clear();
                        TimeCollection.Clear();
                        metrics.Ping();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            PingCollection.Add(metrics.Ping());
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(PingCollection);
                        }
                        break;
                    }
                case MetricType.Speed:
                    {
                        timer.Restart();
                        SpeedCollection.Clear();
                        TimeCollection.Clear();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            SpeedCollection.Add(metrics.Speed(speedTime));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(SpeedCollection);
                        }
                        break;
                    }
                case MetricType.Delay:
                    {
                        timer.Restart();
                        DelayCollection.Clear();
                        TimeCollection.Clear();
                        metrics.Delay();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            DelayCollection.Add(metrics.Delay());
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(DelayCollection);
                        }
                        break;
                    }
                case MetricType.DeliveryCoefficient:
                    {
                        timer.Restart();
                        DeliveryCoefCollection.Clear();
                        TimeCollection.Clear();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            DeliveryCoefCollection.Add(metrics.DeliveryCoef(dcPackets, dcPackSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(DeliveryCoefCollection);
                        }
                        break;
                    }
                case MetricType.PacketLoss:
                    {
                        timer.Restart();
                        PacketLooseCollection.Clear();
                        TimeCollection.Clear();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            PacketLooseCollection.Add(metrics.PacketLoss(plPackets, plPackSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(PacketLooseCollection);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public void ShowStatistics(List<double> collection)
        {
            graph.Series["Graph"].Points.Clear();
            double sum = 0;
            for (int i = 0; i < collection.Count; i++)
                sum += collection[i];
            ResultTextbox.Text = (sum / (double)collection.Count).ToString() + " ms";
            for (int i = 0; i < TimeCollection.Count; i++)
                graph.Series["Graph"].Points.AddXY(TimeCollection[i], collection[i]);
        }
    }
}
