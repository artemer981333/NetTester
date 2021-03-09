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
        NetStatistics statistics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            NetStatistics.ProtocolType protocolType;
            switch (ProtocolType.Text)
            {
                case "TCP":
                    {
                        protocolType = NetStatistics.ProtocolType.TCP;
                        break;
                    }
                case "UDP":
                    {
                        protocolType = NetStatistics.ProtocolType.UDP;
                        break;
                    }
                default:
                    throw new Exception("Unknown protocol type");
            }
            if (statistics != null)
                if (statistics.Connected && statistics.Protocol == protocolType)
                    return;
            statistics = new NetStatistics(protocolType);
            statistics.Graph = Graphs;
            statistics.ResultTextbox = Result;
            switch (protocolType)
            {
                case NetStatistics.ProtocolType.TCP:
                    {
                        statistics.Connect(InputIP.Text, 8080);
                        break;
                    }
                case NetStatistics.ProtocolType.UDP:
                    {
                        statistics.Connect(InputIP.Text, 8089);
                        break;
                    }
                default:
                    throw new Exception("Not TCP or UDP");
            }
        }
        private void CloseConnection()
        {
            if (statistics != null)
                statistics.Close();
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
                        statistics.StartTest(NetStatistics.MetricType.Ping, Convert.ToInt32(TestsNumber.Value));
                        break;
                    }
                case 1:
                    {
                        statistics.StartTest(NetStatistics.MetricType.Speed, Convert.ToInt32(TestsNumber.Value));
                        break;
                    }
                case 2:
                    {
                        statistics.StartTest(NetStatistics.MetricType.Delay, Convert.ToInt32(TestsNumber.Value));
                        break;
                    }
                case 3:
                    {
                        statistics.StartTest(NetStatistics.MetricType.DeliveryCoefficient, Convert.ToInt32(TestsNumber.Value));
                        break;
                    }
                case 4:
                    {
                        statistics.StartTest(NetStatistics.MetricType.PacketLoss, Convert.ToInt32(TestsNumber.Value));
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
