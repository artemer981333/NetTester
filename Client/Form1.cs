using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OfficeOpenXml;

namespace Client
{
    public partial class Form1 : Form
    {
        NetMetrics metrics;
        public System.Diagnostics.Stopwatch timer;

        List<double> PingCollection, SpeedCollection, DelayCollection, DeliveryKoefCollection, PacketLooseCollection, TimeCollection;
        public Form1()
        {
            PingCollection = new List<double>();
            SpeedCollection = new List<double>();
            DelayCollection = new List<double>();
            DeliveryKoefCollection = new List<double>();
            PacketLooseCollection = new List<double>();
            TimeCollection = new List<double>();
            timer = new System.Diagnostics.Stopwatch();
            InitializeComponent();
        }

        private void Connect()
        {
            if (metrics != null)
                if (metrics.Connected && metrics.Protocol == ProtocolType.Text)
                    return;
            switch (ProtocolType.SelectedIndex)
            {
                case 0:
                    {
                        metrics = new NetMetricsTCP();
                        metrics.Connect(InputIP.Text, 8080);
                        break;
                    }
                case 1:
                    {
                        metrics = new NetMetricsUDP();
                        metrics.Connect(InputIP.Text, 8089);
                        break;
                    }
                default:
                    throw new Exception("Not TCP or UDP");
            }
        }
        private void CloseConnection()
        {
            if (metrics != null)
                metrics.Close();
        }
        private void ShowStatistics(List<double> MetricCollection)
        {
            Graphs.Series["Graph"].Points.Clear();
            double sum = 0;
            for (int i = 0; i < MetricCollection.Count; i++)
                sum += MetricCollection[i];
            Result.Text = (sum / (double)MetricCollection.Count).ToString() + " ms";
            for (int i = 0; i < TimeCollection.Count; i++)
                Graphs.Series["Graph"].Points.AddXY(TimeCollection[i], MetricCollection[i]);
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            CloseConnection();
        }

        private void TestClick(object sender, EventArgs e)
        {
            Connect();
            int index = MetricsChoose.SelectedIndex;
            switch (index)
            {
                case 0:
                    {
                        timer.Restart();
                        PingCollection.Clear();
                        TimeCollection.Clear();
                        int N = Convert.ToInt32(TestsNumber.Value);
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
                case 1:
                    {
                        int time = Convert.ToInt32(FirstParameter.Text);
                        timer.Restart();
                        SpeedCollection.Clear();
                        TimeCollection.Clear();
                        int N = Convert.ToInt32(TestsNumber.Value);
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            SpeedCollection.Add(metrics.Speed(time));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(SpeedCollection);
                        }
                        break;
                    }
                case 2:
                    {
                        timer.Restart();
                        DelayCollection.Clear();
                        TimeCollection.Clear();
                        int N = Convert.ToInt32(TestsNumber.Value);
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
                case 3:
                    {
                        int packets = Convert.ToInt32(FirstParameter.Text);
                        int packSize = Convert.ToInt32(SecondParameter.Text) * 1024;
                        if (packSize >= 65000)
                        {
                            MessageBox.Show("Размер пакета не должен превышать 65000 байт");
                            break;
                        }
                        timer.Restart();
                        DeliveryKoefCollection.Clear();
                        TimeCollection.Clear();
                        int N = Convert.ToInt32(TestsNumber.Value);
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            DeliveryKoefCollection.Add(metrics.DeliveryCoef(packets, packSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(DeliveryKoefCollection);
                        }
                        break;
                    }
                case 4:
                    {
                        int packets = Convert.ToInt32(FirstParameter.Text);
                        int packSize = Convert.ToInt32(SecondParameter.Text) * 1024;
                        if (packSize >= 65000)
                        {
                            MessageBox.Show("Размер пакета не должен превышать 65000 байт");
                            break;
                        }
                        timer.Restart();
                        PacketLooseCollection.Clear();
                        TimeCollection.Clear();
                        int N = Convert.ToInt32(TestsNumber.Value);
                        for (int i = 0; i < N; i++)
                        {
                            Thread.Sleep(1);
                            PacketLooseCollection.Add(metrics.PacketLoss(packets, packSize));
                            TimeCollection.Add(timer.ElapsedMilliseconds / 1000.0);
                            ShowStatistics(PacketLooseCollection);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ChangeTextboxValues(string lable1, string lable2, string val1, string val2, bool val1En, bool val2En)
        {
            label3.Text = lable1;
            label2.Text = lable2;
            SecondParameter.Enabled = val1En;
            FirstParameter.Enabled = val2En;
            SecondParameter.Text = val1;
            FirstParameter.Text = val2;
        }
        private void IndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < MetricsChoose.Items.Count; i++)
                MetricsChoose.SetItemChecked(i, false);
            MetricsChoose.SetItemChecked(MetricsChoose.SelectedIndex, true);

            switch (MetricsChoose.SelectedIndex)
            {
                case 0:
                    {
                        ChangeTextboxValues("Нет параметра", "Нет параметра", "", "", false, false);
                        break;
                    }
                case 1:
                    {
                        ChangeTextboxValues("Время (сек)", "", "", "1", false, true);
                        break;
                    }
                case 2:
                    {
                        ChangeTextboxValues("Нет параметра", "Нет параметра", "", "", false, false);
                        break;
                    }
                case 3:
                    {
                        ChangeTextboxValues("Количество пакетов", "Размер пакета (КБ)", "1", "10", true, true);
                        break;
                    }
                case 4:
                    {
                        ChangeTextboxValues("Количество пакетов", "Размер пакета (КБ)", "1", "10", true, true);
                        break;
                    }
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
