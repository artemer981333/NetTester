using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private NetMetrics metrics;
        private Stopwatch timer;
        private ProtocolType protocolType;
        private ExcelPackage excelPackage;
        private ExcelWorksheet sheet;

        public List<double> PingCollection { get; private set; }
        public List<double> SpeedCollection { get; private set; }
        public List<double> DelayCollection { get; private set; }
        public List<double> DeliveryCoefCollection { get; private set; }
        public List<double> PacketLossCollection { get; private set; }
        public List<double> TimeCollection { get; private set; }
        public int SpeedTime { get; set; }
        public int DCPackSize { get; set; }
        public int DCPackets { get; set; }
        public int PLPackSize { get; set; }
        public int PLPackets { get; set; }
        public Chart Graph { get; set; }
        public TextBox ResultTextbox { get; set; }
        public bool Connected => metrics.Connected;
        internal ProtocolType Protocol 
        { 
            get => protocolType; 
            set
            {
                protocolType = value;
                switch (protocolType)
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
            }
        }

        public NetStatistics(ProtocolType protocol)
        {
            timer = new Stopwatch();
            Protocol = protocol;
            Graph = null;
            ResultTextbox = null;
            PingCollection = new List<double>();
            SpeedCollection = new List<double>();
            DelayCollection = new List<double>();
            DeliveryCoefCollection = new List<double>();
            PacketLossCollection = new List<double>();
            TimeCollection = new List<double>();
            excelPackage = new ExcelPackage();
            sheet = excelPackage.Workbook.Worksheets.Add("Statistics");
        }
        public void Connect(string IP, int port)
        {
            metrics.Connect(IP, port);
        }
        public void Close()
        {
            metrics.Close();
        }
        public void ExportInExcel(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            excelPackage.File = fileInfo;
            if (PingCollection.Count != 0)
            {
                sheet.Cells[1, 1].Value = "Ping";
                for (int i = 0; i < PingCollection.Count; i++)
                    sheet.Cells[1, i + 2].Value = PingCollection[i];
            }
            if (SpeedCollection.Count != 0)
            {
                sheet.Cells[2, 1].Value = "Speed";
                for (int i = 0; i < SpeedCollection.Count; i++)
                    sheet.Cells[2, i + 2].Value = SpeedCollection[i];
            }
            if (DelayCollection.Count != 0)
            {
                sheet.Cells[3, 1].Value = "Delay";
                for (int i = 0; i < DelayCollection.Count; i++)
                    sheet.Cells[3, i + 2].Value = DelayCollection[i];
            }
            if (DeliveryCoefCollection.Count != 0)
            {
                sheet.Cells[4, 1].Value = "DC";
                for (int i = 0; i < DeliveryCoefCollection.Count; i++)
                    sheet.Cells[4, i + 2].Value = DeliveryCoefCollection[i];
            }
            if (PacketLossCollection.Count != 0)
            {
                sheet.Cells[5, 1].Value = "PL";
                for (int i = 0; i < PacketLossCollection.Count; i++)
                    sheet.Cells[5, i + 2].Value = PacketLossCollection[i];
            }
            excelPackage.SaveAs(fileInfo);
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
                            ShowStatistics(PingCollection, "ms");
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
                            SpeedCollection.Add(metrics.Speed(SpeedTime));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(SpeedCollection, "MB/s");
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
                            ShowStatistics(DelayCollection, "ms");
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
                            DeliveryCoefCollection.Add(metrics.DeliveryCoef(DCPackets, DCPackSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(DeliveryCoefCollection, "");
                        }
                        break;
                    }
                case MetricType.PacketLoss:
                    {
                        timer.Restart();
                        PacketLossCollection.Clear();
                        TimeCollection.Clear();
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            PacketLossCollection.Add(metrics.PacketLoss(PLPackets, PLPackSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(PacketLossCollection, "");
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public void PeriodicTesting(List<MetricType> metricsTypes, int interval, int N, int numberOfPeriods)
        {

        }
        public void ShowStatistics(List<double> collection, string unit)
        {
            Graph.Series["Values"].Points.Clear();
            double sum = 0;
            for (int i = 0; i < collection.Count; i++)
                sum += collection[i];
            ResultTextbox.Text = (sum / (double)collection.Count).ToString() + " " + unit;
            for (int i = 0; i < TimeCollection.Count; i++)
                Graph.Series["Values"].Points.AddXY(TimeCollection[i], collection[i]);
        }
    }
}
